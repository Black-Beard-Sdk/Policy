namespace Bb.Policies.Asts
{

    /// <summary>
    /// Represents a comment in a Jslt.
    /// </summary>
    public class PolicyComment : Policy
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyComment"/> class.
        /// </summary>
        public PolicyComment()
        {

        }

        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitComment(this);
        }

        public override bool ToString(Writer writer)
        {
            return true;
        }

    }

}
