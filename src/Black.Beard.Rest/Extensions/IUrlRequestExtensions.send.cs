using Bb.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Extensions
{

    public static partial class IUrlRequestExtensions
    {


        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous request.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="verb">The HTTP verb used to make the request.</param>
        /// <param name="content">The request body content.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> SendAsync(this Url url, HttpMethod verb, HttpContent content = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).SendAsync(verb, content, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous request.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="verb">The HTTP verb used to make the request.</param>
        /// <param name="body">An object representing the request body, which will be serialized to JSON.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> SendObjectAsync(this Url url, HttpMethod verb, object body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).SendObjectAsync(verb, body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous request.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="verb">The HTTP verb used to make the request.</param>
        /// <param name="body">The request body.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> SendStringAsync(this Url url, HttpMethod verb, string body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).SendStringAsync(verb, body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous request.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="verb">The HTTP verb used to make the request.</param>
        /// <param name="body">An object representing the request body, which will be serialized to a URL-encoded string.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> SendUrlEncodedAsync(this Url url, HttpMethod verb, object body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).SendUrlEncodedAsync(verb, body, completionOption, cancellationToken);
        }



    }

}
