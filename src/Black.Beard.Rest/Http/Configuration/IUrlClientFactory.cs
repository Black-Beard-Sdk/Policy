

using RestSharp;

namespace Bb.Http.Configuration
{
    /// <summary>
    /// Interface for defining a strategy for creating, caching, and reusing IUrlClient instances and
    /// their underlying RestClient instances. It is generally preferable to derive from UurlClientFactoryBase
    /// and only override methods as needed, rather than implementing this interface from scratch.
    /// </summary>
    public interface IUrlClientFactory : IDisposable
	{
		/// <summary>
		/// Strategy to create a UrlClient or reuse an existing one, based on the URL being called.
		/// </summary>
		/// <param name="url">The URL being called.</param>
		/// <returns></returns>
		IUrlClient Get(Url url);

		/// <summary>
		/// Defines how RestClient should be instantiated and configured by default. Do NOT attempt
		/// to cache/reuse RestClient instances here - that should be done at the UrlClient level
		/// via a custom UrlClientFactory that gets registered globally.
		/// </summary>
		/// <param name="handler">The HttpMessageHandler used to construct the RestClient.</param>
		/// <returns></returns>
		RestClient CreateRestClient(HttpMessageHandler handler);

		/// <summary>
		/// Defines how the HttpMessageHandler used by RestClients that are created by
		/// this factory should be instantiated and configured. 
		/// </summary>
		/// <returns></returns>
		HttpMessageHandler CreateMessageHandler();
	}
}