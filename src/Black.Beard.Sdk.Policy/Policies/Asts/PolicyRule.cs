using System.Collections.Generic;

namespace Bb.Policies.Asts
{
    /// <summary>
    /// Represents a policy rule
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("policy {Name} : {Value}")]
    public class PolicyRule : Policy
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        /// <param name="name">name of the variable</param>
        public PolicyRule(string name)
        {
            Kind = PolicyKind.Rule;
            this.Name = name;
            this._categories = new HashSet<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        public PolicyRule()
        {
            Kind = PolicyKind.Rule;
        }

        public override bool HasSource()
        {
            return Value.HasSource();
        }

        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitRule(this);
        }

        public override bool ToString(Writer writer)
        {

            writer.Append($"policy {Name} : ");

            if (Value != null)
                writer.ToString(Value);

            writer.AppendEndLine();

            return true;

        }

        /// <summary>
        /// Add categories on the rule
        /// </summary>
        /// <param name="categories"></param>
        public void AddCategories(IEnumerable<string> categories)
        {
            if (categories != null)
                foreach (string category in categories) 
                    if (!string.IsNullOrEmpty(category))
                        this._categories.Add(category);
        }

        /// <summary>
        /// Return true if the rule has all required categories
        /// </summary>
        /// <param name="categories">list of category to required</param>
        /// <returns></returns>
        public bool WithCategories(params string[] categories)
        {
            foreach (string category in categories)
                if (!string.IsNullOrEmpty(category))
                    return false;
            return true;
        }

        /// <summary>
        /// Categories of the rule
        /// </summary>
        public IEnumerable<string> Categories => this._categories;

        /// <summary>
        /// Name of the rule
        /// </summary>
        public string Name { get; }

        private readonly HashSet<string> _categories;

        /// <summary>
        /// Rule expression
        /// </summary>
        public Policy Value { get; set; }

        /// <summary>
        /// Specify the category inherit of another one category
        /// </summary>
        public string InheritFrom { get; set; }

        /// <summary>
        /// Return the origin source of the path where the rule was loaded
        /// </summary>
        public string Origin { get; internal set; }

    }

}
