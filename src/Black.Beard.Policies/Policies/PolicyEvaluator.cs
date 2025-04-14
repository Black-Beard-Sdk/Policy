using Bb.Analysis.DiagTraces;
using Bb.Policies.Asts;

namespace Bb.Policies
{

    /// <summary>
    /// Evaluates policy rules against data objects.
    /// </summary>
    /// <remarks>
    /// PolicyEvaluator compiles policy rules from a container into executable functions
    /// and provides methods to evaluate those rules against data objects.
    /// </remarks>
    public class PolicyEvaluator
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyEvaluator"/> class.
        /// </summary>
        /// <param name="container">The policy container with rules to evaluate. Must not be null.</param>
        /// <param name="withDebug">Whether to include debug information in the compiled rules.</param>
        /// <remarks>
        /// This constructor compiles all rules in the provided container into executable functions.
        /// If withDebug is true, the compiled rules will include additional diagnostics for debugging.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when container is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// // Create a policy container with rules
        /// var container = new PolicyContainer();
        /// var rule = new PolicyRule("IsAdult");
        /// rule.Value = new PolicyOperationBinary(
        ///     new PolicyIdExpression("age", ConstantType.Id) { Source = "user" },
        ///     PolicyOperator.Equal,
        ///     new PolicyConstant("18", ConstantType.String)
        /// );
        /// container.Add(rule);
        /// 
        /// // Create an evaluator for the container
        /// var evaluator = new PolicyEvaluator(container);
        /// </code>
        /// </example>
        public PolicyEvaluator(PolicyContainer container, bool withDebug = false)
        {

            this._dic = new Dictionary<string, Func<RuntimeContext, bool>>();

            var e = new PoliciesParserBuilder(container.Diagnostics, this, withDebug);
            e.Evaluate(container);

        }

        /// <summary>
        /// Adds a compiled rule function to the evaluator.
        /// </summary>
        /// <param name="policyName">The name of the policy rule. Must not be null or empty.</param>
        /// <param name="rule">The compiled rule function. Must not be null.</param>
        /// <remarks>
        /// This method adds a named compiled rule to the evaluator's dictionary.
        /// If a rule with the same name already exists, it will not be replaced.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when policyName or rule is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var evaluator = new PolicyEvaluator(new PolicyContainer());
        /// 
        /// // Add a custom rule function
        /// evaluator.AddPolicyRule("HasPermission", context => {
        ///     var user = context.GetSource() as UserProfile;
        ///     return user != null && user.Permissions.Contains("admin");
        /// });
        /// </code>
        /// </example>
        public void AddPolicyRule(string policyName, Func<RuntimeContext, bool> rule)
        {

            if (!this._dic.ContainsKey(policyName))
                this._dic.Add(policyName, rule);

        }

        /// <summary>
        /// Evaluates a policy rule against a data object.
        /// </summary>
        /// <param name="policy">The name of the policy rule to evaluate. Must not be null or empty.</param>
        /// <param name="value">The data object to evaluate against the rule. Must not be null.</param>
        /// <param name="context">The runtime context for the evaluation.</param>
        /// <returns>True if the data object satisfies the policy rule; otherwise, false.</returns>
        /// <remarks>
        /// This method evaluates a named policy rule against a provided data object.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when policy or value is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var evaluator = new PolicyEvaluator(new PolicyContainer());
        /// var user = new UserProfile { Age = 20 };
        /// 
        /// // Evaluate the "IsAdult" policy rule against the user object
        /// var result = evaluator.Evaluate("IsAdult", user, out var context);
        /// </code>
        /// </example>
        public bool Evaluate(string policy, object value, out RuntimeContext context)
        {
           return Evaluate(policy, value, null, out context);
        }

        /// <summary>
        /// Evaluates a policy rule against a data object with diagnostics.
        /// </summary>
        /// <param name="policy">The name of the policy rule to evaluate. Must not be null or empty.</param>
        /// <param name="value">The data object to evaluate against the rule. Must not be null.</param>
        /// <param name="diagnostic">Optional diagnostics for the evaluation.</param>
        /// <param name="context">The runtime context for the evaluation.</param>
        /// <returns>True if the data object satisfies the policy rule; otherwise, false.</returns>
        /// <remarks>
        /// This method evaluates a named policy rule against a provided data object, with optional diagnostics.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when policy or value is null.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown when policy is missing.</exception>
        /// <example>
        /// <code lang="C#">
        /// var evaluator = new PolicyEvaluator(new PolicyContainer());
        /// var user = new UserProfile { Age = 20 };
        /// var diagnostics = new ScriptDiagnostics();
        /// 
        /// // Evaluate the "IsAdult" policy rule against the user object with diagnostics
        /// var result = evaluator.Evaluate("IsAdult", user, diagnostics, out var context);
        /// </code>
        /// </example>
        public bool Evaluate(string policy, object value, ScriptDiagnostics? diagnostic, out RuntimeContext context)
        {

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (string.IsNullOrEmpty(policy))
                throw new ArgumentNullException(nameof(policy));

            if (_dic.TryGetValue(policy, out var rule))
            {

                if (CreateContext != null)
                    context = CreateContext();
                else
                    context = new RuntimeContext();

                context
                    .Initialize(diagnostic, this._dic)
                    .Store(value);

                if (ServiceProvider != null)
                    context.Store(ServiceProvider);

                context.Result = rule(context);

                return context.Result;
            
            }

            throw new InvalidOperationException(policy);

        }

        /// <summary>
        /// Gets the runtime context for the evaluation.
        /// </summary>
        public Func<RuntimeContext> CreateContext { get; set; }

        /// <summary>
        /// Gets or sets the service provider for the evaluator.
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        private readonly Dictionary<string, Func<RuntimeContext, bool>> _dic;

    }

}
