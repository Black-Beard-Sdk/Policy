using Bb.Http;

namespace Bb.Curls
{

    public partial class CurlInterpreterAction
    {

        /// <summary>
        /// create a new instance of <see cref="CurlInterpreterAction"/>
        /// </summary>
        /// <param name="configureAction"></param>
        /// <param name="arguments"></param>
        public CurlInterpreterAction(
            Action<CurlInterpreterAction,
            CurlContext> configureAction,
            params Argument[] arguments)
        {
            _parameters = new List<KeyValuePair<string, string>>();
            Arguments = arguments;
            _configureAction = configureAction;
        }

        public int Priority { get; internal set; }

        public ArgumentList Arguments { get; }

        public Argument First => Arguments.First();

        public string FirstValue => Arguments.First().Value;

        /// <summary>
        /// Get or set arguments
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Argument Get(string name) => Arguments.First(c => c.Name.Trim() == name);

        /// <summary>
        /// Get or set arguments
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public Argument Get(Func<Argument, bool> test) => Arguments.First(test);

        /// <summary>
        /// Get argument by specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exists(string name) => Arguments.Any(c => c.Name.Trim() == name);


        internal void Configure(CurlContext context)
        {
            _configureAction(this, context);
        }

        internal void CollectParameters(CurlContext context)
        {
            if (_parameters.Count > 0)
                foreach (var item in _parameters)
                    context.Add(item);
        }

        internal CurlInterpreterAction Add(string key, string value)
        {
            _parameters.Add(new KeyValuePair<string, string>(key, value));
            return this;
        }

        internal CurlInterpreterAction _next;

        private Action<CurlInterpreterAction, CurlContext> _configureAction;
        private List<KeyValuePair<string, string>> _parameters = new List<KeyValuePair<string, string>>();

    }

}
