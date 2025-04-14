namespace Bb.Http
{
    /// <summary>
    /// Exception thrown when attempting to add or update an invalid UrlCookie to a CookieJar.
    /// </summary>
    public class InvalidCookieException : Exception
	{
		/// <summary>
		/// Creates a new InvalidCookieException.
		/// </summary>
		public InvalidCookieException(string reason) : base(reason) { }
	}
}
