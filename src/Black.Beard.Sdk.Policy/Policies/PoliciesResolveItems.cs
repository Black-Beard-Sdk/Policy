using Bb.Policies.Asts;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bb.Policies
{

    public enum PoliciesResolveItemEnum
    {
        Default,
        Role,
        Custom,
        Claim
    }

    public class PoliciesResolveItems : IPolicyVisitor<object>
    {

        private PoliciesResolveItems()
        {
            _stack = new Stack<Context>();
            this._roles = new HashSet<string>();
            this._claims = new HashSet<string>();
        }

        public static PoliciesResolveItems Get(PolicyRule policy)
        {
            var visitor = new PoliciesResolveItems();
            visitor.VisitRule(policy);
            return visitor;
        }

        public object VisitRule(PolicyRule e)
        {
            using (var ctx = Add())
            {
                return e.Value.Accept(this);
            }
        }

        public object VisitBinaryOperation(PolicyOperationBinary e)
        {
            using (var ctx = Add())
            {
                e.Left.Accept(this);
                e.Right.Accept(this);
            }
            return null;
        }

        public object VisitId(PolicyIdExpression e)
        {

            if (e.HasSource())
            {
                Current.Current = PoliciesResolveItemEnum.Custom;
            }
            else
            {

                var l = e.Value.ToLower();
                if (l == "role" || l == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                    Current.Current = PoliciesResolveItemEnum.Role;

                else
                    Current.Current = PoliciesResolveItemEnum.Claim;
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
            switch (Current.Current)
            {
                case PoliciesResolveItemEnum.Default:
                    break;
                case PoliciesResolveItemEnum.Role:
                    _roles.Add(e.Value);
                    break;
                case PoliciesResolveItemEnum.Custom:
                    break;
                case PoliciesResolveItemEnum.Claim:
                    _claims.Add(e.Value);
                    break;
                default:
                    break;
            }

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
            return e.Left.Accept(this);
        }

        public object VisitVariable(PolicyVariable e)
        {
            return null;
        }



        public IEnumerable<string> Roles { get { return _roles; } }

        public IEnumerable<string> Claims { get { return _claims; } }


        protected Context Current => _stack.Peek();

        protected Context Add()
        {
            var c = new Context(this);
            _stack.Push(c);
            return c;
        }

        protected class Context : IDisposable
        {

            public Context(PoliciesResolveItems parent)
            {
                this._parent = parent;
            }


            public PoliciesResolveItemEnum Current { get; set; }

            public void Dispose()
            {
                _parent._stack.Pop();
            }

            private readonly PoliciesResolveItems _parent;

        }

        private readonly Stack<Context> _stack;
        private HashSet<string> _roles;
        private HashSet<string> _claims;
    }

}
