﻿using Bb.Analysis.DiagTraces;
using Bb.Expressions;
using Bb.Policies.Asts;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;

namespace Bb.Policies
{

    internal class PoliciesParserBuilder : SourceBuilder, IPolicyVisitor<object>
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

        public object? VisitContainer(PolicyContainer e)
        {

            if (_container == null)
                throw new InvalidOperationException("container is null");

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

            if (_compiler == null)
                throw new InvalidOperationException("compiler is null");

            var rule = item.Value ?? throw new InvalidOperationException("rule value is null");

            using (CurrentContext current_ctx = NewContext())
            {

                // Start parsing
                var ruleExpression = rule.Accept(this) as Expression;

                if (ruleExpression == null)
                    throw new InvalidOperationException("rule is null");

                if (ruleExpression.Type != typeof(bool))
                {
                    var ctx = BuildCtx;
                    var p = (rule.Location ?? TextLocation.Empty).AsConstant();
                    ruleExpression = Expression.Call(ctx.Context, RuntimeContext._evaluateconvertBool, ruleExpression, p);
                }

                if (Debugger.IsAttached)
                {
                    var txt = item.ToString();
                    var payload = ruleExpression.ToString();
                    Debug.WriteLine($"{txt.Trim()} - {payload.Trim()}");
                }

                _compiler.Add(ruleExpression);
                var result = _compiler.Compile<Func<RuntimeContext, bool>>(null);

                if (ruleExpression != null)
                {
                    _evaluator.AddPolicyRule(item.Name, result);

                }

            }
        }

        private void PrepareToBuild()
        {

#if DEBUG_EXPRESSION
            _compiler = new LocalMethodCompiler(_withDebug)
            {
                // OutputPath = path,
            };
#else
            _compiler = new LocalMethodCompiler()
            {
                // OutputPath = path,
            };
#endif
            PrivatesIndex.Reset();
            SetIndexMethod(0);
            var Context = _compiler.AddParameter(typeof(RuntimeContext), "argContext");

            ClearContexts();
            PushContext(new BuildContext()
            {
                Argument = Context,
                Context = Context,
                RootSource = Context,
                RootTarget = null,
                Source = _compiler

            });
        }

        public object? VisitBinaryOperation(PolicyOperationBinary e)
        {

            var ctx = BuildCtx;

            var left = e.Left?.Accept(this) as Expression ?? throw new InvalidDataException($"left can't be null");
            var right = e.Right?.Accept(this) as Expression ?? throw new InvalidDataException($"right can't be null");
            var p = (e.Location ?? TextLocation.Empty).AsConstant();

            if (left.Type == typeof(object) && right.Type == typeof(string))
                return Expression.Call(ctx.Context, RuntimeContext._evaluateBinary, left, e.Operator.AsConstant(), right, p);

            switch (e.Operator)
            {
                case PolicyOperator.Has:
                case PolicyOperator.HasNot:
                case PolicyOperator.NotIn:
                case PolicyOperator.In:
                    return EvaluateBinaryIn(ctx, e.Operator, left, right, p);

                case PolicyOperator.Lesser:
                case PolicyOperator.LesserOrEqual:
                case PolicyOperator.Greater:
                case PolicyOperator.GreaterOrEqual:
                    return EvaluateBinaryNumeric(ctx, e.Operator, left, right, p);

                case PolicyOperator.Equal:
                case PolicyOperator.NotEqual:
                    return EvaluateBinaryEquality(ctx, e.Operator, left, right, p);

                case PolicyOperator.AndExclusive:
                    return Expression.AndAlso(left, right);

                case PolicyOperator.OrExclusive:
                    return Expression.OrElse(left, right);

                default:
                    break;

            }

            throw new NotImplementedException();

        }

        private static MethodCallExpression EvaluateBinaryEquality(BuildContext ctx, PolicyOperator @operator, Expression left, Expression right, ConstantExpression p)
        {

            bool isNumeric = left.Type == typeof(object) && right.Type == typeof(int);
            bool isString = left.Type == typeof(string) && right.Type == typeof(string);
            var ope = @operator.AsConstant();

            switch (@operator)
            {

                case PolicyOperator.Equal:
                    if (isString)
                        return Expression.Call(ctx.Context, RuntimeContext._evaluateEquality, left, right, p);

                    else if (isNumeric)
                        return Expression.Call(ctx.Context, RuntimeContext._evaluateBinaryNumeric, left, ope, right, p);

                    break;

                case PolicyOperator.NotEqual:
                    if (isString)
                        return Expression.Call(ctx.Context, RuntimeContext._evaluateNotEquality, left, right, p);

                    else if (isNumeric)
                        return Expression.Call(ctx.Context, RuntimeContext._evaluateBinaryNumeric, left, ope, right, p);

                    break;

                case PolicyOperator.UnaryCompare:
                case PolicyOperator.Lesser:
                case PolicyOperator.LesserOrEqual:
                case PolicyOperator.Greater:
                case PolicyOperator.GreaterOrEqual:
                case PolicyOperator.In:
                case PolicyOperator.NotIn:
                case PolicyOperator.Has:
                case PolicyOperator.HasNot:
                case PolicyOperator.AndExclusive:
                case PolicyOperator.OrExclusive:
                case PolicyOperator.Not:
                case PolicyOperator.Required:
                case PolicyOperator.Undefined:
                default:
                    break;
            }

            throw new NotImplementedException(@operator.ToString());

        }

        private static MethodCallExpression EvaluateBinaryNumeric(BuildContext ctx, PolicyOperator @operator, Expression left, Expression right, ConstantExpression p)
        {

            bool isNumeric = left.Type == typeof(object) && right.Type == typeof(int);
            var ope = @operator.AsConstant();

            switch (@operator)
            {

                case PolicyOperator.Equal:
                case PolicyOperator.NotEqual:
                case PolicyOperator.Lesser:
                case PolicyOperator.LesserOrEqual:
                case PolicyOperator.Greater:
                case PolicyOperator.GreaterOrEqual:
                    if (isNumeric)
                        return Expression.Call(ctx.Context, RuntimeContext._evaluateBinaryNumeric, left, ope.AsConstant(), right, p);
                    break;

                case PolicyOperator.UnaryCompare:
                case PolicyOperator.In:
                case PolicyOperator.NotIn:
                case PolicyOperator.Has:
                case PolicyOperator.HasNot:
                case PolicyOperator.AndExclusive:
                case PolicyOperator.OrExclusive:
                case PolicyOperator.Not:
                case PolicyOperator.Required:
                case PolicyOperator.Undefined:
                default:
                    break;
            }

            throw new NotImplementedException(@operator.ToString());

        }

        private static MethodCallExpression EvaluateBinaryIn(BuildContext ctx, PolicyOperator @operator, Expression left, Expression right, ConstantExpression p)
        {

            bool isString = left.Type == typeof(string) && right.Type == typeof(string[]);
            if (isString)
                switch (@operator)
                {
                    case PolicyOperator.In:
                        return Expression.Call(ctx.Context, RuntimeContext._evaluateIn, left, right, p);
                    case PolicyOperator.NotIn:
                        return Expression.Call(ctx.Context, RuntimeContext._evaluateNotIn, left, right, p);
                    case PolicyOperator.Has:
                        return Expression.Call(ctx.Context, RuntimeContext._evaluateHas, left, right, p);
                    case PolicyOperator.HasNot:
                        return Expression.Call(ctx.Context, RuntimeContext._evaluateHasNot, left, right, p);

                    case PolicyOperator.AndExclusive:
                    case PolicyOperator.OrExclusive:
                    case PolicyOperator.Not:
                    case PolicyOperator.Required:
                    case PolicyOperator.Equal:
                    case PolicyOperator.NotEqual:
                    case PolicyOperator.Lesser:
                    case PolicyOperator.LesserOrEqual:
                    case PolicyOperator.Greater:
                    case PolicyOperator.GreaterOrEqual:
                    case PolicyOperator.UnaryCompare:
                    case PolicyOperator.Undefined:
                    default:
                        break;
                }

            throw new NotImplementedException(@operator.ToString());

        }

        public object? VisitId(PolicyIdExpression e)
        {

            var result = VisitConstant(e) as Expression ?? throw new InvalidOperationException("constant creation return null value");

            if (!string.IsNullOrEmpty(e.Source))
            {
                var ctx = BuildCtx;
                var p = (e.Location ?? TextLocation.Empty).AsConstant();
                result = Expression.Call(ctx.Context, RuntimeContext._evaluateValue, e.Source.AsConstant(), result, p);

            }


            return result;
        }

        public object? VisitConstant(PolicyConstant e)
        {

            if (this.ResolveVariable(e.Value, out var result))
            {

                if (result == null)
                    return Expression.Constant(null);

                return result.AsConstant();

            }

            switch (e.Type)
            {
                case ConstantType.Text:
                case ConstantType.Id:
                case ConstantType.QuotedId:
                    return e.Value.AsConstant();

                case ConstantType.Boolean:
                    if (e.Value == "true")
                        return Expression.Constant(true);
                    return Expression.Constant(false);

                case ConstantType.IntegerNumeric:
                    int i = int.Parse(e.Value, CultureInfo.InvariantCulture);
                    return Expression.Constant(i);

                default:
                    break;

            }

            throw new NotImplementedException(e.Type.ToString());

        }

        public object? VisitArray(PolicyArray e)
        {
            List<Expression> items = new List<Expression>();
            foreach (var item in e)
                items.Add(item.Accept(this) as Expression ?? throw new InvalidOperationException($"item {items.Count} is null"));
            return Expression.NewArrayInit(typeof(string), items.ToArray());
        }

        public object? VisitSubExpression(PolicySubExpression e)
        {
            return e.Sub.Accept(this);
        }

        public object? VisitUnaryOperation(PolicyOperationUnary e)
        {

            var result = e.Left?.Accept(this) as Expression ?? throw new InvalidOperationException($"operand is null");

            var ctx = BuildCtx;
            var p = (e.Location ?? TextLocation.Empty).AsConstant();

            if (e.Operator == PolicyOperator.Not)
                return Expression.Not(result);

            else if (e.Operator == PolicyOperator.Required)
                return Expression.Call(ctx.Context, RuntimeContext._evaluateUnary, result, e.Operator.AsConstant(), p);

            else if (e.Operator == PolicyOperator.UnaryCompare)
            {

                if (result.Type == typeof(object))
                    return Expression.Call(ctx.Context, RuntimeContext._evaluateconvertBool, result, p);

                return Expression.Call(ctx.Context, RuntimeContext._evaluateFrom, result, p);

            }

            throw new NotImplementedException(e.Operator.ToString());

        }


        public object? VisitComment(PolicyComment e)
        {
            return null;
        }

        public object? VisitRule(PolicyRule e)
        {
            throw new NotImplementedException();
        }

        public object? VisitVariable(PolicyVariable e)
        {
            throw new NotImplementedException();
        }

        private bool ResolveVariable(string name, out string? alias)
        {
            if (_container == null)
                throw new InvalidOperationException("container is null");

            return _container.ResolveVariable(name, out alias);
        }

        private LocalMethodCompiler? _compiler;

        private PolicyContainer? _container;
        private readonly PolicyEvaluator _evaluator;
    }

}
