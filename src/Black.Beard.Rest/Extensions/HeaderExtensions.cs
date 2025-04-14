using System.Collections;
using System.Text;
using Bb.Http;
using Bb.Util;

namespace Bb.Extensions
{
    /// <summary>
    /// Fluent extension methods for working with HTTP request headers.
    /// </summary>
    public static class HeaderExtensions
    {

        /// <summary>
        /// Sets an HTTP header to be sent with this IUrlRequest or all requests made with this IUrlClient.
        /// </summary>
        /// <param name="clientOrRequest">The IUrlClient or IUrlRequest.</param>
        /// <param name="name">HTTP header name.</param>
        /// <param name="value">HTTP header value.</param>
        /// <returns>This IUrlClient or IUrlRequest.</returns>
        public static T WithHeader<T>(this T clientOrRequest, string name, object value) where T : IHttpSettingsContainer
        {
            if (value == null)
                clientOrRequest.Headers.Remove(name);
            else
                clientOrRequest.Headers.AddOrReplace(name, value.ToInvariantString().Trim());
            return clientOrRequest;
        }

        /// <summary>
        /// Sets an HTTP header to be sent with this IUrlRequest or all requests made with this IUrlClient.
        /// </summary>
        /// <param name="clientOrRequest">The IUrlClient or IUrlRequest.</param>
        /// <param name="name">HTTP header name.</param>
        /// <param name="value">HTTP header value.</param>
        /// <returns>This IUrlClient or IUrlRequest.</returns>
        public static T WithHeader<T>(this T clientOrRequest, string name, ContentType value) where T : IHttpSettingsContainer
        {
            if (value == null)
                throw new System.ArgumentNullException(nameof(value));

            clientOrRequest.Headers.AddOrReplace(name, value.ToString());

            return clientOrRequest;
        }

        /// <summary>
        /// Sets HTTP headers based on property names/values of the provided object, or keys/values if object is a dictionary, to be sent with this IUrlRequest or all requests made with this IUrlClient.
        /// </summary>
        /// <param name="clientOrRequest">The IUrlClient or IUrlRequest.</param>
        /// <param name="headers">Names/values of HTTP headers to set. Typically an anonymous object or IDictionary.</param>
        /// <param name="replaceUnderscoreWithHyphen">If true, underscores in property names will be replaced by hyphens. Default is true.</param>
        /// <returns>This IUrlClient or IUrlRequest.</returns>
        public static T WithHeaders<T>(this T clientOrRequest, object headers, bool replaceUnderscoreWithHyphen = true) where T : IHttpSettingsContainer
        {
            if (headers == null)
                return clientOrRequest;

            // underscore replacement only applies when object properties are parsed to kv pairs
            replaceUnderscoreWithHyphen = replaceUnderscoreWithHyphen && !(headers is string) && !(headers is IEnumerable);

            foreach (var kv in headers.ToKeyValuePairs())
            {
                var key = replaceUnderscoreWithHyphen ? kv.Key.Replace("_", "-") : kv.Key;
                clientOrRequest.WithHeader(key, kv.Value);
            }

            return clientOrRequest;
        }

        /// <summary>
        /// Sets HTTP authorization header according to Basic Authentication protocol to be sent with this IUrlRequest or all requests made with this IUrlClient.
        /// </summary>
        /// <param name="clientOrRequest">The IUrlClient or IUrlRequest.</param>
        /// <param name="userName">User name of authenticating user.</param>
        /// <param name="password">Password of authenticating user.</param>
        /// <returns>This IUrlClient or IUrlRequest.</returns>
        public static T WithBasicAuth<T>(this T clientOrRequest, string userName, string password) where T : IHttpSettingsContainer
        {
            // http://stackoverflow.com/questions/14627399/setting-authorization-header-of-RestClient
            var encodedCreds = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}"));
            return clientOrRequest.WithHeader("Authorization", $"Basic {encodedCreds}");
        }

        /// <summary>
        /// Sets HTTP authorization header with acquired bearer token according to OAuth 2.0 specification to be sent with this IUrlRequest or all requests made with this IUrlClient.
        /// </summary>
        /// <param name="clientOrRequest">The IUrlClient or IUrlRequest.</param>
        /// <param name="token">The acquired bearer token to pass.</param>
        /// <returns>This IUrlClient or IUrlRequest.</returns>
        public static T WithOAuthBearerToken<T>(this T clientOrRequest, string token) where T : IHttpSettingsContainer
        {
            return clientOrRequest.WithHeader("Authorization", $"Bearer {token}");
        }
    }
}
