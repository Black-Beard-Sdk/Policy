
namespace Bb.Policies.Asts
{

    /// <summary>
    /// Represents a policy rule in the policy language.
    /// </summary>
    /// <remarks>
    /// A policy rule defines a named condition or action that can be evaluated against data.
    /// Rules can be categorized and can inherit from other rules.
    /// </remarks>
    //[System.Diagnostics.DebuggerDisplay("policy {Name} : {Value}")]
    public class PolicyRule : PolicyNamed
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyRule"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name of the rule. Must not be null or empty.</param>
        /// <remarks>
        /// This constructor initializes a new rule with the specified name and an empty category set.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when name is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// // Create a new policy rule named "CheckAccess"
        /// var rule = new PolicyRule("CheckAccess");
        /// </code>
        /// </example>
        public PolicyRule(string name) : base(name)
        {
            Kind = PolicyKind.Rule;
            this._categories = new HashSet<string>();
        }

        /// <summary>
        /// Accepts a visitor to process this rule.
        /// </summary>
        /// <typeparam name="T">The return type of the visitor processing.</typeparam>
        /// <param name="visitor">The visitor that will process this rule. Must not be null.</param>
        /// <returns>The result of the visitor's processing of this rule.</returns>
        /// <remarks>
        /// This method implements the visitor pattern for traversing and processing the policy AST.
        /// It calls the visitor's VisitRule method with this rule as the argument.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when visitor is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var rule = new PolicyRule("CheckAccess");
        /// var visitor = new PolicyEvaluator&lt;bool&gt;();
        /// bool result = rule.Accept(visitor);
        /// </code>
        /// </example>
        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitRule(this);
        }

        /// <summary>
        /// Writes this rule to the specified writer.
        /// </summary>
        /// <param name="writer">The writer to which the rule should be written. Must not be null.</param>
        /// <returns><c>true</c> if the writing operation was successful.</returns>
        /// <remarks>
        /// This method writes the rule in the format "policy Name : Value" to the specified writer.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when writer is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var rule = new PolicyRule("CheckAccess");
        /// rule.Value = new PolicyConstant("true", ConstantType.Boolean);
        /// var writer = new Writer();
        /// rule.ToString(writer);
        /// string result = writer.ToString(); // Will contain: policy CheckAccess : true
        /// </code>
        /// </example>
        public override bool ToString(Writer writer)
        {

            writer.Append($"policy {Name} : ");

            if (Value != null)
                writer.ToString(Value);

            return true;

        }

        /// <summary>
        /// Adds categories to this rule.
        /// </summary>
        /// <param name="categories">The collection of categories to add. Can be null.</param>
        /// <remarks>
        /// This method adds the specified categories to the rule's category set.
        /// Null or empty category names are ignored.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var rule = new PolicyRule("CheckAccess");
        /// rule.AddCategories(new[] { "Security", "AccessControl" });
        /// </code>
        /// </example>
        public void AddCategories(IEnumerable<string> categories)
        {
            if (categories != null)
                foreach (string category in categories) 
                    if (!string.IsNullOrEmpty(category))
                        this._categories.Add(category);
        }

        /// <summary>
        /// Checks if this rule has all the specified categories.
        /// </summary>
        /// <param name="categories">The categories to check for. Must not be null.</param>
        /// <returns><c>true</c> if the rule has all the specified categories; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method checks if the rule has all the specified categories.
        /// Note: The current implementation returns false if any of the specified categories is not null or empty,
        /// which appears to be incorrect. It should check if each required category is in the rule's category set.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when categories is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var rule = new PolicyRule("CheckAccess");
        /// rule.AddCategories(new[] { "Security", "AccessControl" });
        /// bool hasCategories = rule.WithCategories("Security", "AccessControl");
        /// </code>
        /// </example>
        public bool WithCategories(params string[] categories)
        {
            foreach (string category in categories)
                if (!string.IsNullOrEmpty(category))
                    return false;
            return true;
        }

        /// <summary>
        /// Gets the categories associated with this rule.
        /// </summary>
        /// <remarks>
        /// Categories can be used to group and filter rules for specific purposes,
        /// such as security rules, validation rules, etc.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var rule = new PolicyRule("CheckAccess");
        /// rule.AddCategories(new[] { "Security", "AccessControl" });
        /// 
        /// foreach (string category in rule.Categories)
        /// {
        ///     Console.WriteLine(category);
        /// }
        /// </code>
        /// </example>
        /// <returns>
        /// A collection of <see cref="System.String"/> values representing the rule's categories.
        /// </returns>
        public IEnumerable<string> Categories => this._categories;

        private readonly HashSet<string> _categories;

        /// <summary>
        /// Gets or sets the value expression of this rule.
        /// </summary>
        /// <remarks>
        /// The value represents the condition or action defined by this rule.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var rule = new PolicyRule("IsAdult");
        /// var left = new PolicyIdExpression("age", ConstantType.Id) { Source = "user" };
        /// var right = new PolicyConstant("18", ConstantType.String);
        /// rule.Value = new PolicyOperationBinary(left, PolicyOperator.Equal, right);
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="Policy"/> object representing the rule's value expression.
        /// </returns>
        public Policy Value { get; set; }

        /// <summary>
        /// Gets or sets the origin path from which this rule was loaded.
        /// </summary>
        /// <remarks>
        /// This property stores the file path or other source identifier from which
        /// the rule was loaded, which can be useful for debugging and auditing.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var rule = Policy.ParsePath(@"C:\policies\access.policy").GetRule("AdminAccess");
        /// string origin = rule.Origin; // Returns "C:\policies\access.policy"
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="System.String"/> representing the origin path of this rule.
        /// </returns>
        public string Origin { get; internal set; }

    }

}
