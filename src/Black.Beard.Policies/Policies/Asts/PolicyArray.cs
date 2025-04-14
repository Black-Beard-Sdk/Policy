using System.Collections;

namespace Bb.Policies.Asts
{
    /// <summary>
    /// Represents an array of policy constants in the policy language.
    /// </summary>
    /// <remarks>
    /// This class provides functionality for working with arrays of policy constants 
    /// and implements the enumerable pattern to iterate through its elements.
    /// </remarks>
    public class PolicyArray : PolicyExpression, IEnumerable<PolicyConstant>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyArray"/> class with the specified items.
        /// </summary>
        /// <param name="items">The collection of policy constants to initialize the array with. Must not be null.</param>
        /// <remarks>
        /// This constructor creates a new policy array containing the specified items.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when items is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var constants = new List&lt;PolicyConstant&gt; 
        /// {
        ///     new PolicyConstant("value1"),
        ///     new PolicyConstant("42"),
        ///     new PolicyConstant("true")
        /// };
        /// var array = new PolicyArray(constants);
        /// </code>
        /// </example>
        public PolicyArray(IEnumerable<PolicyConstant> items)
        {
            _items = [.. items];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyArray"/> class with an empty array.
        /// </summary>
        /// <remarks>
        /// This constructor creates a new empty policy array. Items can be added later.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var array = new PolicyArray();
        /// </code>
        /// </example>
        public PolicyArray()
        {
            _items = new List<PolicyConstant>();
        }

        /// <summary>
        /// Accepts a visitor to process this policy array.
        /// </summary>
        /// <typeparam name="T">The return type of the visitor processing.</typeparam>
        /// <param name="visitor">The visitor that will process this array. Must not be null.</param>
        /// <returns>The result of the visitor's processing.</returns>
        /// <remarks>
        /// This method implements the visitor pattern for traversing and processing the policy AST.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when visitor is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var array = new PolicyArray();
        /// var visitor = new PolicyEvaluator&lt;bool&gt;();
        /// bool result = array.Accept(visitor);
        /// </code>
        /// </example>
        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitArray(this);
        }

        /// <summary>
        /// Writes this array to the specified writer.
        /// </summary>
        /// <param name="writer">The writer to which the array should be written. Must not be null.</param>
        /// <returns><c>true</c> if the writing operation was successful; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method writes the array in the format [item1, item2, ...] to the specified writer.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when writer is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var array = new PolicyArray();
        /// var writer = new Writer();
        /// array.ToString(writer);
        /// string result = writer.ToString();
        /// </code>
        /// </example>
        public override bool ToString(Writer writer)
        {

            writer.Append("[");
            bool first = true;
            foreach (var item in _items)
            {
                
                if (!first)
                    writer.Append(", ");
                first = false;

                writer.ToString(item);

            }

            writer.Append("]");

            return true;

        }

        /// <summary>
        /// Returns an enumerator that iterates through the policy constants in this array.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the policy constants.</returns>
        /// <remarks>
        /// This method enables foreach iteration over the array elements.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var array = new PolicyArray();
        /// foreach (var constant in array)
        /// {
        ///     Console.WriteLine(constant);
        /// }
        /// </code>
        /// </example>
        public IEnumerator<PolicyConstant> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the policy constants in this array.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the policy constants.</returns>
        /// <remarks>
        /// This method provides the non-generic version of GetEnumerator for the IEnumerable interface.
        /// </remarks>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        private readonly List<PolicyConstant> _items;

    }

}
