
namespace Bb.Curls
{

    public class Argument
    {

        public Argument(string value, string name = null)
        {
            Value = value;
            Name = name;
        }

        public string Name { get; }

        public string Value { get; }

        public override string ToString()
        {
            return Value;
        }

    }

    public class ArgumentSource
    {

        public ArgumentSource(string[] arguments)
        {
            this._index = -1;
            _arguments = arguments;
            this._max = _arguments.Length - 1;
            this.IsFailed = false;
            this.FailMessage = null;
        }

        public string Current
        {
            get
            {
                return _arguments[_index];
            }
        }

        public bool CanRead => !IsFailed && _index < _max;

        public bool IsFailed { get; private set; }

        public string FailMessage { get; private set; }

        public bool ReadNext()
        {
            if (_index < _max)
            {
                _index++;
                return true;
            }

            return false;

        }

        public Argument GetArgument()
        {
            return new Argument(Current);
        }

        //public Argument? GetArgumentWithValue()
        //{
        //    var key = Current;
        //    if (ReadNext())
        //        Failed($"Failed to read a value for {key}");
        //    {
        //        var value = Current;
        //        return new Argument(key, value);
        //    }
        //    return null;
        //}

        internal void Failed(string failMessage)
        {
            this.IsFailed = true;
            this.FailMessage = failMessage;
        }

        private int _index;
        private readonly string[] _arguments;
        private int _max;


    }



}
