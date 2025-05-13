using Bb.Accessors;
using Bb.Analysis.DiagTraces;
using Bb.Converters;
using Bb.Expressions;
using Bb.Policies.Asts;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Linq;

namespace Bb.Policies
{

    /// <summary>
    /// Provides a runtime context for policy evaluation.
    /// </summary>
    /// <remarks>
    /// RuntimeContext manages the state during policy evaluation, including access to data objects,
    /// rule functions, and diagnostic information. It also provides methods for evaluating
    /// different types of policy operations.
    /// </remarks>
    public class RuntimeContext
    {

        /// <summary>
        /// Initializes static members of the <see cref="RuntimeContext"/> class.
        /// </summary>
        /// <remarks>
        /// This static constructor caches reflection references to the evaluation methods to improve performance
        /// when these methods are called dynamically during policy execution.
        /// </remarks>
        static RuntimeContext()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeContext"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor initializes the runtime context with the specified diagnostics, rules, and data.
        /// The data object is stored in a dictionary for access during policy evaluation.
        /// </remarks>
        public RuntimeContext()
        {
            _diagnostics = new ScriptDiagnostics();
            _principal = null;
            ServiceProvider = null;


            _stack = new Stack<MethodContext>();
            _dic = new Dictionary<string, object>();
            _rules = new Dictionary<string, Func<RuntimeContext, bool>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeContext"/> class.
        /// </summary>
        /// <param name="diagnostic">The diagnostic collector for recording errors and information during rule evaluation. If null, a new instance is created.</param>
        /// <param name="rules">A dictionary of named policy rules that can be referenced during evaluation.</param>
        /// <param name="datas">The data object that provides context values for policy evaluation.</param>
        /// <remarks>
        /// This constructor initializes the runtime context with the specified diagnostics, rules, and data.
        /// The data object is stored in a dictionary for access during policy evaluation.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when data is null during the Store operation.</exception> 
        public RuntimeContext Initialize(ScriptDiagnostics? diagnostic, Dictionary<string, Func<RuntimeContext, bool>> rules)
        {

            if (rules != null)
                foreach (var item in rules)
                    _rules.Add(item.Key, item.Value);

            _diagnostics = diagnostic ?? [];

            return this;

        }

        /// <summary>
        /// Stores the data object in the context's dictionary.
        /// </summary>
        /// <param name="data">The data object to store.</param>
        /// <remarks>
        /// This method processes the data object and stores its properties in the context's dictionary
        /// for access during policy evaluation. It handles different types of data objects:
        /// - IPrincipal objects are stored as the context's principal
        /// - Dictionaries have their key-value pairs added directly
        /// - Other objects have their properties extracted and added
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when data is null.</exception>
        /// <exception cref="Exception">Thrown when data cannot be evaluated.</exception>
        public RuntimeContext Store(object data)
        {

            ArgumentNullException.ThrowIfNull(data);

            if (data is IPrincipal)
                Store(null, data);

            else if (data is IServiceProvider)
                Store(null, data);

            else if (data is IDictionary<string, object> d)
                foreach (var item in d)
                    Store(item.Key, item.Value);

            else if (data.GetType().IsClass)
            {
                var properties = data.GetType().GetAccessors(MemberStrategys.Instance);
                foreach (var item in properties)
                    if (item.GetValue != null)
                        Store(item.Name, item.GetValue(data));
            }

            else
                throw new InvalidOperationException($"{nameof(data)} can't be evaluated");

            return this;

        }

        /// <summary>
        /// Store data
        /// </summary>
        /// <param name="key">key of the object</param>
        /// <param name="value">value to store</param>
        /// <returns></returns>
        public RuntimeContext Store(string? key, object value)
        {

            if (value is IPrincipal p2)
            {
                _principal = p2;
                if (p2.Identity != null)
                {
                    _dic.Add("Identity", p2.Identity);
                    _dic.Add("identity", p2.Identity);
                }

            }
            else if (value is IServiceProvider serviceProvider1)
                ServiceProvider = serviceProvider1;

            else if (!string.IsNullOrEmpty(key))
                _dic.Add(key, value);

            return this;

        }

        /// <summary>
        /// Evaluates a property value from a specified source object.
        /// </summary>
        /// <param name="pathSource">The name of the source object in the context dictionary.</param>
        /// <param name="property">The name of the property to evaluate.</param>
        /// <param name="textLocation">The location in the source text for error reporting.</param>
        /// <returns>The value of the specified property, or null if either the source or property cannot be resolved.</returns>
        /// <remarks>
        /// This method retrieves a source object from the context dictionary and then accesses
        /// a specified property on that object. It adds diagnostic errors if either the source
        /// or property cannot be resolved.
        /// </remarks>
        /// <exception cref="KeyNotFoundException">Not thrown directly, but may occur during dictionary access.</exception>
        /// <example>
        /// <code lang="C#">
        /// var context = new RuntimeContext(new ScriptDiagnostics(), new Dictionary&lt;string, Func&lt;RuntimeContext, bool&gt;&gt;(), 
        ///     new { user = new { name = "John", age = 25 } });
        /// var location = TextLocation.Create((1, 1, 0), (1, 10, 9));
        /// object age = context.EvaluateValue("user", "age", location);
        /// // age will be 25
        /// </code>
        /// </example>
        /// <returns>
        /// The value of the specified property, or null if the source or property cannot be resolved.
        /// </returns>
        public object? EvaluateValue(string pathSource, string property, TextLocation textLocation)
        {

            object? result = default;

            if (!this._dic.TryGetValue(pathSource, out var source))
                _diagnostics.AddInformation(textLocation, pathSource, $"source {pathSource}, can't be resolved");

            else
            {

                var accessor = source.GetType().GetAccessors(MemberStrategys.Instance);
                if (!accessor.TryGetValue(property, out var acc))
                    _diagnostics.AddInformation(textLocation, property, $"failed to resolve {property} in {pathSource.GetType()}");

                else if (acc != null && acc.GetValue != null)
                    result = acc.GetValue(source);

            }

            return result;

        }

        /// <summary>
        /// Evaluates a rule by its name.
        /// </summary>
        /// <param name="formKey">The name of the rule to evaluate.</param>
        /// <param name="textLocation">The location in the source text for error reporting.</param>
        /// <returns><c>true</c> if the rule evaluates to true; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method looks up a rule by name in the rules dictionary and executes it.
        /// If the rule is not found, it adds an error to the diagnostics.
        /// </remarks>
        /// <returns>
        /// <c>true</c> if the rule evaluates to true; <c>false</c> if the rule evaluates to false or cannot be found.
        /// </returns>
        public bool EvaluateFrom(string formKey, Analysis.DiagTraces.TextLocation textLocation)
        {

            if (_rules.TryGetValue(formKey, out var rule))
                return rule(this);

            _diagnostics.AddInformation(textLocation, formKey, $"reference to {formKey} can't be resolved");

            return false;

        }

        /// <summary>
        /// Evaluates whether the principal has all the specified roles or claims.
        /// </summary>
        /// <param name="key">The role key or claim type to check.</param>
        /// <param name="values">The values to check for.</param>
        /// <param name="textLocation">The location in the source text for error reporting.</param>
        /// <returns><c>true</c> if the principal has all the specified roles or claims; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method checks if the principal has all the specified roles (if key is "role")
        /// or claims of the specified type. It adds diagnostic errors for any missing roles or claims.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var principal = new GenericPrincipal(
        ///     new GenericIdentity("user"), 
        ///     new[] { "Administrator", "User" }
        /// );
        /// var context = new RuntimeContext(new ScriptDiagnostics(), 
        ///     new Dictionary&lt;string, Func&lt;RuntimeContext, bool&gt;&gt;(), principal);
        /// var location = TextLocation.Create((1, 1, 0), (1, 10, 9));
        /// bool result = context.EvaluateHas("role", new[] { "Administrator", "User" }, location);
        /// // result will be true
        /// </code>
        /// </example>
        /// <returns>
        /// <c>true</c> if the principal has all the specified roles or claims; otherwise, <c>false</c>.
        /// </returns>
        public bool EvaluateHas(string key, string[] values, TextLocation textLocation)
        {

            if (_principal == null)
                return false;

            bool result = true;

            if (IsInRole(key))
            {
                foreach (var item in values)
                {
                    var test = _principal.IsInRole(item);
                    if (!test)
                    {
                        result = false;
                        _diagnostics.AddInformation(textLocation, item, $"user hasn't role {item}");
                    }
                }
            }
            else if (_principal is ClaimsPrincipal p)
            {
                var pp = p.Claims.Where(c => c.Type == key).ToList();
                foreach (var item in values)
                {
                    var test = pp.Any(c => c.Value == item);
                    if (!test)
                    {
                        result = false;
                        _diagnostics.AddInformation(textLocation, item, $"user hasn't claim {key} = {item}");
                    }
                }


            }

            return result;

        }

        /// <summary>
        /// Evaluates whether the principal does not have any of the specified roles or claims.
        /// </summary>
        /// <param name="key">The role key or claim type to check.</param>
        /// <param name="values">The values to check for absence.</param>
        /// <param name="textLocation">The location in the source text for error reporting.</param>
        /// <returns><c>true</c> if the principal does not have any of the specified roles or claims; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method checks if the principal does not have any of the specified roles (if key is "role")
        /// or claims of the specified type. It adds diagnostic errors for any found roles or claims.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var principal = new GenericPrincipal(
        ///     new GenericIdentity("user"), 
        ///     new[] { "User" }
        /// );
        /// var context = new RuntimeContext(new ScriptDiagnostics(), 
        ///     new Dictionary&lt;string, Func&lt;RuntimeContext, bool&gt;&gt;(), principal);
        /// var location = TextLocation.Create((1, 1, 0), (1, 10, 9));
        /// bool result = context.EvaluateHasNot("role", new[] { "Administrator" }, location);
        /// // result will be true
        /// </code>
        /// </example>
        /// <returns>
        /// <c>true</c> if the principal does not have any of the specified roles or claims; otherwise, <c>false</c>.
        /// </returns>
        public bool EvaluateHasNot(string key, string[] values, TextLocation textLocation)
        {

            if (_principal == null)
                return false;

            bool result = true;

            if (IsInRole(key))
            {
                foreach (var item in values)
                {
                    var test = _principal.IsInRole(item);
                    if (test)
                    {
                        result = false;
                        _diagnostics.AddInformation(textLocation, item, $"user has role {item}");
                    }
                }
            }
            else if (_principal is ClaimsPrincipal p)
            {
                var pp = p.Claims.Where(c => c.Type == key).ToList();
                foreach (var item in values)
                {
                    var test = pp.Any(c => c.Value == item);
                    if (test)
                    {
                        result = false;
                        _diagnostics.AddInformation(textLocation, item, $"user has claim {key} = {item}");
                    }
                }


            }

            return result;

        }

        /// <summary>
        /// Evaluates whether the principal has any of the specified roles or claims.
        /// </summary>
        /// <param name="key">The role key or claim type to check.</param>
        /// <param name="values">The values to check for.</param>
        /// <param name="textLocation">The location in the source text for error reporting.</param>
        /// <returns><c>true</c> if the principal has any of the specified roles or claims; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method checks if the principal has any of the specified roles (if key is "role")
        /// or claims of the specified type. It adds diagnostic errors if none of the roles or claims are found.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var principal = new GenericPrincipal(
        ///     new GenericIdentity("user"), 
        ///     new[] { "User" }
        /// );
        /// var context = new RuntimeContext(new ScriptDiagnostics(), 
        ///     new Dictionary&lt;string, Func&lt;RuntimeContext, bool&gt;&gt;(), principal);
        /// var location = TextLocation.Create((1, 1, 0), (1, 10, 9));
        /// bool result = context.EvaluateIn("role", new[] { "Administrator", "User" }, location);
        /// // result will be true
        /// </code>
        /// </example>
        /// <returns>
        /// <c>true</c> if the principal has any of the specified roles or claims; otherwise, <c>false</c>.
        /// </returns>
        public bool EvaluateIn(string key, string[] values, TextLocation textLocation)
        {

            if (_principal == null)
                return false;

            bool result = false;

            if (IsInRole(key))
            {
                if (values.Any(c => _principal.IsInRole(c)))
                    return true;

                _diagnostics.AddInformation(textLocation, string.Join(", ", values), $"user hasn't role");

            }
            else if (_principal is ClaimsPrincipal p)
            {
                var pp = p.Claims.Where(c => c.Type == key).ToList();
                foreach (var _ in values.Where(item => pp.Any(c => c.Value == item)).Select(item => new { }))
                    result = true;

                _diagnostics.AddInformation(textLocation, string.Join(", ", values), $"user hasn't necessary claim {key}");

            }

            return result;

        }

        /// <summary>
        /// Evaluates whether the principal does not have any of the specified roles or claims.
        /// </summary>
        /// <param name="key">The role key or claim type to check.</param>
        /// <param name="values">The values to check for absence.</param>
        /// <param name="textLocation">The location in the source text for error reporting.</param>
        /// <returns><c>true</c> if the principal does not have any of the specified roles or claims; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method checks if the principal does not have any of the specified roles (if key is "role")
        /// or claims of the specified type. It adds diagnostic errors for any found roles or claims.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var principal = new GenericPrincipal(
        ///     new GenericIdentity("user"), 
        ///     new[] { "User" }
        /// );
        /// var context = new RuntimeContext(new ScriptDiagnostics(), 
        ///     new Dictionary&lt;string, Func&lt;RuntimeContext, bool&gt;&gt;(), principal);
        /// var location = TextLocation.Create((1, 1, 0), (1, 10, 9));
        /// bool result = context.EvaluateNotIn("role", new[] { "Admin" }, location);
        /// // result will be true
        /// </code>
        /// </example>
        /// <returns>
        /// <c>true</c> if the principal does not have any of the specified roles or claims; otherwise, <c>false</c>.
        /// </returns>
        public bool EvaluateNotIn(string key, string[] values, TextLocation textLocation)
        {

            if (_principal == null)
                return false;

            bool result = true;

            if (IsInRole(key))
            {
                foreach (var item in values.Where(item => _principal.IsInRole(item)))
                {
                    _diagnostics.AddInformation(textLocation, item, $"user can't have role {item}");
                    result = false;
                }
            }

            else if (_principal is ClaimsPrincipal p)
            {
                var pp = p.Claims.Where(c => c.Type == key).ToList();
                foreach (var _ in values.Where(item => pp.Any(c => c.Value == item)).Select(item => new { }))
                {
                    result = false;
                    _diagnostics.AddInformation(textLocation, string.Join(", ", values), $"user hasn't necessary claim {key}");
                }
            }

            return result;

        }

        /// <summary>
        /// Evaluates whether the principal has a specific role or claim.
        /// </summary>
        /// <param name="key">The role key or claim type to check.</param>
        /// <param name="value">The value to check for.</param>
        /// <param name="textLocation">The location in the source text for error reporting.</param>
        /// <returns><c>true</c> if the principal has the specified role or claim; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method checks if the principal has the specified role (if key is "role")
        /// or claim of the specified type. It adds diagnostic errors if the role or claim is not found.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var principal = new GenericPrincipal(
        ///     new GenericIdentity("user"), 
        ///     new[] { "Admin" }
        /// );
        /// var context = new RuntimeContext(new ScriptDiagnostics(), 
        ///     new Dictionary&lt;string, Func&lt;RuntimeContext, bool&gt;&gt;(), principal);
        /// var location = TextLocation.Create((1, 1, 0), (1, 10, 9));
        /// bool result = context.EvaluateEquality("role", "Admin", location);
        /// // result will be true
        /// </code>
        /// </example>
        /// <returns>
        /// <c>true</c> if the principal has the specified role or claim; otherwise, <c>false</c>.
        /// </returns>
        public bool EvaluateEquality(string key, string value, TextLocation textLocation)
        {

            if (_principal == null)
                return false;

            bool result = true;

            if (IsInRole(key))
            {
                if (!_principal.IsInRole(value))
                {
                    result = false;
                    _diagnostics.AddInformation(textLocation, value, $"user must have role {value}");
                }
            }
            else if (_principal is ClaimsPrincipal p)
            {
                var pp = p.Claims.Where(c => c.Type == key).ToList();
                if (!pp.Any(c => c.Value == value))
                {
                    result = false;
                    _diagnostics.AddInformation(textLocation, value, $"user must have claim {key} = {value}");
                }
            }

            return result;

        }

        /// <summary>
        /// Evaluates whether the principal does not have a specific role or claim.
        /// </summary>
        /// <param name="key">The role key or claim type to check.</param>
        /// <param name="value">The value to check for absence.</param>
        /// <param name="textLocation">The location in the source text for error reporting.</param>
        /// <returns><c>true</c> if the principal does not have the specified role or claim; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method checks if the principal does not have the specified role (if key is "role")
        /// or claim of the specified type. It adds diagnostic errors if the role or claim is found.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var principal = new GenericPrincipal(
        ///     new GenericIdentity("user"), 
        ///     new[] { "User" }
        /// );
        /// var context = new RuntimeContext(new ScriptDiagnostics(), 
        ///     new Dictionary&lt;string, Func&lt;RuntimeContext, bool&gt;&gt;(), principal);
        /// var location = TextLocation.Create((1, 1, 0), (1, 10, 9));
        /// bool result = context.EvaluateNotEquality("role", "Admin", location);
        /// // result will be true
        /// </code>
        /// </example>
        /// <returns>
        /// <c>true</c> if the principal does not have the specified role or claim; otherwise, <c>false</c>.
        /// </returns>
        public bool EvaluateNotEquality(string key, string value, TextLocation textLocation)
        {

            if (_principal == null)
                return false;

            bool result = true;

            if (IsInRole(key))
            {
                if (_principal.IsInRole(value))
                {
                    result = false;
                    _diagnostics.AddInformation(textLocation, value, $"user can't have role {value}");
                }
            }
            else if (_principal is ClaimsPrincipal p)
            {
                var pp = p.Claims.Where(c => c.Type == key).ToList();
                if (pp.Any(c => c.Value == value))
                {
                    result = false;
                    _diagnostics.AddInformation(textLocation, value, $"user can't have claim {key} = {value}");
                }
            }

            return result;

        }

        /// <summary>
        /// Evaluates a policy operation.
        /// </summary>
        /// <param name="left">The left operand of the operation.</param>
        /// <param name="operator">The policy operator to apply.</param>
        /// <param name="right">The right operand of the operation.</param>
        /// <param name="textLocation">The location in the source text for error reporting.</param>
        /// <returns><c>true</c> if the operation evaluates to true; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method evaluates a policy operation based on the specified operator and operands.
        /// It supports equality, inequality, and other policy-specific operations.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var context = new RuntimeContext(new ScriptDiagnostics(), 
        ///     new Dictionary&lt;string, Func&lt;RuntimeContext, bool&gt;&gt;(), new { });
        /// var location = TextLocation.Create((1, 1, 0), (1, 10, 9));
        /// bool result = context.Evaluate(5, PolicyOperator.Equal, "5", location);
        /// // result will be true
        /// </code>
        /// </example>
        /// <returns>
        /// <c>true</c> if the operation evaluates to true; otherwise, <c>false</c>.
        /// </returns>
        public bool EvaluateBinary(object left, PolicyOperator @operator, string right, TextLocation textLocation)
        {

            if (left == null)
                switch (@operator)
                {

                    case PolicyOperator.Equal:
                        return right == null;
                    case PolicyOperator.NotEqual:
                        return right != null;

                    case PolicyOperator.Has:
                    case PolicyOperator.HasNot:
                    case PolicyOperator.AndExclusive:
                    case PolicyOperator.OrExclusive:
                    case PolicyOperator.Not:
                    case PolicyOperator.In:
                    case PolicyOperator.NotIn:
                    case PolicyOperator.Undefined:
                    default:
                        return false;

                }

            var type = left.GetType();
            var r = right.ConvertTo(type, CultureInfo.CurrentCulture);

            switch (@operator)
            {

                case PolicyOperator.Equal:
                    return object.Equals(left, r);

                case PolicyOperator.NotEqual:
                    return !object.Equals(left, r);

                case PolicyOperator.In:
                    break;
                case PolicyOperator.NotIn:
                    break;
                case PolicyOperator.Has:
                    break;
                case PolicyOperator.HasNot:
                    break;
                case PolicyOperator.AndExclusive:
                    break;
                case PolicyOperator.OrExclusive:
                    break;
                case PolicyOperator.Not:
                case PolicyOperator.Undefined:
                default:
                    break;
            }

            return false;

        }

        public bool EvaluateBinaryNumeric(object left, PolicyOperator @operator, int right, TextLocation textLocation)
        {

            if (left == null)
                return false;

            var l = left.ConvertTo<int>(CultureInfo.CurrentCulture);

            switch (@operator)
            {

                case PolicyOperator.Lesser:
                    return l < right;

                case PolicyOperator.LesserOrEqual:
                    return l <= right;

                case PolicyOperator.Greater:
                    return l > right;

                case PolicyOperator.GreaterOrEqual:
                    return l >= right;

                case PolicyOperator.Equal:
                    return l == right;

                case PolicyOperator.NotEqual:
                    return l != right;

                default:
                    break;

            }

            return false;

        }

        public bool EvaluateUnary(object left, PolicyOperator @operator, TextLocation textLocation)
        {

            if (left == null)
                return false;

            switch (@operator)
            {

                case PolicyOperator.Required:
                    bool result = true;
                    var key = left.ConvertTo<string>(CultureInfo.CurrentCulture);
                    if (!string.IsNullOrEmpty(key ) && _principal is ClaimsPrincipal p && !p.Claims.Any(c => c.Type == key))
                        {
                            result = false;
                            _diagnostics.AddInformation(textLocation, key, $"user must have claim {key}");
                        }
                    return result;

                case PolicyOperator.UnaryCompare:

                    break;

                case PolicyOperator.Equal:
                case PolicyOperator.NotEqual:
                case PolicyOperator.In:
                case PolicyOperator.NotIn:
                case PolicyOperator.Has:
                case PolicyOperator.HasNot:
                case PolicyOperator.AndExclusive:
                case PolicyOperator.OrExclusive:
                case PolicyOperator.Undefined:
                default:
                    break;

            }

            return false;

        }

        public bool EvaluateConvertBool(object left, TextLocation textLocation)
        {
            if (left == null)
                return false;
            var result = left.ConvertTo<bool>(CultureInfo.CurrentCulture);
            return result;
        }

        public bool IsInRole(string key)
        {

            var c = key.ToLower();

            if (c == "role")
                return true;

            if (c == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                return true;

            return false;

        }


        #region trace

        public static bool IsTrace(RuntimeContext ctx, TextLocation trace)
        {

            if (ctx._stack.Count > 0)
                return ctx._stack.Peek().Trace == trace;

            return false;

        }

        public static object ExitLocation(RuntimeContext ctx, object result)
        {

            if (ctx._stack.Count > 0)
            {
                var e = ctx._stack.Pop();
                e.Watch.Stop();

                ctx._diagnostics.AddInformation("", e.Trace, "Diagnostic", $"Elapsed time in {e.Trace.Get("Function")} seconds(s) {Math.Round(e.Watch.Elapsed.TotalSeconds, 4)}");

            }

            if (ctx._stack.Count > 0)
            {
                var p = ctx._stack.Peek();
                p.Watch.Stop();
                p.Watch.Restart();
            }

            return result;

        }

        public static RuntimeContext TraceLocation(RuntimeContext ctx, string functionName, TextLocation token)
        {
            var e = new MethodContext()
            {
                Trace = token,
            };
            ctx._stack.Push(e);

            e.Watch.Start();

            return ctx;
        }

        public static RuntimeContext TraceLocation(RuntimeContext ctx, string functionName, int line, int column, int position, int positionEnd)
        {

            var location = TextLocation.Create((line, column, position), (-1, -1, positionEnd));

            var e = new MethodContext()
            {
                Trace = location.Add("Function", functionName),
            };
            ctx._stack.Push(e);

            e.Watch.Start();

            return ctx;
        }

        #endregion trace


        //[DebuggerStepThrough]
        //[DebuggerNonUserCode]
        public void Stop()
        {

            if (!Debugger.IsAttached)
                Debugger.Launch();

            if (StopIsActivated && Debugger.IsAttached)
                Debugger.Break();

        }


        public bool StopIsActivated { get; set; } = true;

        public bool Result { get; internal set; }
        public IServiceProvider? ServiceProvider { get; private set; }

        private sealed class MethodContext
        {

            public MethodContext()
            {
                Watch = new Stopwatch();
            }

            public TextLocation? Trace { get; internal set; }

            public Stopwatch Watch { get; }

        }


        internal static readonly MethodInfo _evaluateHas = typeof(RuntimeContext).GetMethod(nameof(EvaluateHas), [typeof(string), typeof(string[]), typeof(TextLocation)])
                ?? throw new InvalidDataException(nameof(EvaluateHas));

        internal static readonly MethodInfo _evaluateHasNot = typeof(RuntimeContext).GetMethod(nameof(EvaluateHasNot), [typeof(string), typeof(string[]), typeof(TextLocation)])
                ?? throw new InvalidDataException(nameof(EvaluateHasNot));

        internal static readonly MethodInfo _evaluateIn = typeof(RuntimeContext).GetMethod(nameof(EvaluateIn), [typeof(string), typeof(string[]), typeof(TextLocation)])
                ?? throw new InvalidDataException(nameof(EvaluateIn));

        internal static readonly MethodInfo _evaluateNotIn = typeof(RuntimeContext).GetMethod(nameof(EvaluateNotIn), [typeof(string), typeof(string[]), typeof(TextLocation)])
                ?? throw new InvalidDataException(nameof(EvaluateNotIn));

        internal static readonly MethodInfo _evaluateEquality = typeof(RuntimeContext).GetMethod(nameof(EvaluateEquality), [typeof(string), typeof(string), typeof(TextLocation)])
                ?? throw new InvalidDataException(nameof(EvaluateEquality));

        internal static readonly MethodInfo _evaluateNotEquality = typeof(RuntimeContext).GetMethod(nameof(EvaluateNotEquality), [typeof(string), typeof(string), typeof(TextLocation)])
                ?? throw new InvalidDataException(nameof(EvaluateNotEquality));

        internal static readonly MethodInfo _evaluateValue = typeof(RuntimeContext).GetMethod(nameof(EvaluateValue), [typeof(string), typeof(string), typeof(TextLocation)])
                ?? throw new InvalidDataException(nameof(EvaluateValue));

        internal static readonly MethodInfo _evaluateBinary = typeof(RuntimeContext).GetMethod(nameof(EvaluateBinary), [typeof(object), typeof(PolicyOperator), typeof(string), typeof(TextLocation)])
                ?? throw new InvalidDataException(nameof(EvaluateBinary));

        internal static readonly MethodInfo _evaluateBinaryNumeric = typeof(RuntimeContext).GetMethod(nameof(EvaluateBinaryNumeric), [typeof(object), typeof(PolicyOperator), typeof(int), typeof(TextLocation)])
                ?? throw new InvalidDataException(nameof(EvaluateBinaryNumeric));

        internal static readonly MethodInfo _evaluateUnary = typeof(RuntimeContext).GetMethod(nameof(EvaluateUnary), [typeof(object), typeof(PolicyOperator), typeof(TextLocation)])
                ?? throw new InvalidDataException(nameof(EvaluateUnary));

        internal static readonly MethodInfo _evaluateFrom = typeof(RuntimeContext).GetMethod(nameof(EvaluateFrom), [typeof(string), typeof(TextLocation)])
                ?? throw new InvalidDataException(nameof(EvaluateFrom));

        internal static readonly MethodInfo _evaluateconvertBool = typeof(RuntimeContext).GetMethod(nameof(EvaluateConvertBool), [typeof(object), typeof(TextLocation)])
                ?? throw new InvalidDataException(nameof(EvaluateConvertBool));

        internal static readonly MethodInfo _TraceLocation = typeof(RuntimeContext).GetMethod(nameof(TraceLocation), [typeof(RuntimeContext), typeof(string), typeof(int), typeof(int), typeof(int), typeof(int)])
                ?? throw new InvalidDataException(nameof(TraceLocation));

        internal static readonly MethodInfo _ExitLocation = typeof(RuntimeContext).GetMethod(nameof(ExitLocation), [typeof(RuntimeContext), typeof(object)])
                ?? throw new InvalidDataException(nameof(ExitLocation));

        private ScriptDiagnostics _diagnostics;
        private readonly Dictionary<string, object> _dic;
        private readonly Dictionary<string, Func<RuntimeContext, bool>> _rules;
        private readonly Stack<MethodContext> _stack;
        private IPrincipal? _principal;
    
    }

}
