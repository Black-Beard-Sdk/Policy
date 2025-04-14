using RestSharp;
using System.Net.Http;

namespace Bb.Curls
{

    public class CurlContext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CurlContext"/> class.
        /// </summary>
        /// <param name="cancellationTokenSource">The cancellation token to cancel operation.</param>
        public CurlContext(CancellationTokenSource? cancellationTokenSource)
        {
            _tokenSource = cancellationTokenSource ?? new CancellationTokenSource();
            _token = _tokenSource.Token;
            this.Headers = new Headers();
        }


        //public RestClient RestClient { get; internal set; }

        // public HttpRequestMessage RequestMessage { get; internal set; }

        public RestRequest Request { get; internal set; }

        public RestResponse Result { get; private set; }

        ///// <summary>
        ///// Send an HTTP request as an asynchronous operation.
        ///// </summary>
        ///// <returns></returns>
        //internal async Task<RestResponse> CallAsync(RestClientFactory factory)
        //{
        //    var client = factory.Cre(Request.Url);
        //    Result = await client.SendAsync(Request, HttpCompletionOption.ResponseContentRead, _token);
        //    return Result;
        //}

        public Headers Headers { get; }


        internal void Add(KeyValuePair<string, string> item)
        {
            if (_parameters.ContainsKey(item.Key))
                _parameters[item.Key] = item.Value;
            else
                _parameters.Add(item.Key, item.Value);
        }

        internal bool Has(string key, string value)
        {

            if (_parameters.TryGetValue(key, out var result))
                return result == value;

            return false;

        }

        internal CurlContext Apply(List<CurlInterpreterAction> list)
        {

            for (int i = 0; i < list.Count; i++)
                list[i].CollectParameters(this);

            for (int i = 0; i < list.Count; i++)
                list[i].Configure(this);

            return this;
        }

        private Dictionary<string, string> _parameters = new Dictionary<string, string>();
        private readonly CancellationTokenSource _tokenSource;
        private readonly CancellationToken _token;

    }

    public class Headers
    {

        public bool TryGetFirst(string header, out string result)
        {

            result = null;

            if (_headers.TryGetValue(header, out var result1))
                result = result1.FirstOrDefault();

            return result != null;

        }

        public bool Contains(string header)
        {
            return _headers.ContainsKey(header);
        }

        public void AddOrReplace(string header, string value)
        {


            if (!_headers.TryGetValue(header, out var result))
                _headers.Add(header, new HashSet<string>() { value });

            else
            {
                result.Clear();
                result.Add(value);
            }

        }

        public void Add(string header, string value)
        {

            if (!_headers.TryGetValue(header, out var result))
                _headers.Add(header, new HashSet<string>() { value });

            else
                result.Add(value);

        }


        private Dictionary<string, HashSet<string>> _headers = new Dictionary<string, HashSet<string>>();

    }

}
