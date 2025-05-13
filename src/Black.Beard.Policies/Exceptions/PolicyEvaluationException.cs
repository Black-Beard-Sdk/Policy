namespace Bb.Exceptions
{

	/// <summary>
	/// Represents an exception that is thrown when a policy evaluation fails.
	/// </summary>
	/// <remarks>
	/// This exception is used to indicate issues encountered during the evaluation of policies.
	/// </remarks>
	[Serializable]
	public class PolicyEvaluationException : Exception
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="PolicyEvaluationException"/> class.
		/// </summary>
		public PolicyEvaluationException() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PolicyEvaluationException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <remarks>
		/// Use this constructor to provide a custom error message when the exception is thrown.
		/// </remarks>
		/// <example>
		/// <code lang="C#">
		/// throw new PolicyEvaluationException("Policy evaluation failed due to invalid input.");
		/// </code>
		/// </example>
		public PolicyEvaluationException(string message) : base(message) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PolicyEvaluationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="inner">The exception that is the cause of the current exception.</param>
		/// <remarks>
		/// Use this constructor to provide additional context about the error by including an inner exception.
		/// </remarks>
		/// <example>
		/// <code lang="C#">
		/// try
		/// {
		///     // Some operation that may fail
		/// }
		/// catch (Exception ex)
		/// {
		///     throw new PolicyEvaluationException("Policy evaluation failed.", ex);
		/// }
		/// </code>
		/// </example>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="message"/> is null.</exception>
		public PolicyEvaluationException(string message, Exception inner) : base(message, inner) { }

	}



}
