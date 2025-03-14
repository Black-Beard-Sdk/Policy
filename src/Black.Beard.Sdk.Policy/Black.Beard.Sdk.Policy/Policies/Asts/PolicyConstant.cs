using System;
using System.Net.Mime;

namespace Bb.Policies.Asts
{

    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class PolicyIdExpression : PolicyConstant
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        /// <param name="name">name of the variable</param>
        public PolicyIdExpression(PolicyConstant constant, bool optional)
            : this(constant.Value, constant.Type, optional)
        {
            Location = constant.Location;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        /// <param name="name">name of the variable</param>
        public PolicyIdExpression(string value, ConstantType type, bool optional)
            : base(value, type)
        {
            this.Kind = PolicyKind.IdExpression;
            this.Optional = optional;
        }

        public bool Optional { get; }

        public override bool ToString(Writer writer)
        {
            base.ToString(writer);
            if (Optional)
                writer.Append("?");
            return true;
        }

    }


    [System.Diagnostics.DebuggerDisplay("{Value}")]
    public class PolicyConstant : PolicyExpression
    {

        public PolicyConstant(string value, ConstantType type)
        {
            this.Kind = PolicyKind.Constant;
            this.Value = value;
            this.Type = type;
        }

        /// <summary>
        /// Get the Constant value
        /// </summary>
        public string Value { get; }

        public ConstantType Type { get; }

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
