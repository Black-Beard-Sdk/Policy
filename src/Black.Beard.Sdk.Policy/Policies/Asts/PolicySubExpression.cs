namespace Bb.Policies.Asts
{
    /// <summary>
    /// Represents a variable in a variable.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("({Sub})")]
    public class PolicySubExpression : PolicyExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        /// <param name="name">name of the variable</param>
        public PolicySubExpression(PolicyExpression sub)
        {
            this.Sub = sub;
        }

        public override bool HasSource()
        {
            return Sub.HasSource();
        }

        public PolicyExpression Sub { get; }

        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitSubExpression(this);
        }

        public override bool ToString(Writer writer)
        {
            
            writer.Append("(");

            if (Sub != null)
                writer.ToString(Sub);

            writer.Append(")");

            return true;

        }
    }

}
