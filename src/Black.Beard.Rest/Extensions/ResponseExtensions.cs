using Bb.Http;
using System.Threading;
using System;

namespace Bb.Extensions
{
    /// <summary>
    /// ReceiveXXX extension methods off Task&lt;IUrlResponse&gt; that allow chaining off methods like SendAsync
    /// without the need for nested awaits.
    /// </summary>
    public static class ResponseExtensions
    {

        /// <summary>
        /// Deserializes JSON-formatted HTTP response body to object of type T. Intended to chain off an async HTTP.
        /// </summary>
        /// <typeparam name="T">A type whose structure matches the expected JSON response.</typeparam>
        /// <param name="response">The response to deserializes.</param>
        /// <param name="actionInterceptMessage">Method to intercept response if the result is not success status code.</param>
        /// <returns>A Task whose result is an object containing data in the response body.</returns>
        /// <example>x = await url.PostAsync(data).ReceiveJson&lt;T&gt;()</example>
        /// <exception cref="UrlHttpException">Condition.</exception>
        public static async Task<T?> As<T>(this Task<IUrlResponse> response, Action<IUrlResponse> actionInterceptMessage = null)
        {
            using var resp = await response.ConfigureAwait(false);
            if (resp == null)
                return default;

            if (actionInterceptMessage != null && !resp.IsSuccessStatusCode)
                actionInterceptMessage(resp);

            return await resp.GetObjectAsync<T>().ConfigureAwait(false);

        }

        ///// <summary>
        ///// Deserializes JSON-formatted HTTP response body to object of type T. Intended to chain off an async HTTP.
        ///// </summary>
        ///// <typeparam name="T">A type whose structure matches the expected JSON response.</typeparam>
        ///// <param name="response">The response to deserializes.</param>
        ///// <param name="actionInterceptMessage">Method to intercept response if the result is not success status code.</param>
        ///// <returns>A Task whose result is an object containing data in the response body.</returns>
        ///// <example>x = await url.PostAsync(data).ReceiveJson&lt;T&gt;()</example>
        ///// <exception cref="UrlHttpException">Condition.</exception>
        //public static async Task<T?> AsJson(this Task<IUrlResponse> response, Action<IUrlResponse> actionInterceptMessage = null)
        //{
        //    using var resp = await response.ConfigureAwait(false);
        //    if (resp == null)
        //        return default;

        //    if (actionInterceptMessage != null && !resp.IsSuccessStatusCode)
        //        actionInterceptMessage(resp);

        //    return await resp.GetObjectAsync<T>().ConfigureAwait(false);

        //}

        /// <summary>
        /// Returns HTTP response body as a string. Intended to chain off an async call.
        /// </summary>
        /// <param name="response">The response to deserializes.</param>
        /// <param name="actionInterceptMessage">Method to intercept response if the result is not success status code.</param>
        /// <returns>A Task whose result is the response body as a string.</returns>
        /// <example>s = await url.PostAsync(data).ReceiveString()</example>
        public static async Task<string?> AsString(this Task<IUrlResponse> response, Action<IUrlResponse> actionInterceptMessage = null)
        {
            using var resp = await response.ConfigureAwait(false);
            if (resp == null)
                return null;

            if (actionInterceptMessage != null && !resp.IsSuccessStatusCode)
                actionInterceptMessage(resp);

            return await resp.GetStringAsync().ConfigureAwait(false);

        }

        /// <summary>
        /// Returns HTTP response body as a stream. Intended to chain off an async call.
        /// </summary>
        /// <param name="response">The response to deserializes.</param>
        /// <param name="actionInterceptMessage">Method to intercept response if the result is not success status code.</param>
        /// <returns>A Task whose result is the response body as a stream.</returns>
        /// <example>stream = await url.PostAsync(data).ReceiveStream()</example>
        public static async Task<Stream?> AsStream(this Task<IUrlResponse> response, Action<IUrlResponse> actionInterceptMessage = null)
        {
            // don't wrap in a using, otherwise we'll dispose the stream too early.
            // we can dispose it if there's an error, otherwise the user is on the hook for it.
            var resp = await response.ConfigureAwait(false);
            if (resp == null)
                return null;

            if (actionInterceptMessage != null && !resp.IsSuccessStatusCode)
                actionInterceptMessage(resp);

            return await resp.GetStreamAsync().ConfigureAwait(false);

        }

        /// <summary>
        /// Returns HTTP response body as a byte array. Intended to chain off an async call.
        /// </summary>
        /// <param name="response">The response to deserializes.</param>
        /// <param name="actionInterceptMessage">Method to intercept response if the result is not success status code.</param>
        /// <returns>A Task whose result is the response body as a byte array.</returns>
        /// <example>bytes = await url.PostAsync(data).ReceiveBytes()</example>
        public static async Task<byte[]?> AsBytes(this Task<IUrlResponse> response, Action<IUrlResponse> actionInterceptMessage = null)
        {
            using var resp = await response.ConfigureAwait(false);
            if (resp == null)
                return null;

            if (actionInterceptMessage != null && !resp.IsSuccessStatusCode)
                actionInterceptMessage(resp);

            return await resp.GetBytesAsync().ConfigureAwait(false);

        }

    }
}