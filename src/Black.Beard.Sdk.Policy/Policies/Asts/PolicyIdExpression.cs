namespace Bb.Policies.Asts
{
    /// <summary>
    /// Represents an identifier expression in a policy.
    /// </summary>
    /// <remarks>
    /// PolicyIdExpression is used for variables, properties, and other identifiers that may be 
    /// qualified with a source. It extends PolicyConstant to include source information.
    /// </remarks>
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class PolicyIdExpression : PolicyConstant
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyIdExpression"/> class from an existing constant.
        /// </summary>
        /// <param name="constant">The constant to convert to an identifier expression. Must not be null.</param>
        /// <remarks>
        /// This constructor creates a new identifier expression with the same value and type as the provided constant,
        /// and copies its location information.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when constant is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var constant = new PolicyConstant("variableName", ConstantType.Id);
        /// var idExpression = new PolicyIdExpression(constant);
        /// </code>
        /// </example>
        public PolicyIdExpression(PolicyConstant constant)
            : this(constant.Value, constant.Type)
        {
            Location = constant.Location;
        }

        /// <summary>
        /// Accepts a visitor to process this identifier expression.
        /// </summary>
        /// <typeparam name="T">The return type of the visitor processing.</typeparam>
        /// <param name="visitor">The visitor that will process this identifier expression. Must not be null.</param>
        /// <returns>The result of the visitor's processing of this identifier expression.</returns>
        /// <remarks>
        /// This method implements the visitor pattern for traversing and processing the policy AST.
        /// It calls the visitor's VisitId method with this identifier expression as the argument.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when visitor is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var idExpression = new PolicyIdExpression("variableName", ConstantType.Id);
        /// var visitor = new PolicyEvaluator&lt;bool&gt;();
        /// bool result = idExpression.Accept(visitor);
        /// </code>
        /// </example>
        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitId(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyIdExpression"/> class with the specified value and type.
        /// </summary>
        /// <param name="value">The string value of the identifier. Must not be null.</param>
        /// <param name="type">The type of the identifier constant.</param>
        /// <remarks>
        /// This constructor creates a new identifier expression with the specified value and type,
        /// and sets its kind to IdExpression.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when value is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var idExpression = new PolicyIdExpression("propertyName", ConstantType.Id);
        /// </code>
        /// </example>
        public PolicyIdExpression(string value, ConstantType type)
            : base(value, type)
        {
            this.Kind = PolicyKind.IdExpression;
        }

        /// <summary>
        /// Gets or sets the source qualifier for this identifier expression.
        /// </summary>
        /// <remarks>
        /// The source represents the qualifier part of a qualified identifier (e.g., "context" in "context.propertyName").
        /// When null or empty, the identifier is considered unqualified.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var idExpression = new PolicyIdExpression("propertyName", ConstantType.Id);
        /// idExpression.Source = "context";
        /// // Now represents "context.propertyName"
        /// </code>
        /// </example>
        public string Source { get; set; }

        public override bool ToString(Writer writer)
        {

            var position = writer.Count;

            if (!string.IsNullOrEmpty(Source))
            {
                writer.Append(Source);
                writer.Append(".");
            }

            base.ToString(writer);

            return position != writer.Count;

        }

    }

}
