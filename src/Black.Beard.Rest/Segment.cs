
namespace Bb
{


    public struct Segment
    {

        public Segment(string segment)
        {
            
            if (segment == null)
                throw new System.ArgumentNullException(nameof(segment));
            this.Value = segment;

            IsVariable = segment.StartsWith("%7B") && segment.EndsWith("%7D");
            if (IsVariable)
                this.Value = segment.Substring(3, segment.Length - 6);
        }

        public bool IsVariable { get; private set; }

        public string Value { get; private set; }

        public string EncodedValue => IsVariable ? $"{{{Value}}}" : Value;

        public override string ToString()
        {
            return Value.ToString();
        }

        public void Map(string value)
        {
            if (IsVariable)
            {
                Value = value;
                IsVariable = false;
            }
            else
                throw new System.InvalidOperationException("This segment is not a variable.");
        }
    }

}