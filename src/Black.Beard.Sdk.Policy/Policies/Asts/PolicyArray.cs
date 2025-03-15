using System.Collections;
using System.Collections.Generic;

namespace Bb.Policies.Asts
{
    public class PolicyArray : PolicyExpression, IEnumerable<PolicyConstant>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        /// <param name="name">name of the variable</param>
        public PolicyArray(IEnumerable<PolicyConstant> items)
        {
            _items = new List<PolicyConstant>(items);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyVariable"/> class.
        /// </summary>
        /// <param name="name">name of the variable</param>
        public PolicyArray()
        {
            _items = new List<PolicyConstant>();
        }

        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitArray(this);
        }

        public override bool ToString(Writer writer)
        {

            writer.Append("[");
            bool first = true;
            foreach (var item in _items)
            {
                
                if (!first)
                    writer.Append(", ");
                first = false;

                writer.ToString(item);

            }

            writer.Append("]");

            return true;

        }

        public IEnumerator<PolicyConstant> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        private readonly List<PolicyConstant> _items;

    }

}
