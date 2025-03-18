namespace Bb.Policies.Asts
{
    /// <summary>
    /// Interface for objects that can be written to a <see cref="Writer"/>.
    /// </summary>
    /// <remarks>
    /// This interface is used for AST nodes and other objects that need to be formatted and written to output.
    /// </remarks>
    public interface IWriter
    {
        /// <summary>
        /// Writes the current object to the specified writer.
        /// </summary>
        /// <param name="writer">The writer to which the object should be written. Must not be null.</param>
        /// <returns>
        /// <c>true</c> if the writing operation was successful; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method is responsible for formatting and writing the object's content to the specified writer.
        /// The implementation should handle all the necessary formatting and indentation using the writer's methods.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="writer"/> is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// // Create a writer instance
        /// var writer = new Writer();
        /// 
        /// // Use an IWriter implementation to write content
        /// var writable = new MyWritableObject();
        /// bool success = writable.ToString(writer);
        /// 
        /// // Get the resulting string
        /// string result = writer.ToString();
        /// </code>
        /// </example>
        bool ToString(Writer writer);
    }
}
