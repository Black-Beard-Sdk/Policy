using Bb.Interceptors;
using System.Net.Http.Headers;
using System.Text;

namespace Bb.Helpers
{
    public static class LogConfigurationExtension
    {


        public static LogConfiguration<HttpRequestMessage> LogStart(this LogConfiguration<HttpRequestMessage> self)
        {
            self.AddRule(async (message, logger, cancellationToken) =>
            {
                logger.AppendLine("-----------------------------------------------");
                logger.AppendLine("HTTP Request: ");
                logger.AppendLine($"{message.Method} {message.RequestUri}");
                await Task.CompletedTask;
            });

            return self;
        }


        public static LogConfiguration<HttpRequestMessage> LogAll(this LogConfiguration<HttpRequestMessage> self)
        {
            self
                .LogHeader()
                .LogBody();
            return self;
        }

        public static LogConfiguration<HttpRequestMessage> LogHeader(this LogConfiguration<HttpRequestMessage> self)
        {
            self.AddRule((message, logger, cancellationToken) => LogHeader(message.Headers, logger, cancellationToken));
            return self;
        }

        public static LogConfiguration<HttpRequestMessage> LogBody(this LogConfiguration<HttpRequestMessage> self)
        {
            self.AddRule(async (message, logger, cancellationToken) => await LogBody(message.Content, logger, cancellationToken));
            return self;
        }





        public static LogConfiguration<HttpResponseMessage> LogAll(this LogConfiguration<HttpResponseMessage> self)
        {
            self
                .LogHeader()
                .LogBody();
            return self;
        }

        public static LogConfiguration<HttpResponseMessage> LogHeader(this LogConfiguration<HttpResponseMessage> self)
        {
            self.AddRule((message, logger, cancellationToken) => LogHeader(message.Headers, logger, cancellationToken));
            return self;
        }

        public static LogConfiguration<HttpResponseMessage> LogBody(this LogConfiguration<HttpResponseMessage> self)
        {
            self.AddRule(async (message, logger, cancellationToken) => await LogBody(message.Content, logger, cancellationToken));
            return self;
        }





        static async ValueTask LogHeader(this HttpHeaders self, StringBuilder logger, CancellationToken cancellationToken)
        {

            logger.AppendLine("[Headers]");
            if (self != null)
                foreach (KeyValuePair<string, IEnumerable<string>> header in self)
                    //if (header.Key != "Autorization" || (!header.Key.Contains("api") && !header.Key.Contains("key")))
                    logger.AppendLine($"{header.Key}: {string.Join(", ", header.Value)}");

            await Task.CompletedTask;

        }

        static async ValueTask LogBody(this HttpContent self, StringBuilder logger, CancellationToken cancellationToken)
        {
            logger.AppendLine("[Content]");
            if (self != null)
                try
                {
                    string content = await self.ReadAsStringAsync(cancellationToken);
                    if (!string.IsNullOrEmpty(content))
                        logger.AppendLine(content);
                }
                catch (Exception ex)
                {
                    logger.AppendLine(string.Format("Failed to read {way} body: {Message}", ex.Message));
                }
            await Task.CompletedTask;
        }


    }

}
