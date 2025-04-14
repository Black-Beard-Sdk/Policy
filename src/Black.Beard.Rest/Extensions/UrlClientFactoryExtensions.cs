using System.Runtime.CompilerServices;
using Bb.Http;
using Bb.Http.Configuration;
using RestSharp;

namespace Bb.Extensions
{
    /// <summary>
    /// Extension methods on IUrlClientFactory
    /// </summary>
    public static class UrlClientFactoryExtensions
    {
        // https://stackoverflow.com/questions/51563732/how-do-i-lock-when-the-ideal-scope-of-the-lock-object-is-known-only-at-runtime
        private static readonly ConditionalWeakTable<IUrlClient, object> _clientLocks = new ConditionalWeakTable<IUrlClient, object>();

        /// <summary>
        /// Provides thread-safe access to a specific IUrlClient, typically to configure settings and default headers.
        /// The URL is used to find the client, but keep in mind that the same client will be used in all calls to the same host by default.
        /// </summary>
        /// <param name="factory">This IUrlClientFactory.</param>
        /// <param name="url">the URL used to find the IUrlClient.</param>
        /// <param name="configAction">the action to perform against the IUrlClient.</param>
        public static IUrlClientFactory ConfigureClient(this IUrlClientFactory factory, string url, Action<IUrlClient> configAction)
        {
            var client = factory.Get(url);
            lock (_clientLocks.GetOrCreateValue(client))
            {
                configAction(client);
            }
            return factory;
        }

        /// <summary>
        /// Creates an RestClient with the HttpMessageHandler returned from this factory's CreateMessageHandler method.
        /// </summary>
        public static RestClient CreateRestClient(this IUrlClientFactory fac) => fac.CreateRestClient(fac.CreateMessageHandler());
    }
}