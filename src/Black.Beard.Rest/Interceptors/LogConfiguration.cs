using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading;

namespace Bb.Interceptors
{


    public class LogConfiguration<T>
    {

        public LogConfiguration()
        {
            _rules = new List<Func<T, StringBuilder, CancellationToken, ValueTask>>();
        }

        public void AddRule(Func<T, StringBuilder, CancellationToken, ValueTask> rule)
        {
            _rules.Add(rule);
        }

        public async ValueTask Log(T message, StringBuilder logger, CancellationToken cancellationToken)
        {
            foreach (var rule in _rules)
                await rule(message, logger, cancellationToken);
        }

        public bool HasRule => _rules.Count > 0;

        private List<Func<T, StringBuilder, CancellationToken, ValueTask>> _rules;

    }

}
