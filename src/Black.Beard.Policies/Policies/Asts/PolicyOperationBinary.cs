using System;

namespace Bb.Policies.Asts
{

    /// <summary>
    /// Represents a binary operation in a policy expression.
    /// </summary>
    /// <remarks>
    /// A binary operation has a left operand, an operator, and a right operand.
    /// This class extends PolicyOperationUnary by adding a right operand to the operation.
    /// Examples of binary operations include equality tests, containment checks, and logical operations.
    /// </remarks>
    //[System.Diagnostics.DebuggerDisplay("{Left} {Operator} {Right}")]
    public class PolicyOperationBinary : PolicyOperationUnary
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyOperationBinary"/> class.
        /// </summary>
        /// <param name="left">The left operand of the binary operation. Must not be null.</param>
        /// <param name="operator">The operator for the binary operation.</param>
        /// <param name="right">The right operand of the binary operation. Must not be null.</param>
        /// <remarks>
        /// This constructor creates a binary operation with the specified left operand, operator, and right operand.
        /// It calls the base constructor to initialize the left operand and operator, then sets the right operand.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when left or right is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var left = new PolicyConstant("value1", ConstantType.String);
        /// var right = new PolicyConstant("value2", ConstantType.String);
        /// var operation = new PolicyOperationBinary(left, PolicyOperator.Equal, right);
        /// </code>
        /// </example>
        public PolicyOperationBinary(Policy left, PolicyOperator @operator, Policy right) 
            : base(left, @operator)
        {
            this.Right = right;
        }

        /// <summary>
        /// Accepts a visitor to process this binary operation.
        /// </summary>
        /// <typeparam name="T">The return type of the visitor processing.</typeparam>
        /// <param name="visitor">The visitor that will process this binary operation. Must not be null.</param>
        /// <returns>The result of the visitor's processing of this binary operation.</returns>
        /// <remarks>
        /// This method implements the visitor pattern for traversing and processing the policy AST.
        /// It calls the visitor's VisitBinaryOperation method with this binary operation as the argument.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when visitor is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var left = new PolicyConstant("value1", ConstantType.String);
        /// var right = new PolicyConstant("value2", ConstantType.String);
        /// var operation = new PolicyOperationBinary(left, PolicyOperator.Equal, right);
        /// var visitor = new PolicyEvaluator&lt;bool&gt;();
        /// bool result = operation.Accept(visitor);
        /// </code>
        /// </example>
        override public T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitBinaryOperation(this);
        }

        /// <summary>
        /// Writes this binary operation to the specified writer.
        /// </summary>
        /// <param name="writer">The writer to which the binary operation should be written. Must not be null.</param>
        /// <returns><c>true</c> if the writing operation was successful.</returns>
        /// <remarks>
        /// This method writes the binary operation in the format "left operator right" to the specified writer.
        /// It first calls the base class's ToString method to write the left operand, then writes the operator
        /// based on its type, and finally writes the right operand.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when writer is null.</exception>
        /// <exception cref="System.NotImplementedException">Thrown when the operator is not supported.</exception>
        /// <example>
        /// <code lang="C#">
        /// var left = new PolicyConstant("value1", ConstantType.String);
        /// var right = new PolicyConstant("value2", ConstantType.String);
        /// var operation = new PolicyOperationBinary(left, PolicyOperator.Equal, right);
        /// var writer = new Writer();
        /// operation.ToString(writer);
        /// string result = writer.ToString(); // Will contain: "value1" = "value2"
        /// </code>
        /// </example>
        public override bool ToString(Writer writer)
        {

            var position = writer.Count;

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

                case PolicyOperator.Greater:
                    writer.Append(" > ");
                    break;

                case PolicyOperator.GreaterOrEqual:
                    writer.Append(" >= ");
                    break;

                case PolicyOperator.Lesser:
                    writer.Append(" < ");
                    break;

                case PolicyOperator.LesserOrEqual:
                    writer.Append(" <= ");
                    break;

                default:
                    throw new NotImplementedException(Operator.ToString());
            
            }

            if (Right != null)
                writer.ToString(Right);

            return position != writer.Count;

        }

        /// <summary>
        /// Gets or sets the right operand of this binary operation.
        /// </summary>
        /// <remarks>
        /// The right operand is the second expression in the binary operation.
        /// For example, in "x = y", "y" is the right operand.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var left = new PolicyConstant("x", ConstantType.Id);
        /// var right = new PolicyConstant("y", ConstantType.Id);
        /// var operation = new PolicyOperationBinary(left, PolicyOperator.Equal, right);
        /// 
        /// // Change the right operand
        /// operation.Right = new PolicyConstant("z", ConstantType.Id);
        /// </code>
        /// </example>
        public Policy Right { get; set; }

    }

}
