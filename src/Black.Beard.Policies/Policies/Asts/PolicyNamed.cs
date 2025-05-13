// Ignore Spelling: Asts

namespace Bb.Policies.Asts
{
    public abstract class PolicyNamed : Policy
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyNamed"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name of the rule. Must not be null or empty.</param>
        /// <remarks>
        /// This constructor initializes a new rule with the specified name and an empty category set.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when name is null.</exception>
        protected PolicyNamed(string name) 
        {
            this.Name = name;
        }


        /// <summary>
        /// Gets the name of this rule.
        /// </summary>
        /// <remarks>
        /// The name uniquely identifies the rule within its container.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var rule = new PolicyRule("CheckAccess");
        /// string name = rule.Name; // Returns "CheckAccess"
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="System.String"/> representing the rule's name.
        /// </returns>
        public string Name { get; }

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
        public string? Origin { get; internal set; }

    }

}
