﻿namespace Bb.Policies.Asts
{

    /// <summary>
    /// Represents a comment in a policy script.
    /// </summary>
    /// <remarks>
    /// Policy comments are used to document policy scripts but do not affect policy execution.
    /// They are preserved in the AST to maintain the full representation of the original script.
    /// </remarks>
    public class PolicyComment : Policy
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyComment"/> class.
        /// </summary>
        /// <remarks>
        /// Creates an empty comment. Content can be added after creation.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var comment = new PolicyComment();
        /// </code>
        /// </example>
        public PolicyComment()
        {

        }

        /// <summary>
        /// Determines whether this policy comment has source information.
        /// </summary>
        /// <returns><c>false</c> as policy comments do not have associated source information.</returns>
        /// <remarks>
        /// This method always returns false because comments are not considered part of the executable policy code
        /// and do not have source tracking information associated with them.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var comment = new PolicyComment();
        /// bool hasSource = comment.HasSource(); // Returns false
        /// </code>
        /// </example>
        public override bool HasSource()
        {
            return false;
        }

        /// <summary>
        /// Accepts a visitor to process this policy comment.
        /// </summary>
        /// <typeparam name="T">The return type of the visitor processing.</typeparam>
        /// <param name="visitor">The visitor that will process this comment. Must not be null.</param>
        /// <returns>The result of the visitor's processing of this comment.</returns>
        /// <remarks>
        /// This method implements the visitor pattern for traversing and processing the policy AST.
        /// Comments are typically ignored during policy execution but may be important for documentation generation.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when visitor is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var comment = new PolicyComment();
        /// var visitor = new DocumentationVisitor&lt;string&gt;();
        /// string result = comment.Accept(visitor);
        /// </code>
        /// </example>
        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitComment(this);
        }

        /// <summary>
        /// Writes this comment to the specified writer.
        /// </summary>
        /// <param name="writer">The writer to which the comment should be written. Must not be null.</param>
        /// <returns><c>true</c> indicating that the operation was successful.</returns>
        /// <remarks>
        /// This method is responsible for formatting and writing the comment's content to the specified writer.
        /// Currently, this implementation does not write any content but returns true to indicate success.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when writer is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var comment = new PolicyComment();
        /// var writer = new Writer();
        /// comment.ToString(writer);
        /// string result = writer.ToString();
        /// </code>
        /// </example>
        public override bool ToString(Writer writer)
        {
            return true;
        }

    }

}
