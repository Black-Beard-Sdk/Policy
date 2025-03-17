using Bb.Analysis.DiagTraces;
using Bb.Policies.Asts;
using System;
using System.Collections.Generic;

namespace Bb.Policies
{


    public class PolicyEvaluator
    {

        public PolicyEvaluator(PolicyContainer container, bool withDebug = false)
        {

            this._dic = new Dictionary<string, Func<RuntimeContext, bool>>();

            var e = new PoliciesParserBuilder(container.Diagnostics, this, withDebug);
            e.Evaluate(container);

        }

        public void Add(string policyName, Func<RuntimeContext, bool> rule)
        {

            if (!this._dic.ContainsKey(policyName))
                this._dic.Add(policyName, rule);

        }

        public bool Evaluate(string policy, object value, out RuntimeContext context)
        {

           return Evaluate(policy, value, null, out context);

        }

        public bool Evaluate(string policy, object value, ScriptDiagnostics? diagnostic, out RuntimeContext context)
        {

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (string.IsNullOrEmpty(policy))
                throw new ArgumentNullException(nameof(policy));

            context = new RuntimeContext(diagnostic, this._dic, value);

            if (_dic.TryGetValue(policy, out var rule))
                context.Result = rule(context);

            return context.Result;

        }

        private readonly Dictionary<string, Func<RuntimeContext, bool>> _dic;

    }

}
