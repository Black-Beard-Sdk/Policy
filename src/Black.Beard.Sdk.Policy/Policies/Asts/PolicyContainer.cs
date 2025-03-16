using Bb.Analysis.DiagTraces;
using System;
using System.Collections.Generic;

namespace Bb.Policies.Asts
{
    public class PolicyContainer : Policy
    {

        public PolicyContainer()
        {
            this._dicVariable = new Dictionary<string, PolicyVariable>();
            this._dicRule = new Dictionary<string, PolicyRule>();
            this.Kind = PolicyKind.Container;
        }

        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitContainer(this);
        }

        public override bool ToString(Writer writer)
        {
            
            foreach (var item in _dicVariable)
                item.Value.ToString(writer);

            foreach (var item in _dicRule)
                item.Value.ToString(writer);

            return true;

        }

        public override bool HasSource()
        {
            return false;
        }

        public bool Add(Policy o)
        {

            if (o is PolicyVariable v)
                return Add(v);

            if (o is PolicyRule r)
                return Add(r);

            return false;

        }

        public bool Add(PolicyVariable o)
        {

            if (_dicVariable.ContainsKey(o.Name))
                return false;

            _dicVariable.Add(o.Name, o);

            return true;

        }

        public bool Add(PolicyRule rule)
        {

            if (_dicRule.ContainsKey(rule.Name))
                return false;

            _dicRule.Add(rule.Name, rule);

            return true;

        }

        /// <summary>
        /// Try to resolve variable
        /// </summary>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool ResolveVariable(string name, out string alias)
        {
            
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            alias = null;

            if (_dicVariable.TryGetValue(name, out var a))
                if (a.Value != null)
                alias = a.Value.Value;

            return !string.IsNullOrEmpty( alias);

        }

        public IEnumerable<PolicyRule> Rules => _dicRule.Values;

        private Dictionary<string, PolicyVariable> _dicVariable;
        private Dictionary<string, PolicyRule> _dicRule;

        public ScriptDiagnostics Diagnostics { get; internal set; }

    }

}
