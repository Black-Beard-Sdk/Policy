
namespace Bb.Util
{
    /// <summary>
    /// Represents a query parameter value with the ability to track whether it was already encoded when created.
    /// </summary>
    public struct QueryParamValue
    {

        private readonly string _encodedValue;

        public QueryParamValue(object value, bool isEncoded)
        {

            this.HasValue = true;

            IsVariable = value is string s 
                && 
                (
                       s.StartsWith("%7B") && s.EndsWith("%7D")
                    || s.StartsWith("{") && s.EndsWith("}")
                    );

            if (isEncoded && value is string s1)
            {
                _encodedValue = s1;
                Value = Url.Decode(s1, true);
            }
            else
            {
                Value = value;
                _encodedValue = null;
            }

        }

        public bool HasValue { get; }

        public object Value { get; private set; }

        public bool IsVariable { get; private set; }

        public bool Match(string name)
        {

            if (Value is string a)
                return a == name;

            return false;

        }

        public string Encode(bool encodeSpaceAsPlus) =>
            Value == null
                ? null
                : _encodedValue != null
                    ? _encodedValue
                    : Value is string s
                        ? Url.Encode(s, encodeSpaceAsPlus)
                        : Url.Encode(Value.ToInvariantString(), encodeSpaceAsPlus);



    }
}