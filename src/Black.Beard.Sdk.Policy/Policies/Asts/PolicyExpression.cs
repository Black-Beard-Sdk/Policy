namespace Bb.Policies.Asts
{

    /// <summary>
    /// Abstract base class for all policy expression elements.
    /// </summary>
    /// <remarks>
    /// This class serves as a common base for all types of expressions in the policy language,
    /// including constants, arrays, operations, and identifiers. It provides shared functionality
    /// and enables polymorphic handling of different expression types.
    /// </remarks>
    public abstract class PolicyExpression : Policy
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is called by derived classes to initialize the base expression.
        /// Since PolicyExpression is an abstract class, it cannot be instantiated directly.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// // PolicyExpression is abstract, so it must be used through derived classes:
        /// public class MyExpression : PolicyExpression
        /// {
        ///     public MyExpression() : base()
        ///     {
        ///         // Initialization code
        ///     }
        /// }
        /// </code>
        /// </example>
        public PolicyExpression()
        {

        }

    }    

}
