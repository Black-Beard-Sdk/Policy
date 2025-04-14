

namespace Bb.Http.Configuration
{
	/// <summary>
	/// An IUrlClientFactory implementation that caches and reuses the same UrlClient instance
	/// per URL requested, which it assumes is a "base" URL, and sets the IUrlClient.BaseUrl property
	/// to that value. Ideal for use with IoC containers - register as a singleton, inject into a service
	/// that wraps some web service, and use to set a private IUrlClient field in the constructor.
	/// </summary>
	public class PerBaseUrlClientFactory : UrlClientFactoryBase
	{
		/// <summary>
		/// Returns the entire URL, which is assumed to be some "base" URL for a service.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <returns>The cache key</returns>
		protected override string GetCacheKey(Url url) => url.ToString();

		/// <summary>
		/// Returns a new new UrlClient with BaseUrl set to the URL passed.
		/// </summary>
		/// <param name="url">The URL</param>
		/// <returns></returns>
		protected override IUrlClient Create(Url url) => new UrlClient(url);
	}
}
