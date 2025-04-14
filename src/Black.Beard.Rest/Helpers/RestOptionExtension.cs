using Bb.Interceptors;
using Microsoft.Extensions.Logging;
using RestSharp;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;
using Bb.Services;

namespace Bb.Helpers
{


    public static class RestOptionExtension
    {


        #region interceptors

        public static RestClientOptions InterceptRequest(this RestClientOptions self, Action<HttpRequestMessage> interceptor)
        {
            self.Interceptors.Add(new RequestMessageInterceptor(interceptor ?? throw new NullReferenceException(nameof(interceptor))));
            return self;
        }

        public static RestClientOptions InterceptResponse(this RestClientOptions self, Action<HttpResponseMessage> interceptor)
        {
            self.Interceptors.Add(new ResponseMessageInterceptor(interceptor ?? throw new NullReferenceException(nameof(interceptor))));
            return self;
        }

        public static RestClientOptions InterceptCookies(this RestClientOptions self, out Bag<List<KeyValuePair<string, IEnumerable<string>>>> bag)
        {

            var cookies = new Bag<List<KeyValuePair<string, IEnumerable<string>>>>();

            Action<HttpResponseMessage> interceptor = response =>
            {
                cookies.Value = response.Headers
                    .Where(header => header.Key.Equals("Set-Cookie", StringComparison.OrdinalIgnoreCase))
                    .Select(header => new KeyValuePair<string, IEnumerable<string>>(header.Key, header.Value))
                    .ToList();
            }
            ;

            bag = cookies;

            self.Interceptors.Add(new ResponseMessageInterceptor(interceptor ?? throw new NullReferenceException(nameof(interceptor))));

            return self;
        }

        #endregion interceptors


        #region Authentication

        public static RestClientOptions WithBasicHttp(this RestClientOptions self, string username, string password)
        {
            self.Authenticator = new HttpBasicAuthenticator(username, password);
            return self;
        }

        public static RestClientOptions WithJwt(this RestClientOptions self, string accessToken)
        {
            self.Authenticator = new JwtAuthenticator(accessToken);
            return self;
        }

        public static RestClientOptions WithAuth1(this RestClientOptions self, string consumerKey, string consumerSecret)
        {
            self.Authenticator = OAuth1Authenticator.ForRequestToken(consumerKey, consumerSecret);
            return self;
        }

        public static RestClientOptions WithOAuth2(this RestClientOptions self, string accessToken)
        {
            var authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, "Bearer");
            self.Authenticator = authenticator;
            return self;
        }

        #endregion Authentication


        #region logs

        public static RestClientOptions WithLog(this RestClientOptions self, ILogger logger = null)
        {

            var request = new LogConfiguration<HttpRequestMessage>()
                .LogStart()
                .LogAll();

            var response = new LogConfiguration<HttpResponseMessage>()
                .LogAll();

            self.Interceptors.Add(new LogInterceptor(request, response, logger));
            return self;
        }

        public static RestClientOptions WithLog(this RestClientOptions self,
            Action<LogConfiguration<HttpRequestMessage>> requestAction,
            ILogger logger = null)
        {
            var request = new LogConfiguration<HttpRequestMessage>();
            requestAction?.Invoke(request);
            var response = new LogConfiguration<HttpResponseMessage>();
            self.Interceptors.Add(new LogInterceptor(request, response, logger));
            return self;
        }

        public static RestClientOptions WithLog(this RestClientOptions self,
            Action<LogConfiguration<HttpRequestMessage>> requestAction,
            Action<LogConfiguration<HttpResponseMessage>> responseAction,
            ILogger logger = null)
        {

            var request = new LogConfiguration<HttpRequestMessage>();
            request.LogStart();
            requestAction?.Invoke(request);

            var response = new LogConfiguration<HttpResponseMessage>();
            responseAction?.Invoke(response);

            self.Interceptors.Add(new LogInterceptor(request, response, logger));
            return self;

        }

        #endregion logs


    }

}
