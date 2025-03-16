using System;

namespace Bb.Policies.Asts
{

    /// <summary>
    /// Represents a variable in a variable.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("alias {Name} : {Value}")]
    public class PolicyVariable : Policy
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        /// <param name="name">name of the variable</param>
        public PolicyVariable(string name)
        {
            Kind = PolicyKind.Variable;
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        public PolicyVariable()
        {
            Kind = PolicyKind.Variable;
        }

        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitVariable(this);
        }

        public override bool ToString(Writer writer)
        {

            writer.Append($"alias {Name} : ");

            if (Value != null)
                writer.ToString(Value);

            writer.AppendEndLine();

            return true;

        }

        public override bool HasSource()
        {
            return false;
        }

        public string Name { get; }

        public PolicyConstant Value { get; set; }
        public string Origin { get; internal set; }
    }

}
