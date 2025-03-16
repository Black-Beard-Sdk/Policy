namespace Bb.Policies.Asts
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class PolicyIdExpression : PolicyConstant
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        /// <param name="name">name of the variable</param>
        public PolicyIdExpression(PolicyConstant constant, bool optional)
            : this(constant.Value, constant.Type, optional)
        {
            Location = constant.Location;
        }

        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitId(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        /// <param name="name">name of the variable</param>
        public PolicyIdExpression(string value, ConstantType type, bool optional)
            : base(value, type)
        {
            this.Kind = PolicyKind.IdExpression;
            this.Optional = optional;
        }

        public bool Optional { get; }

        public string Source { get; set; }

        public override bool ToString(Writer writer)
        {

            if (!string.IsNullOrEmpty(Source))
            {
                writer.Append(Source);
                writer.Append(".");
            }

            base.ToString(writer);
            
            if (Optional)
                writer.Append("?");

            return true;
        }

    }

}
