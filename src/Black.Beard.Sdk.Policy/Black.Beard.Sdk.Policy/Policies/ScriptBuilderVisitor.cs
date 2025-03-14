using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Bb.Analysis.DiagTraces;
using Bb.Policies.Asts;
using Bb.Policies.Parser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

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

            var result = new PolicyContainer() { Diagnostics = _diagnostics };
            foreach (var item in pair)
            {
                var o = (Policy)item.Accept(this);
                if (o != null)
                {
                    if (!result.Add(o))
                        this.AddError(o.Location, item.GetText(), "duplicated name", _scriptPath);
                }
            }

            return result;

        }

        public override object VisitPair_alias([NotNull] PolicyParser.Pair_aliasContext context)
        {

            var id = context.ID().GetText();
            var str = context.@string();
            if (str != null)
            {
                var s = (string)str.Accept(this);
                return new PolicyVariable(id)
                {
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

            var id = context.ID().GetText();
            var expr = context.expression();
            if (expr != null)
            {

                var e = expr.Accept(this);

                return new PolicyRule(id)
                {
                    Value = (Policy)e,
                    Location = context.ToLocation()
                };

            }

            return base.VisitPair_policy(context);
        }

        public override object VisitOperationBoolean([NotNull] PolicyParser.OperationBooleanContext context)
        {
            if (context.AND() != null)
                return PolicyOperator.And;

            if (context.OR() != null)
                return PolicyOperator.Or;

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

        public override object VisitString([NotNull] PolicyParser.StringContext context)
        {

            ITerminalNode str = context.STRING();
            if (str != null)
            {
                var txt = str.GetText()?.Trim() ?? string.Empty;
                return txt;
            }

            return string.Empty;

        }

        public override object VisitExpression([NotNull] PolicyParser.ExpressionContext context)
        {

            var t = context.GetText();

            PolicyOperator opera;
            var p = context.item();
            Policy left;

            if (p != null && p.Length > 0)
            {

                left = (Policy)p[0].Accept(this);
                var optional = context.QUESTION_MARK() != null;
                left = new PolicyIdExpression((PolicyConstant)left, optional) { Location = context.ToLocation() };

                var @operator = context.operationEqual();
                if (@operator == null)
                    return left;
                
                if (p.Length > 1)
                {
                    opera = (PolicyOperator)@operator.Accept(this);
                    var right = (Policy)p[1].Accept(this);
                    return new PolicyOperationBinary(left, opera, right) { Location = context.ToLocation() };
                }

                opera = (PolicyOperator)context.operationBoolean().Accept(this);
                return new PolicyOperationUnary(left, opera) { Location = context.ToLocation() };

            }


            var e = context.expression();
            left = (Policy)e[0].Accept(this);

            if (context.PARENT_LEFT() != null)
            {
                return new PolicySubExpression((PolicyExpression)left) { Location = context.ToLocation() };
            }
            else if (context.NOT() != null)
            {
                opera = PolicyOperator.Not;
                return new PolicyOperationUnary((PolicyExpression)left, opera) { Location = context.ToLocation() };
            }

            var operaC = context.operationContains();
            if (operaC != null)
            {
                opera = (PolicyOperator)operaC.Accept(this);
                var right = (Policy)context.array().Accept(this);
                return new PolicyOperationBinary(left, opera, right) { Location = context.ToLocation() };
            }

            var o2 = context.operationBoolean();
            if (o2 != null)
            {
                opera = (PolicyOperator)o2.Accept(this);
                var right = (Policy)e[1].Accept(this);
                return new PolicyOperationBinary(left, opera, right) { Location = context.ToLocation() };
            }

            throw new NotImplementedException(context.GetText());

        }

        public override object VisitArray([NotNull] PolicyParser.ArrayContext context)
        {

            context.item();
            List<PolicyConstant> items = new List<PolicyConstant>();

            foreach (var item in context.item())
            {
                var o = (PolicyConstant)item.Accept(this);
                items.Add(o);
            }

            return new PolicyArray(items) { Location = context.ToLocation() };
        }

        public override object VisitItem([NotNull] PolicyParser.ItemContext context)
        {

            var str = context.@string();
            if (str != null)
                return new PolicyConstant((string)str.Accept(this), ConstantType.String) { Location = context.ToLocation() };

            var id = context.ID();
            if (id != null)
                return  new PolicyConstant(id.GetText(), ConstantType.Id) { Location = context.ToLocation() };

            var id2 = context.IDQUOTED();
            if (id2 != null)
                return new PolicyConstant(id2.GetText(), ConstantType.QuotedId) { Location = context.ToLocation() };

            throw new NotImplementedException(context.GetText());
        
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
        private readonly string _scriptPathDirectory;

        private CultureInfo _currentCulture;

    }

}


