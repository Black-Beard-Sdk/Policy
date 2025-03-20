namespace Bb.Policies.Asts
{
    /// <summary>
    /// Represents a parenthesized subexpression in a policy expression.
    /// </summary>
    /// <remarks>
    /// This class encapsulates an expression within parentheses to control precedence
    /// or to group parts of complex expressions for clarity.
    /// </remarks>
    [System.Diagnostics.DebuggerDisplay("({Sub})")]
    public class PolicySubExpression : PolicyExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicySubExpression"/> class.
        /// </summary>
        /// <param name="sub">The expression to be wrapped in parentheses. Must not be null.</param>
        /// <remarks>
        /// This constructor creates a new subexpression that wraps the specified expression
        /// in parentheses, which can affect evaluation order or improve readability.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when sub is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var innerExpr = new PolicyConstant("value", ConstantType.String);
        /// var subExpr = new PolicySubExpression(innerExpr);
        /// </code>
        /// </example>
        public PolicySubExpression(PolicyExpression sub)
        {
            this.Sub = sub;
        }

        /// <summary>
        /// Gets the inner expression of this subexpression.
        /// </summary>
        /// <remarks>
        /// The Sub property represents the expression enclosed within parentheses.
        /// </remarks>
        /// <returns>
        /// A <see cref="PolicyExpression"/> representing the inner expression.
        /// </returns>
        public PolicyExpression Sub { get; }

        /// <summary>
        /// Accepts a visitor to process this subexpression.
        /// </summary>
        /// <typeparam name="T">The return type of the visitor processing.</typeparam>
        /// <param name="visitor">The visitor that will process this subexpression. Must not be null.</param>
        /// <returns>The result of the visitor's processing of this subexpression.</returns>
        /// <remarks>
        /// This method implements the visitor pattern for traversing and processing the policy AST.
        /// It calls the visitor's VisitSubExpression method with this subexpression as the argument.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when visitor is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var innerExpr = new PolicyConstant("value", ConstantType.String);
        /// var subExpr = new PolicySubExpression(innerExpr);
        /// var visitor = new PolicyEvaluator&lt;string&gt;();
        /// string result = subExpr.Accept(visitor);
        /// </code>
        /// </example>
        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitSubExpression(this);
        }

        /// <summary>
        /// Writes this subexpression to the specified writer.
        /// </summary>
        /// <param name="writer">The writer to which the subexpression should be written. Must not be null.</param>
        /// <returns><c>true</c> if the writing operation was successful.</returns>
        /// <remarks>
        /// This method writes the subexpression in the format "(innerExpression)" to the specified writer.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when writer is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var innerExpr = new PolicyConstant("value", ConstantType.String);
        /// var subExpr = new PolicySubExpression(innerExpr);
        /// var writer = new Writer();
        /// subExpr.ToString(writer);
        /// string result = writer.ToString(); // Will contain: ("value")
        /// </code>
        /// </example>
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
