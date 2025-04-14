using Bb.Http.Configuration;
using Bb.Util;

namespace Bb.Http
{
	/// <summary>
	/// Defines state full aspects (headers, cookies, etc) common to both IUrlClient and IUrlRequest
	/// </summary>
	public interface IHttpSettingsContainer
	{
	    /// <summary>
	    /// Gets the UrlHttpSettings object used by this client or request.
	    /// </summary>
	    UrlHttpSettings Settings { get; }

		/// <summary>
		/// Collection of headers sent on this request or all requests using this client.
		/// </summary>
		INameValueList<string> Headers { get; }
    }
}
