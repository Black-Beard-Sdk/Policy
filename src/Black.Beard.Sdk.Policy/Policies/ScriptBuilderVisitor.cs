using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Bb.Analysis.DiagTraces;
using Bb.Policies.Asts;
using Bb.Policies.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using static System.Collections.Specialized.BitVector32;

#pragma warning disable CS3001
#pragma warning disable CS3003

namespace Bb.Policies
{


    /// <summary>
    /// Generate tree of script from a Policy script text
    /// </summary>
    public class ScriptBuilderVisitor : PolicyParserBaseVisitor<object>
    {

        static ScriptBuilderVisitor()
        {

        }

        /// <summary>
        /// Create a new instance of <see cref="ScriptBuilderVisitor"/>
        /// </summary>
        /// <param name="culture"></param>
        public ScriptBuilderVisitor(PolicyParser parser, ScriptDiagnostics diagnostics, PolicyContainer container, Action<PolicyRule> action, string path)
        {
            _parser = parser;
            _diagnostics = diagnostics;
            _scriptPath = path;
            _container = container;
            _action = action;
            if (!string.IsNullOrEmpty(path))
                _scriptPathDirectory = new FileInfo(path).Directory.FullName;
            else
                _scriptPathDirectory = AppDomain.CurrentDomain.BaseDirectory;

        }


        /// <summary>
        /// Create a new instance of <see cref="ScriptBuilderVisitor"/>
        /// </summary>
        /// <param name="culture"></param>
        public ScriptBuilderVisitor(PolicyParser parser, ScriptDiagnostics diagnostics, string path)
        {
            _parser = parser;
            _diagnostics = diagnostics;
            _scriptPath = path;

            if (!string.IsNullOrEmpty(path))
                _scriptPathDirectory = new FileInfo(path).Directory.FullName;
            else
                _scriptPathDirectory = AppDomain.CurrentDomain.BaseDirectory;

        }

        /// <summary>
        /// Parse tree produced by <see cref="PolicyParser.script"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitScript([NotNull] PolicyParser.ScriptContext context)
        {
            _initialSource = new StringBuilder(context.Start.InputStream.ToString());

            var pair = context.pair();

            if (_container == null)
                _container = new PolicyContainer() { Diagnostics = _diagnostics };

            foreach (var item in pair)
            {
                var o = (Policy)item.Accept(this);
                if (o != null)
                {

                    if (_action != null && o is PolicyRule r1)
                        _action(r1);

                    if (!_container.Add(o))
                    {

                        string name = o.ToString();

                        if (o is PolicyRule r)
                            name = "policy " + r.Name;

                        else if (o is PolicyVariable s)
                            name = "alias " + s.Name;

                        this.AddError(o.Location, name, "duplicated name", _scriptPath);

                    }
                }
            }

            return _container;

        }

        public override object VisitPolicy_id([NotNull] PolicyParser.Policy_idContext context)
        {

            var id = context.ID();
            if (id != null)
                return id.GetText();

            id = context.IDQUOTED();
            if (id != null)
                return id.GetText().Trim('\'');

            return null;
        }

        public override object VisitAlias_id([NotNull] PolicyParser.Alias_idContext context)
        {
            var id = context.ID();
            if (id != null)
                return id.GetText();

            id = context.IDQUOTED();
            if (id != null)
                return id.GetText().Trim('\'');

            return null;
        }

        public override object VisitPolicy_ref([NotNull] PolicyParser.Policy_refContext context)
        {
            var id = context.ID();
            if (id != null)
                return id.GetText();

            id = context.IDQUOTED();
            if (id != null)
                return id.GetText().Trim('\'');

            return null;
        }

        public override object VisitString([NotNull] PolicyParser.StringContext context)
        {

            ITerminalNode str = context.STRING();
            if (str != null)
            {
                var txt = str.GetText()?.Trim() ?? string.Empty;
                return txt.Trim('"');
            }

            return string.Empty;

        }

        public override object VisitKey_ref([NotNull] PolicyParser.Key_refContext context)
        {

            var id = context.ID();
            if (id != null)
                return new PolicyConstant(id.GetText(), ConstantType.Id) { Location = context.ToLocation() };

            var id2 = context.IDQUOTED();
            if (id2 != null)
                return new PolicyConstant(id2.GetText().Trim('\''), ConstantType.QuotedId) { Location = context.ToLocation() };

            throw new NotImplementedException(context.GetText());

        }

        public override object VisitValue_ref([NotNull] PolicyParser.Value_refContext context)
        {
         
            var str = context.@string();
            if (str != null)
                return new PolicyConstant((string)str.Accept(this), ConstantType.String) { Location = context.ToLocation() };
            
            var id = context.ID();
            if (id != null)
                return new PolicyConstant(id.GetText(), ConstantType.Id) { Location = context.ToLocation() };

            var id2 = context.IDQUOTED();
            if (id2 != null)
                return new PolicyConstant(id2.GetText().Trim('\''), ConstantType.QuotedId) { Location = context.ToLocation() };

            throw new NotImplementedException(context.GetText());

        }

        public override object VisitSource([NotNull] PolicyParser.SourceContext context)
        {

            var id = context.ID();
            if (id != null)
                return id.GetText();

            id = context.IDQUOTED();
            if (id != null)
                return id.GetText().Trim('\'');

            return null;
            
        }

        public override object VisitPair_alias([NotNull] PolicyParser.Pair_aliasContext context)
        {

            var alias_id = context.alias_id();
            if (alias_id == null)
                return null;

            var _id = (string)context.alias_id().Accept(this);
            var str = context.@string();
            if (str != null)
            {
                var s = (string)str.Accept(this);
                return new PolicyVariable(_id)
                {
                    Origin = _scriptPath,
                    Value = new PolicyConstant(s, ConstantType.String)
                    {
                        Location = str.ToLocation()
                    },
                    Location = context.ToLocation()
                };

            }

            return null;
        }

        public override object VisitPair_policy([NotNull] PolicyParser.Pair_policyContext context)
        {

            var policy_id = context.policy_id();
            if (policy_id == null)
                return null;

            string inheritFrom = string.Empty;
            var inherit = context.inherit();
            if (inherit != null)            
                inheritFrom = (string)inherit.policy_ref().Accept(this);
            
            var expr = context.expression();
            if (expr != null)
            {

                var _id = (string)policy_id.Accept(this);
                var e = expr.Accept(this);

                return new PolicyRule(_id)
                {
                    Value = (Policy)e,
                    Location = context.ToLocation(),
                    InheritFrom = inheritFrom,
                    Origin = _scriptPath
                };

            }

            return base.VisitPair_policy(context);
        }

        public override object VisitOperationBoolean([NotNull] PolicyParser.OperationBooleanContext context)
        {

            if (context.AND() != null)
                return PolicyOperator.AndExclusive;

            if (context.OR() != null)
                return PolicyOperator.OrExclusive;

            throw new NotImplementedException(context.GetText());
        }

        public override object VisitOperationContains([NotNull] PolicyParser.OperationContainsContext context)
        {

            if (context.IN() != null)
                return PolicyOperator.In;

            if (context.NOT_IN() != null)
                return PolicyOperator.NotIn;

            if (context.HAS() != null)
                return PolicyOperator.Has;

            if (context.HAS_NOT() != null)
                return PolicyOperator.HasNot;

            throw new NotImplementedException(context.GetText());
        }

        public override object VisitOperationEqual([NotNull] PolicyParser.OperationEqualContext context)
        {

            if (context.EQUAL() != null)
                return PolicyOperator.Equal;

            if (context.INEQUAL() != null)
                return PolicyOperator.NotEqual;

            throw new NotImplementedException(context.GetText());

        }       

        public override object VisitExpression([NotNull] PolicyParser.ExpressionContext context)
        {

            PolicyOperator _operator;
            var key_ref = context.key_ref();
            Policy left;

            if (key_ref != null)
            {
                string source = string.Empty;
                var l = context.source();
                if (l != null)
                    source = l.ID().GetText();

                left = (Policy)key_ref.Accept(this);                
                left = new PolicyIdExpression((PolicyConstant)left) { Location = context.ToLocation(), Source = source };

                var @operator = context.operationEqual();
                if (@operator == null)
                    return left;

                var value_ref = context.value_ref();

                if (value_ref != null)
                {
                    _operator = (PolicyOperator)@operator.Accept(this);
                    var right = (Policy)value_ref.Accept(this);
                    return new PolicyOperationBinary(left, _operator, right) { Location = context.ToLocation() };
                }

                _operator = (PolicyOperator)context.operationBoolean().Accept(this);
                return new PolicyOperationUnary(left, _operator) { Location = context.ToLocation() };

            }

            var e = context.expression();
            left = (Policy)e[0].Accept(this);

            if (context.PARENT_LEFT() != null)
                return new PolicySubExpression((PolicyExpression)left) { Location = context.ToLocation() };
            
            else if (context.NOT() != null)
            {
                _operator = PolicyOperator.Not;
                return new PolicyOperationUnary((PolicyExpression)left, _operator) { Location = context.ToLocation() };
            }

            var operaC = context.operationContains();
            if (operaC != null)
            {
                _operator = (PolicyOperator)operaC.Accept(this);
                var right = (Policy)context.array().Accept(this);
                return new PolicyOperationBinary(left, _operator, right) { Location = context.ToLocation() };
            }

            var o2 = context.operationBoolean();
            if (o2 != null)
            {
                _operator = (PolicyOperator)o2.Accept(this);
                var right = (Policy)e[1].Accept(this);
                return new PolicyOperationBinary(left, _operator, right) { Location = context.ToLocation() };
            }

            throw new NotImplementedException(context.GetText());

        }

        public override object VisitArray([NotNull] PolicyParser.ArrayContext context)
        {

            var values = context.value_ref();
            if (values == null)
                return null;

            List<PolicyConstant> items = new List<PolicyConstant>();

            foreach (var item in values)
            {
                var o = (PolicyConstant)item.Accept(this);
                items.Add(o);
            }

            return new PolicyArray(items) { Location = context.ToLocation() };
        }        


        public void EvaluateErrors(IParseTree item)
        {

            if (item != null)
            {

                if (item is ErrorNodeImpl e)
                    AddError(e);

                else if (item is ParserRuleContext r)
                {

                    if (r.exception != null)
                    {
                        AddError(r);
                    }

                }

                int c = item.ChildCount;
                for (int i = 0; i < c; i++)
                {
                    IParseTree child = item.GetChild(i);
                    EvaluateErrors(child);
                }

            }

        }

        public override object Visit(IParseTree tree)
        {
            EvaluateErrors(tree);
            //if (this._diagnostics.Count > 0)
            //    LocalDebug.Stop();
            var result = base.Visit(tree);

            return result;

        }

        public IEnumerable<ScriptDiagnostic> Errors { get => _diagnostics; }

        public string Filename { get; set; }

        public CultureInfo Culture { get => _currentCulture; }


        void AddError(TextLocation start, string txt, string message, string path = null)
        {
            _diagnostics.AddError(start.InDocument(path ?? Filename), txt, message);
        }

        void AddWarning(TextLocation start, string txt, string message, string path = null)
        {
            _diagnostics.AddWarning(start.InDocument(path ?? Filename), txt, message);
        }

        void AddError(ParserRuleContext r)
        {

            int stateId = r.invokingState;

            if (stateId == -1)
                stateId = r.exception.OffendingState;

            ATNState state = _parser.Atn.states[stateId];
            string o0 = _parser.RuleNames[state.ruleIndex];
            string o1 = _parser.RuleNames[r.RuleIndex];

            _diagnostics.AddError(r.Start.ToLocation(Filename), r.Start.Text, $"Failed to parse script. '{o0}' expect '{o1}'");

        }

        void AddError(ErrorNodeImpl e)
        {
            _diagnostics.AddError(e.Symbol.ToLocation(Filename), e.Symbol.Text,
                    $"Failed to parse script at position {e.Symbol.StartIndex}, line {e.Symbol.Line}, col {e.Symbol.Column} '{e.Symbol.Text}'"
            );
        }

        private StringBuilder _initialSource;
        private readonly PolicyParser _parser;
        private ScriptDiagnostics _diagnostics;
        private readonly string _scriptPath;
        private PolicyContainer _container;
        private readonly Action<PolicyRule> _action;
        private readonly string _scriptPathDirectory;

        private CultureInfo _currentCulture;

    }

}


