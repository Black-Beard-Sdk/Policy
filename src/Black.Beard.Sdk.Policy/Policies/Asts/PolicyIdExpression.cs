namespace Bb.Policies.Asts
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class PolicyIdExpression : PolicyConstant
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        /// <param name="name">name of the variable</param>
        public PolicyIdExpression(PolicyConstant constant)
            : this(constant.Value, constant.Type)
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
        public PolicyIdExpression(string value, ConstantType type)
            : base(value, type)
        {
            this.Kind = PolicyKind.IdExpression;
        }

        public override bool HasSource()
        {
            return !string.IsNullOrEmpty(Source);
        }

        public string Source { get; set; }

        public override bool ToString(Writer writer)
        {

            if (!string.IsNullOrEmpty(Source))
            {
                writer.Append(Source);
                writer.Append(".");
            }

            base.ToString(writer);
            
            return true;

        }

    }

}
