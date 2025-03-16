using Bb.Policies.Asts;
using System;
using System.Collections.Generic;

namespace Bb.Policies
{


    public class PoliciesCategorizer : IPolicyVisitor<object>
    {

        private PoliciesCategorizer()
        {
            _stack = new Stack<Context>();
        }

        public static PoliciesCategorizer Get(PolicyRule policy)
        {
            var visitor = new PoliciesCategorizer();
            visitor.VisitRule(policy);
            return visitor;
        }

        public object VisitRule(PolicyRule e)
        {
            using (Add())
            {
                if (!string.IsNullOrEmpty(e.InheritFrom))
                    ContainsInherit = true;
                return e.Value.Accept(this);
            }
        }

        public object VisitBinaryOperation(PolicyOperationBinary e)
        {
            using (var p = Add())
            {

                e.Left.Accept(this);
                e.Right.Accept(this);

                if ((e.Operator == PolicyOperator.Has || e.Operator == PolicyOperator.Equal))
                {

                    if (p.ContainsClaim)
                        ContainsClaim = true;

                    if (p.ContainsRole)
                        ContainsRole = true;
                }

            }

            return null;
        }

        public object VisitId(PolicyIdExpression e)
        {
            var c = Current;
            if (e.HasSource())
            {
                c.ContainsInherit = true;
            }
            else
            {

                var l = e.Value.ToLower();
                if (l == "role" || l == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                    c.ContainsRole = true;

                else
                    c.ContainsClaim = true;
            }

            return null;

        }

        public object VisitArray(PolicyArray e)
        {

            foreach (var item in e)
                item.Accept(this);

            return null;

        }

        public object VisitComment(PolicyComment e)
        {
            return null;
        }

        public object VisitConstant(PolicyConstant e)
        {
            return null;
        }

        public object VisitContainer(PolicyContainer e)
        {
            return null;
        }

        public object VisitSubExpression(PolicySubExpression e)
        {
            return e.Sub.Accept(this);
        }

        public object VisitUnaryOperation(PolicyOperationUnary e)
        {
            Current.ContainsRole = false;
            Current.ContainsClaim = false;
            return e.Left.Accept(this);
        }

        public object VisitVariable(PolicyVariable e)
        {
            return null;
        }

        public bool ContainsOnlyRole
        {
            get
            {
                return ContainsRole && !ContainsClaim && !ContainsInherit;
            }
        }

        public bool ContainsOnlyClaim
        {
            get
            {
                return !ContainsRole && ContainsClaim && !ContainsInherit;
            }
        }

        public bool ContainsRole { get; private set; }

        public bool ContainsClaim { get; private set; }

        public bool ContainsInherit { get; private set; }



        protected Context Current => _stack.Peek();

        protected Context Add()
        {
            var c = new Context(this);
            _stack.Push(c);
            return c;
        }

        protected class Context : IDisposable
        {

            public Context(PoliciesCategorizer parent)
            {
                this._parent = parent;
            }


            public void Dispose()
            {
                _parent._stack.Pop();
            }


            public bool ContainsRole { get; set; }

            public bool ContainsClaim { get; set; }

            public bool ContainsInherit { get; set; }

            private readonly PoliciesCategorizer _parent;

        }

        private readonly Stack<Context> _stack;
        private HashSet<string> _roles;
        private HashSet<string> _claims;

    }

}
