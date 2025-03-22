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
        public PolicyNamed(string name) 
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

        public string Origin { get; internal set; }

    }

}
