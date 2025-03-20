namespace Bb.Policies.Asts
{


    /// <summary>
    /// Represents a constant value in a policy expression.
    /// </summary>
    /// <remarks>
    /// Policy constants can represent different types of values like strings, identifiers, 
    /// and booleans. They are immutable and store both the value and its type.
    /// </remarks>
    //[System.Diagnostics.DebuggerDisplay("{Value}")]
    public class PolicyConstant : PolicyExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyConstant"/> class with the specified value and type.
        /// </summary>
        /// <param name="value">The string representation of the constant value. Must not be null.</param>
        /// <param name="type">The type of the constant.</param>
        /// <remarks>
        /// This constructor creates a new policy constant with the specified value and type.
        /// The value is stored as provided without additional processing.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when value is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// // Create a string constant
        /// var stringConstant = new PolicyConstant("Hello", ConstantType.String);
        /// 
        /// // Create a boolean constant
        /// var booleanConstant = new PolicyConstant("true", ConstantType.Boolean);
        /// 
        /// // Create an identifier constant
        /// var idConstant = new PolicyConstant("variableName", ConstantType.Id);
        /// </code>
        /// </example>
        public PolicyConstant(string value, ConstantType type)
        {
            this.Kind = PolicyKind.Constant;

            //var o = value.Trim();
            //if (o.StartsWith("\"") && o.EndsWith("\""))
            //    o = o.Trim('"');
            //else if (o.StartsWith("'") && o.EndsWith("'"))
            //    o = o.Trim('\'');

            this.Value = value;
            this.Type = type;
        }

        /// <summary>
        /// Gets the constant value as a string.
        /// </summary>
        /// <remarks>
        /// This property provides access to the raw string value of the constant.
        /// The interpretation of this value depends on the Type property.
        /// </remarks>
        /// <returns>
        /// A string containing the constant value.
        /// </returns>
        public string Value { get; }

        /// <summary>
        /// Gets the type of this constant.
        /// </summary>
        /// <remarks>
        /// The type determines how the value should be interpreted and formatted
        /// when writing the constant to a string or evaluating it.
        /// </remarks>
        /// <returns>
        /// A <see cref="ConstantType"/> enum value indicating the type of this constant.
        /// </returns>
        public ConstantType Type { get; }

        /// <summary>
        /// Accepts a visitor to process this policy constant.
        /// </summary>
        /// <typeparam name="T">The return type of the visitor processing.</typeparam>
        /// <param name="visitor">The visitor that will process this constant. Must not be null.</param>
        /// <returns>The result of the visitor's processing of this constant.</returns>
        /// <remarks>
        /// This method implements the visitor pattern for traversing and processing the policy AST.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when visitor is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var constant = new PolicyConstant("example", ConstantType.String);
        /// var visitor = new PolicyEvaluator&lt;bool&gt;();
        /// bool result = constant.Accept(visitor);
        /// </code>
        /// </example>
        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitConstant(this);
        }

        /// <summary>
        /// Writes this constant to the specified writer with appropriate formatting based on its type.
        /// </summary>
        /// <param name="writer">The writer to which the constant should be written. Must not be null.</param>
        /// <returns><c>true</c> if the writing operation was successful; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method formats the constant differently based on its type:
        /// - Boolean values are written as "true" or "false"
        /// - String values are enclosed in double quotes
        /// - QuotedId values are enclosed in single quotes
        /// - Id values are written as-is without quotes
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when writer is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var constant = new PolicyConstant("example", ConstantType.String);
        /// var writer = new Writer();
        /// constant.ToString(writer);
        /// string result = writer.ToString(); // Will contain: "example"
        /// </code>
        /// </example>
        public override bool ToString(Writer writer)
        {
         
            switch (Type)
            {
                case ConstantType.Boolean:
                    if (Value == "true")
                        writer.Append("true");
                    else
                        writer.Append("false");
                    break;
                case ConstantType.String:
                    writer.Append($"\"{Value}\"");
                    break;

                case ConstantType.QuotedId:
                    writer.Append($"'{Value}'");
                    break;

                case ConstantType.Integer:
                    writer.Append(Value);
                    break;

                case ConstantType.Id:
                default:
                    writer.Append(Value);
                    break;

            }

            return true;
        }

    }

    /// <summary>
    /// Defines the types of constants that can be used in policy expressions.
    /// </summary>
    /// <remarks>
    /// This enumeration specifies the different types of constants that policy expressions can contain,
    /// which affects how they are interpreted and formatted.
    /// </remarks>
    public enum ConstantType
    {
        /// <summary>
        /// Represents a string constant, enclosed in double quotes.
        /// </summary>
        String,

        /// <summary>
        /// Represents an identifier constant, without quotes.
        /// </summary>
        Id,

        /// <summary>
        /// Represents a quoted identifier constant, enclosed in single quotes.
        /// </summary>
        QuotedId,

        /// <summary>
        /// Represents a boolean constant (true or false).
        /// </summary>
        Boolean,

        /// <summary>
        /// Represents an positive integer constant
        /// </summary>
        Integer
    
    }

 
}
