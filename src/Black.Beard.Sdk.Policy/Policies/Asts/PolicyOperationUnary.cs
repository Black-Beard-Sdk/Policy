using System;

namespace Bb.Policies.Asts
{


    /// <summary>
    /// Represents a variable in a variable.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Operator}{Left}")]
    public class PolicyOperationUnary : PolicyExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        /// <param name="name">name of the variable</param>
        public PolicyOperationUnary(Policy left, PolicyOperator @operator)
        {
            Kind = PolicyKind.Operation;
            this.Left = left;
            Operator = @operator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        public PolicyOperationUnary()
        {
            Kind = PolicyKind.Operation;
        }

        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitUnaryOperation(this);
        }

        public override bool ToString(Writer writer)
        {

            if (Operator == PolicyOperator.Not)
                    writer.Append("!");

            if (Left != null)
                writer.ToString(Left);

            return true;

        }

        public Policy Left { get; set; }

        public PolicyOperator Operator { get; set; }

    }

}
