

namespace Bb.Interceptors
{
    public class RequestMessageInterceptor : RestSharp.Interceptors.Interceptor
    {

        public RequestMessageInterceptor(Action<HttpRequestMessage> action)
        {

            this._action = action;
        }

        public override ValueTask BeforeHttpRequest(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            _action(requestMessage);
            return base.BeforeHttpRequest(requestMessage, cancellationToken);
        }

        public override async ValueTask AfterHttpRequest(HttpResponseMessage responseMessage, CancellationToken cancellationToken)
        {
            await base.AfterHttpRequest(responseMessage, cancellationToken);
        }

        private readonly Action<HttpRequestMessage> _action;

    }

}
