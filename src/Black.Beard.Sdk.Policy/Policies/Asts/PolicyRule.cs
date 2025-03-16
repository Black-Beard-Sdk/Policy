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
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        public PolicyRule()
        {
            Kind = PolicyKind.Rule;
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

        public string Name { get; }

        public Policy Value { get; set; }
        public string InheritFrom { get; internal set; }
    }

}
