
namespace Bb.Policies.Asts
{


    [System.Diagnostics.DebuggerDisplay("{Value}")]
    public class PolicyConstant : PolicyExpression
    {

        public PolicyConstant(string value, ConstantType type)
        {
            this.Kind = PolicyKind.Constant;

            //var o = value.Trim();
            //if (o.StartsWith("\"") && o.EndsWith("\""))
            //    o = o.Trim('"');
            //else if (o.StartsWith("'") && o.EndsWith("'"))
            //    o = o.Trim('\'');

            this.Value = value;
            this.Type = type;
        }

        /// <summary>
        /// Get the Constant value
        /// </summary>
        public string Value { get; }

        public ConstantType Type { get; }

        public override bool HasSource()
        {
            return false;
        }

        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitConstant(this);
        }


        public override bool ToString(Writer writer)
        {
         
            switch (Type)
            {
                case ConstantType.String:
                    writer.Append($"\"{Value}\"");
                    break;

                case ConstantType.QuotedId:
                    writer.Append($"'{Value}'");
                    break;

                case ConstantType.Id:
                default:
                    writer.Append(Value);
                    break;

            }
            return true;
        }

    }

    public enum ConstantType
    {
        String,
        Id,
        QuotedId,
    }

 
}
