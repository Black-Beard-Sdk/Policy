using Bb.Analysis.DiagTraces;
using Bb.Analysis.Tools;
using Bb.Expressions;
using Bb.Policies.Asts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bb.Policies
{

    internal class PoliciesParserBuilder : Sourcebuilder, IPolicyVisitor<object>
    {

        public PoliciesParserBuilder(ScriptDiagnostics diagnostics, PolicyEvaluator evaluator, bool withDebug)
            : base(diagnostics, withDebug)
        {
            _evaluator = evaluator;
        }

        public void Evaluate(PolicyContainer container)
        {
            _container = container;
            _container.Accept(this);
        }

        public object VisitContainer(PolicyContainer e)
        {

            if (e.Diagnostics.InError)
                throw new InvalidOperationException("rules are in error");

            if (_container.DefaultRule != null)
                Build(_container.DefaultRule);

            if (_container.FallbackRule != null)
                Build(_container.FallbackRule);

            foreach (var item in _container.Rules)
                Build(item);

            return null;

        }

        private void Build(PolicyRule item)
        {

            PrepareToBuild();

            var s = _stack.Peek();

            using (CurrentContext current_ctx = NewContext())
            {

                // Start parsing
                var rule = item.Value.Accept(this) as Expression;

                if (!string.IsNullOrEmpty(item.InheritFrom))
                {
                    var ctx = BuildCtx;
                    var p = item.Location.AsConstant();
                    var r = Expression.Call(ctx.Context, RuntimeContext._evaluateFrom, item.InheritFrom.AsConstant(), p);
                    rule = Expression.AndAlso(rule, r);
                }

                _compiler.Add(rule);
                var result = _compiler.Compile<Func<RuntimeContext, bool>>();

                if (rule != null)
                {
                    _evaluator.Add(item.Name, result);

                }
                else
                {

                }

            }
        }

        private void PrepareToBuild()
        {
            _compiler = new LocalMethodCompiler(_withDebug)
            {
                // OutputPath = path,
            };

            _pathStorages = new Stack<DisposingStorage>();
            PrivatedIndex.Reset();
            //_resultReset = new List<MethodCallExpression>();
            _indexMethod = 0;

            var Context = _compiler.AddParameter(typeof(RuntimeContext), "argContext");
            //var Argument = _compiler.AddParameter(typeof(object), "argDatas");

            _stack.Clear();
            _stack.Push(new BuildContext()
            {
                Argument = Context,
                Context = Context,
                RootSource = Context,
                RootTarget = null,
                Source = _compiler

            });
        }

        public object VisitBinaryOperation(PolicyOperationBinary e)
        {

            var ctx = BuildCtx;

            var left = (Expression)e.Left.Accept(this);
            var right = (Expression)e.Right.Accept(this);

            var p = e.Location.AsConstant();

            if (e.Left.HasSource())
                return Expression.Call(ctx.Context, RuntimeContext._evaluate, left, e.Operator.AsConstant(), right, p);

            switch (e.Operator)
            {

                case PolicyOperator.Equal:
                    return Expression.Call(ctx.Context, RuntimeContext._evaluateEquality, left, right, p);
                case PolicyOperator.NotEqual:
                    return Expression.Call(ctx.Context, RuntimeContext._evaluateNotEquality, left, right, p);
                case PolicyOperator.In:
                    return Expression.Call(ctx.Context, RuntimeContext._evaluateIn, left, right, p);
                case PolicyOperator.NotIn:
                    return Expression.Call(ctx.Context, RuntimeContext._evaluateNotIn, left, right, p);
                case PolicyOperator.Has:
                    return Expression.Call(ctx.Context, RuntimeContext._evaluateHas, left, right, p);
                case PolicyOperator.HasNot:
                    return Expression.Call(ctx.Context, RuntimeContext._evaluateHasNot, left, right, p);

                case PolicyOperator.AndExclusive:
                    return Expression.AndAlso(left, right);
                case PolicyOperator.OrExclusive:
                    return Expression.OrElse(left, right);


                case PolicyOperator.Undefined:
                case PolicyOperator.Not:
                default:
                    throw new NotImplementedException();
            }

            throw new NotImplementedException();

        }


        public object VisitId(PolicyIdExpression e)
        {

            var result = (Expression)VisitConstant(e);

            if (!string.IsNullOrEmpty(e.Source))
            {
                var ctx = BuildCtx;
                var p = e.Location.AsConstant();
                result = Expression.Call(ctx.Context, RuntimeContext._evaluateValue, e.Source.AsConstant(), result, p);

            }


            return result;
        }

        public object VisitConstant(PolicyConstant e)
        {

            if (this.ResolveVariable(e.Value, e.Location, out var result))
                return result.AsConstant();

            switch (e.Type)
            {
                case ConstantType.String:
                case ConstantType.Id:
                case ConstantType.QuotedId:
                case ConstantType.Boolean:
                    return e.Value.AsConstant();

                default:
                    break;
            }

            throw new NotImplementedException(e.Type.ToString());

        }

        public object VisitArray(PolicyArray e)
        {
            List<Expression> items = new List<Expression>();
            foreach (var item in e)
                items.Add((Expression)item.Accept(this));
            return Expression.NewArrayInit(typeof(string), items.ToArray());
        }

        public object VisitSubExpression(PolicySubExpression e)
        {
            return e.Sub.Accept(this);
        }

        public object VisitUnaryOperation(PolicyOperationUnary e)
        {

            var result = (Expression)e.Left.Accept(this);

            if (e.Operator == PolicyOperator.Not)
                return Expression.Not(result);

            else if (e.Operator == PolicyOperator.Required)
            {
                var ctx = BuildCtx;
                var p = e.Location.AsConstant();
                return Expression.Call(ctx.Context, RuntimeContext._evaluateUnary, result, e.Operator.AsConstant(), p);
            }
            throw new NotImplementedException(e.Operator.ToString());

        }


        public object VisitComment(PolicyComment e)
        {
            return null;
        }

        public object VisitRule(PolicyRule e)
        {
            throw new NotImplementedException();
        }

        public object VisitVariable(PolicyVariable e)
        {
            throw new NotImplementedException();
        }

        private bool ResolveVariable(string name, TextLocation location, out string alias)
        {
            return _container.ResolveVariable(name, out alias);
        }

        private LocalMethodCompiler _compiler;
        private Stack<DisposingStorage> _pathStorages;

        private PolicyContainer _container;
        private readonly PolicyEvaluator _evaluator;
    }

}
