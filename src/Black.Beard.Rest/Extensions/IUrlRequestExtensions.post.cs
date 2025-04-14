using Bb.Http;
using Bb.Util;
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
        /// Creates a UrlRequest and sends an asynchronous POST request.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="content">The request body content.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PostAsync(this Url url, HttpContent content = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PostAsync(content, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous POST request.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="body">An object representing the request body, which will be serialized to JSON.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PostAsync(this Url url, object body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PostAsync(body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous POST request.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="body">An object representing the request body, which will be serialized to JSON.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PostAsync(this Url url, QueryParamCollection body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PostAsync(body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous POST request.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="body">The request body.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PostAsync(this Url url, string body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PostAsync(body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous POST request.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="body">An object representing the request body, which will be serialized to a URL-encoded string.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PostUrlEncodedAsync(this Url url, object body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PostUrlEncodedAsync(body, completionOption, cancellationToken);
        }


    }

}
