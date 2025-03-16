using System;

namespace Bb.Policies.Asts
{

    [System.Diagnostics.DebuggerDisplay("{Left} {Operator} {Right}")]
    public class PolicyOperationBinary : PolicyOperationUnary
    {

        public PolicyOperationBinary(Policy left, PolicyOperator @operator, Policy right) 
            : base(left, @operator)
        {
            this.Right = right;
        }

        override public T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitBinaryOperation(this);
        }

        public override bool ToString(Writer writer)
        {
            var result = base.ToString(writer);

            switch (Operator)
            {

                case PolicyOperator.Equal:
                    writer.Append(" = ");
                    break;

                case PolicyOperator.NotEqual:
                    writer.Append(" != ");
                    break;

                case PolicyOperator.In:
                    writer.Append(" in ");
                    break;

                case PolicyOperator.NotIn:
                    writer.Append(" !in ");
                    break;

                case PolicyOperator.Has:
                    writer.Append(" has ");
                    break;

                case PolicyOperator.HasNot:
                    writer.Append(" !has ");
                    break;

                case PolicyOperator.AndExclusive:
                    writer.Append(" & ");
                    break;

                case PolicyOperator.OrExclusive:
                    writer.Append(" | ");
                    break;

                default:
                    throw new NotImplementedException(Operator.ToString());
            
            }

            if (Right != null)
                writer.ToString(Right);

            return result;
        }

        public Policy Right { get; set; }

    }

}
