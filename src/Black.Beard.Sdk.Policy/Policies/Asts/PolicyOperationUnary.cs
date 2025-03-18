using System;

namespace Bb.Policies.Asts
{
    /// <summary>
    /// Represents a unary operation in a policy expression.
    /// </summary>
    /// <remarks>
    /// A unary operation consists of an operator and a single operand.
    /// This class serves as the base for binary operations and can represent operations like logical negation.
    /// </remarks>
    [System.Diagnostics.DebuggerDisplay("{Operator}{Left}")]
    public class PolicyOperationUnary : PolicyExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyOperationUnary"/> class with the specified operand and operator.
        /// </summary>
        /// <param name="left">The operand of the unary operation. Must not be null.</param>
        /// <param name="operator">The operator for the unary operation.</param>
        /// <remarks>
        /// This constructor creates a unary operation with the specified operand and operator.
        /// The operand is set to the Left property and the operation kind is set to Operation.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when left is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var operand = new PolicyConstant("value", ConstantType.Boolean);
        /// var operation = new PolicyOperationUnary(operand, PolicyOperator.Not);
        /// </code>
        /// </example>
        public PolicyOperationUnary(Policy left, PolicyOperator @operator)
        {
            Kind = PolicyKind.Operation;
            this.Left = left;
            Operator = @operator;
        }

        /// <summary>
        /// Determines whether this unary operation has source information.
        /// </summary>
        /// <returns><c>true</c> if the operand has source information; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method delegates to the Left operand's HasSource method, as a unary operation's
        /// source information is determined by its operand.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var operand = new PolicyIdExpression("propertyName", ConstantType.Id) { Source = "context" };
        /// var operation = new PolicyOperationUnary(operand, PolicyOperator.Not);
        /// bool hasSource = operation.HasSource(); // Returns true because the operand has a source
        /// </code>
        /// </example>
        public override bool HasSource()
        {
            return this.Left.HasSource();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyOperationUnary"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor creates an empty unary operation with no operand or operator specified.
        /// The operation kind is set to Operation.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var operation = new PolicyOperationUnary();
        /// operation.Left = new PolicyConstant("value", ConstantType.Boolean);
        /// operation.Operator = PolicyOperator.Not;
        /// </code>
        /// </example>
        public PolicyOperationUnary()
        {
            Kind = PolicyKind.Operation;
        }

        /// <summary>
        /// Accepts a visitor to process this unary operation.
        /// </summary>
        /// <typeparam name="T">The return type of the visitor processing.</typeparam>
        /// <param name="visitor">The visitor that will process this unary operation. Must not be null.</param>
        /// <returns>The result of the visitor's processing of this unary operation.</returns>
        /// <remarks>
        /// This method implements the visitor pattern for traversing and processing the policy AST.
        /// It calls the visitor's VisitUnaryOperation method with this unary operation as the argument.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when visitor is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var operand = new PolicyConstant("true", ConstantType.Boolean);
        /// var operation = new PolicyOperationUnary(operand, PolicyOperator.Not);
        /// var visitor = new PolicyEvaluator&lt;bool&gt;();
        /// bool result = operation.Accept(visitor);
        /// </code>
        /// </example>
        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitUnaryOperation(this);
        }

        /// <summary>
        /// Writes this unary operation to the specified writer.
        /// </summary>
        /// <param name="writer">The writer to which the unary operation should be written. Must not be null.</param>
        /// <returns><c>true</c> if the writing operation was successful.</returns>
        /// <remarks>
        /// This method writes the unary operation to the specified writer.
        /// For the Not operator, it writes "!" followed by the operand.
        /// For other operators, it just writes the operand.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when writer is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var operand = new PolicyConstant("true", ConstantType.Boolean);
        /// var operation = new PolicyOperationUnary(operand, PolicyOperator.Not);
        /// var writer = new Writer();
        /// operation.ToString(writer);
        /// string result = writer.ToString(); // Will contain: !true
        /// </code>
        /// </example>
        public override bool ToString(Writer writer)
        {

            if (Operator == PolicyOperator.Not)
                    writer.Append("!");

            if (Left != null)
                writer.ToString(Left);

            if (Operator == PolicyOperator.Required)
                writer.Append("+");

            return true;
        }

        /// <summary>
        /// Gets or sets the operand of this unary operation.
        /// </summary>
        /// <remarks>
        /// The Left property represents the operand of the unary operation.
        /// For example, in the expression "!x", "x" is the operand.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var operation = new PolicyOperationUnary();
        /// operation.Left = new PolicyConstant("value", ConstantType.Boolean);
        /// </code>
        /// </example>
        public Policy Left { get; set; }

        /// <summary>
        /// Gets or sets the operator of this unary operation.
        /// </summary>
        /// <remarks>
        /// The Operator property specifies what operation should be performed on the operand.
        /// For unary operations, this is typically the Not operator.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var operation = new PolicyOperationUnary();
        /// operation.Operator = PolicyOperator.Not;
        /// </code>
        /// </example>
        public PolicyOperator Operator { get; set; }
    }
}
