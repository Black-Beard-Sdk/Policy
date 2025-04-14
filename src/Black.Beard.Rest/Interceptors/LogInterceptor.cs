using Bb.Services;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;

namespace Bb.Interceptors
{



    public class LogInterceptor : RestSharp.Interceptors.Interceptor
    {

        public LogInterceptor(LogConfiguration<HttpRequestMessage> configurationRequest, LogConfiguration<HttpResponseMessage> configurationResponse, ILogger logger)
        {

            if (logger != null)
                CurrentLogger = logger;
            _stopwatch = new Stopwatch();

            _configurationRequest = configurationRequest;
            _configurationResponse = configurationResponse;

        }

        public override async ValueTask BeforeHttpRequest(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {

            // Log du contenu de la requête si demandé
            if (_configurationRequest.HasRule)
            {
                var sb = new StringBuilder();
                _configurationRequest.Log(requestMessage, sb, cancellationToken);
                sb.AppendLine();
                CurrentLogger.LogDebug(sb.ToString());
            }

            _stopwatch.Restart();
            await base.BeforeHttpRequest(requestMessage, cancellationToken);
        }

        public override async ValueTask AfterHttpRequest(HttpResponseMessage responseMessage, CancellationToken cancellationToken)
        {

            _stopwatch.Stop();

            // Log des informations de base de la réponse
            CurrentLogger.LogInformation("HTTP Response: {StatusCode} {ReasonPhrase} en {ElapsedMilliseconds}ms",
                (int)responseMessage.StatusCode,
                responseMessage.ReasonPhrase,
                _stopwatch.ElapsedMilliseconds);

            // Log du contenu de la réponse si demandé
            if (_configurationResponse.HasRule)
            {
                var sb = new StringBuilder();
                _configurationResponse.Log(responseMessage, sb, cancellationToken);
                CurrentLogger.LogDebug(sb.ToString());
            }
            await base.AfterHttpRequest(responseMessage, cancellationToken);
        }

        public ILogger CurrentLogger
        {
            get => _logger ?? (_logger = CreateLogger());
            set { _logger = value; }
        }

        private ILogger? CreateLogger()
        {
            return new LocalLogger("http client");
        }

        private ILogger _logger;
        private readonly Stopwatch _stopwatch;
        private readonly LogConfiguration<HttpRequestMessage> _configurationRequest;
        private readonly LogConfiguration<HttpResponseMessage> _configurationResponse;
    }

}
