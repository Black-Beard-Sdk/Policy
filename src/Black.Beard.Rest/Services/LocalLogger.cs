using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Bb.Services
{


    internal class LocalLogger : ILogger
    {
        
        public LocalLogger(string categoryName = "LogInterceptor")
        {
            _categoryName = categoryName;
            _writer = Console.Out;
        }

        public LocalLogger(string categoryName, TextWriter writer)
        {
            _categoryName = categoryName;
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new NoopDisposable();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // Active tous les niveaux de log par défaut
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {

            if (!IsEnabled(logLevel))
                return;

            string logLevelString = GetLogLevelString(logLevel);
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string message = formatter(state, exception);

            string logMessage = $"[{timestamp}] [{logLevelString}] [{_categoryName}]{Environment.NewLine} {message}";

            if (exception != null)
                logMessage += Environment.NewLine + exception.ToString();

            lock (_writer)
            {

                if (Trace.Listeners.Count > 0)
                {
                    
                    Trace.WriteLine(logMessage);
                    Trace.WriteLine("-----------------------------------------------");

                }

                _writer.WriteLine(logMessage);
                _writer.Flush();
            }
        }

        private string GetLogLevelString(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => "TRACE",
                LogLevel.Debug => "DEBUG",
                LogLevel.Information => "INFO ",
                LogLevel.Warning => "WARN ",
                LogLevel.Error => "ERROR",
                LogLevel.Critical => "CRIT ",
                _ => "NONE "
            };
        }

        private class NoopDisposable : IDisposable
        {
            public void Dispose() { }
        }

        private readonly string _categoryName;
        private readonly TextWriter _writer;

    }

}
