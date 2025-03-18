using System;

namespace Bb.Policies.Asts
{

    /// <summary>
    /// Represents a variable in a policy.
    /// </summary>
    /// <remarks>
    /// Policy variables define named constants that can be referenced in policy rules.
    /// They are typically defined using an "alias" syntax in the policy language.
    /// </remarks>
    [System.Diagnostics.DebuggerDisplay("alias {Name} : {Value}")]
    public class PolicyVariable : Policy
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name of the variable. Must not be null or empty.</param>
        /// <remarks>
        /// This constructor creates a new variable with the specified name and sets its kind to Variable.
        /// The value must be set separately after creation.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when name is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// // Create a new policy variable named "MaxRetries"
        /// var variable = new PolicyVariable("MaxRetries");
        /// variable.Value = new PolicyConstant("3", ConstantType.String);
        /// </code>
        /// </example>
        public PolicyVariable(string name, bool cache)
        {
            Kind = PolicyKind.Variable;
            this.Name = name;
            this._cache = cache;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class with default values.
        /// </summary>
        /// <remarks>
        /// This constructor creates a new variable with no name or value.
        /// The name and value must be set separately after creation.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// // Create a new policy variable with default values
        /// var variable = new PolicyVariable();
        /// </code>
        /// </example>
        public PolicyVariable()
        {
            Kind = PolicyKind.Variable;
        }

        /// <summary>
        /// Accepts a visitor to process this policy variable.
        /// </summary>
        /// <typeparam name="T">The return type of the visitor processing.</typeparam>
        /// <param name="visitor">The visitor that will process this variable. Must not be null.</param>
        /// <returns>The result of the visitor's processing of this variable.</returns>
        /// <remarks>
        /// This method implements the visitor pattern for traversing and processing the policy AST.
        /// It calls the visitor's VisitVariable method with this variable as the argument.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when visitor is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var variable = new PolicyVariable("MaxRetries");
        /// var visitor = new PolicyEvaluator&lt;string&gt;();
        /// string result = variable.Accept(visitor);
        /// </code>
        /// </example>
        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitVariable(this);
        }

        /// <summary>
        /// Writes this variable to the specified writer.
        /// </summary>
        /// <param name="writer">The writer to which the variable should be written. Must not be null.</param>
        /// <returns><c>true</c> if the writing operation was successful.</returns>
        /// <remarks>
        /// This method writes the variable in the format "alias Name : Value" to the specified writer.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when writer is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var variable = new PolicyVariable("MaxRetries");
        /// variable.Value = new PolicyConstant("3", ConstantType.String);
        /// var writer = new Writer();
        /// variable.ToString(writer);
        /// string result = writer.ToString(); // Will contain: alias MaxRetries : "3"
        /// </code>
        /// </example>
        public override bool ToString(Writer writer)
        {

            if (_cache)
                return true;

            writer.Append($"alias {Name} : ");

            if (Value != null)
                writer.ToString(Value);

            writer.AppendEndLine();

            return true;

        }

        /// <summary>
        /// Determines whether this variable has source information.
        /// </summary>
        /// <returns><c>false</c>, as policy variables do not have source information.</returns>
        /// <remarks>
        /// This method always returns false because variables are typically static declarations
        /// and do not reference external sources.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var variable = new PolicyVariable("MaxRetries");
        /// bool hasSource = variable.HasSource(); // Returns false
        /// </code>
        /// </example>
        public override bool HasSource()
        {
            return false;
        }

        /// <summary>
        /// Gets the name of this variable.
        /// </summary>
        /// <remarks>
        /// The name uniquely identifies the variable within its container.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var variable = new PolicyVariable("MaxRetries");
        /// string name = variable.Name; // Returns "MaxRetries"
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="System.String"/> representing the variable's name.
        /// </returns>
        public string Name { get; }

        private readonly bool _cache;

        /// <summary>
        /// Gets or sets the constant value of this variable.
        /// </summary>
        /// <remarks>
        /// The value represents the data that the variable holds.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var variable = new PolicyVariable("MaxRetries");
        /// variable.Value = new PolicyConstant("3", ConstantType.String);
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="PolicyConstant"/> representing the variable's value.
        /// </returns>
        public PolicyConstant Value { get; set; }

        /// <summary>
        /// Gets or sets the origin path from which this variable was loaded.
        /// </summary>
        /// <remarks>
        /// This property stores the file path or other source identifier from which
        /// the variable was loaded, which can be useful for debugging and auditing.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var container = Policy.ParsePath(@"C:\policies\config.policy");
        /// var variable = container.Variables.First();
        /// string origin = variable.Origin; // Returns "C:\policies\config.policy"
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="System.String"/> representing the origin path of this variable.
        /// </returns>
        public string Origin { get; internal set; }
    }

}
