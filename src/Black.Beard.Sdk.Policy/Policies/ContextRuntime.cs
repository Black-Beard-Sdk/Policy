using Bb.Analysis.DiagTraces;
using Bb.ComponentModel.Accessors;
using Bb.Expressions;
using Bb.Policies.Asts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;


namespace Bb.Policies
{

    public class RuntimeContext
    {


        static RuntimeContext()
        {
            _evaluateHas = typeof(RuntimeContext).GetMethod(nameof(EvaluateHas), new Type[] { typeof(string), typeof(string[]), typeof(TextLocation) });
            _evaluateHasNot = typeof(RuntimeContext).GetMethod(nameof(EvaluateHasNot), new Type[] { typeof(string), typeof(string[]), typeof(TextLocation) });
            _evaluateIn = typeof(RuntimeContext).GetMethod(nameof(EvaluateIn), new Type[] { typeof(string), typeof(string[]), typeof(TextLocation) });
            _evaluateNotIn = typeof(RuntimeContext).GetMethod(nameof(EvaluateNotIn), new Type[] { typeof(string), typeof(string[]), typeof(TextLocation) });
            _evaluateEquality = typeof(RuntimeContext).GetMethod(nameof(EvaluateEquality), new Type[] { typeof(string), typeof(string), typeof(TextLocation) });
            _evaluateNotEquality = typeof(RuntimeContext).GetMethod(nameof(EvaluateNotEquality), new Type[] { typeof(string), typeof(string), typeof(TextLocation) });
            _evaluate = typeof(RuntimeContext).GetMethod(nameof(Evaluate), new Type[] { typeof(object), typeof(PolicyOperator), typeof(string), typeof(TextLocation) });

            _evaluateFrom = typeof(RuntimeContext).GetMethod(nameof(EvaluateFrom), new Type[] { typeof(string), typeof(TextLocation) });
            _evaluateValue = typeof(RuntimeContext).GetMethod(nameof(EvaluateValue), new Type[] { typeof(string), typeof(string), typeof(TextLocation) });

            _TraceLocation = typeof(RuntimeContext).GetMethod(nameof(TraceLocation), new Type[] { typeof(RuntimeContext), typeof(string), typeof(int), typeof(int), typeof(int), typeof(int) });
            _ExitLocation = typeof(RuntimeContext).GetMethod(nameof(ExitLocation), new Type[] { typeof(RuntimeContext), typeof(object) });
        }


        public RuntimeContext(ScriptDiagnostics diagnostic, Dictionary<string, Func<RuntimeContext, bool>> rules, object datas)
        {
            this._rules = rules;
            _watch = new Stopwatch();
            _stack = new Stack<MethodContext>();
            _dic = new Dictionary<string, object>();
            _diagnostics = diagnostic ?? new ScriptDiagnostics();
            Store(datas);
        }

        private void Store(object datas)
        {

            if (datas == null)
                throw new ArgumentNullException(nameof(datas));

            if (datas is IPrincipal p1)
            {
                _principal = p1;
                _dic.Add("Identity", p1.Identity);
                _dic.Add("identity", p1.Identity);
            }

            else if (datas is IDictionary<string, object> d)
            {
                foreach (var item in d)
                {
                    if (item.Value is IPrincipal p2)
                        _principal = p2;
                    else
                        _dic.Add(item.Key, item.Value);
                }
            }

            else if (datas.GetType().IsClass)
            {
                var properties = datas.GetType().GetAccessors(MemberStrategy.Instance);
                foreach (var item in properties)
                {

                    var v = item.GetValue(datas);
                    if (v is IPrincipal p3)
                        _principal = p3;

                    else
                    {
                        _dic.Add(item.Name, v);
                        var c = item.Name.ToLower();
                        if (c != item.Name)
                            _dic.Add(c, v);
                    }

                }

            }

            else
                throw new Exception($"{nameof(datas)} can't be evaluated");
        }



        public object EvaluateValue(string pathSource, string property, TextLocation textLocation)
        {

            if (!this._dic.TryGetValue(pathSource, out var source))
                _diagnostics.AddError(textLocation, pathSource, $"source {pathSource}, can't be resolved");

            else
            {

                var accessor = source.GetType().GetAccessors(MemberStrategy.Instance);
                if (!accessor.TryGetValue(property, out var acc))
                    _diagnostics.AddError(textLocation, property, $"failed to resolve {property} in {pathSource.GetType()}");

                else
                    return acc.GetValue(source);

            }

            return null;

        }

        public bool EvaluateFrom(string formKey, TextLocation textLocation)
        {

            if (_rules.TryGetValue(formKey, out var rule))
                return rule(this);

            _diagnostics.AddError(textLocation, formKey, $"reference to {formKey} can't be resolved");

            return false;

        }

        public bool EvaluateHas(string key, string[] values, TextLocation textLocation)
        {

            bool result = true;

            if (IsInRole(key))
            {
                foreach (var item in values)
                {
                    var test = _principal.IsInRole(item);
                    if (!test)
                    {
                        result = false;
                        _diagnostics.AddError(textLocation, item, $"user hasn't role {item}");
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
                        _diagnostics.AddError(textLocation, item, $"user hasn't claim {key} = {item}");
                    }
                }


            }

            return result;

        }

        public bool EvaluateHasNot(string key, string[] values, TextLocation textLocation)
        {

            bool result = true;

            if (IsInRole(key))
            {
                foreach (var item in values)
                {
                    var test = _principal.IsInRole(item);
                    if (test)
                    {
                        result = false;
                        _diagnostics.AddError(textLocation, item, $"user has role {item}");
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
                        _diagnostics.AddError(textLocation, item, $"user has claim {key} = {item}");
                    }
                }


            }

            return result;

        }

        public bool EvaluateIn(string key, string[] values, TextLocation textLocation)
        {

            bool result = false;

            if (IsInRole(key))
            {
                foreach (var item in values)
                    if (_principal.IsInRole(item))
                        return true;

                _diagnostics.AddError(textLocation, string.Join(", ", values), $"user hasn't role");

            }
            else if (_principal is ClaimsPrincipal p)
            {
                var pp = p.Claims.Where(c => c.Type == key).ToList();
                foreach (var item in values)
                    if (pp.Any(c => c.Value == item))
                        result = true;

                _diagnostics.AddError(textLocation, string.Join(", ", values), $"user hasn't necessary claim {key}");

            }

            return result;

        }

        public bool EvaluateNotIn(string key, string[] values, TextLocation textLocation)
        {

            bool result = true;

            if (IsInRole(key))
            {
                foreach (var item in values)
                    if (_principal.IsInRole(item))
                    {
                        _diagnostics.AddError(textLocation, item, $"user can't have role {item}");
                        result = false;
                    }
            }

            else if (_principal is ClaimsPrincipal p)
            {
                var pp = p.Claims.Where(c => c.Type == key).ToList();
                foreach (var item in values)
                    if (pp.Any(c => c.Value == item))
                    {
                        result = false;
                        _diagnostics.AddError(textLocation, string.Join(", ", values), $"user hasn't necessary claim {key}");
                    }
            }

            return result;

        }

        public bool EvaluateEquality(string key, string value, TextLocation textLocation)
        {

            bool result = true;

            if (IsInRole(key))
            {
                if (!_principal.IsInRole(value))
                {
                    result = false;
                    _diagnostics.AddError(textLocation, value, $"user must have role {value}");
                }
            }
            else if (_principal is ClaimsPrincipal p)
            {
                var pp = p.Claims.Where(c => c.Type == key).ToList();
                if (!pp.Any(c => c.Value == value))
                {
                    result = false;
                    _diagnostics.AddError(textLocation, value, $"user must have claim {key} = {value}");
                }
            }

            return result;

        }

        public bool EvaluateNotEquality(string key, string value, TextLocation textLocation)
        {

            bool result = true;

            if (IsInRole(key))
            {
                if (_principal.IsInRole(value))
                {
                    result = false;
                    _diagnostics.AddError(textLocation, value, $"user can't have role {value}");
                }
            }
            else if (_principal is ClaimsPrincipal p)
            {
                var pp = p.Claims.Where(c => c.Type == key).ToList();
                if (pp.Any(c => c.Value == value))
                {
                    result = false;
                    _diagnostics.AddError(textLocation, value, $"user can't have claim {key} = {value}");
                }
            }

            return result;

        }

        public bool Evaluate(object left, PolicyOperator @operator, string right, TextLocation textLocation)
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

        private bool IsInRole(string key)
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
            //location.Filename = ctx.ScriptFile;

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

        private class MethodContext
        {

            public MethodContext()
            {
                Watch = new Stopwatch();
            }

            public TextLocation Trace { get; internal set; }

            public Stopwatch Watch { get; }

        }

        internal static readonly MethodInfo? _evaluate;
        internal static readonly MethodInfo? _evaluateEquality;
        internal static readonly MethodInfo? _evaluateNotEquality;
        internal static readonly MethodInfo? _evaluateFrom;
        internal static readonly MethodInfo? _evaluateValue;
        internal static readonly MethodInfo? _evaluateIn;
        internal static readonly MethodInfo? _evaluateNotIn;
        internal static readonly MethodInfo? _evaluateHas;
        internal static readonly MethodInfo? _evaluateHasNot;
        //internal static readonly MethodInfo _isInRole;
        internal static readonly MethodInfo _TraceLocation;
        internal static readonly MethodInfo _ExitLocation;

        private readonly ScriptDiagnostics _diagnostics;
        private readonly Dictionary<string, object> _dic;
        private readonly Dictionary<string, Func<RuntimeContext, bool>> _rules;
        private readonly Stopwatch _watch;
        private readonly Stack<MethodContext> _stack;
        private readonly object sources;
        private IPrincipal _principal;
    }

}
