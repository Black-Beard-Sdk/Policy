

namespace Bb.Interceptors
{

    public class ResponseMessageInterceptor : RestSharp.Interceptors.Interceptor
    {

        public ResponseMessageInterceptor(Action<HttpResponseMessage> action)
        {

            this._action = action;
        }

        public override async ValueTask AfterHttpRequest(HttpResponseMessage responseMessage, CancellationToken cancellationToken)
        {
            _action(responseMessage);
            await base.AfterHttpRequest(responseMessage, cancellationToken);
        }

        private readonly Action<HttpResponseMessage> _action;

    }

}
