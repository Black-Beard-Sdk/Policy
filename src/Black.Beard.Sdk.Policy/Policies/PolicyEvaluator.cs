using Bb.Analysis.DiagTraces;
using Bb.Analysis.Tools;
using Bb.Expressions;
using Bb.Policies.Asts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bb.Policies
{

    public class PolicyEvaluator
    {

        public PolicyEvaluator(PolicyContainer container, bool withDebug = false)
        {

            this._dic = new Dictionary<string, Func<RuntimeContext, bool>>();

            var e = new PoliciesParser(container.Diagnostics, this, withDebug);
            e.Evaluate(container);

        }

        public void Add(string policyName, Func<RuntimeContext, bool> rule)
        {

            if (!this._dic.ContainsKey(policyName))
                this._dic.Add(policyName, rule);

        }

        public bool Evaluate(string policy, object value, out RuntimeContext context)
        {

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (string.IsNullOrEmpty(policy))
                throw new ArgumentNullException(nameof(policy));

            context = new RuntimeContext(null, this._dic, value);

            if (_dic.TryGetValue(policy, out var rule))
                context.Result = rule(context);

            return context.Result;

        }

        private readonly Dictionary<string, Func<RuntimeContext, bool>> _dic;

    }


    internal class PoliciesParser : Sourcebuilder, IPolicyVisitor<object>
    {

        public PoliciesParser(ScriptDiagnostics diagnostics, PolicyEvaluator evaluator, bool withDebug)
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

            foreach (var item in _container.Rules)
            {

                PrepareToBuild();


                var s = _stack.Peek();
                //var s1 = Expression.Assign(Expression.Property(s.Context, RuntimeContext._ScriptProperty), Expression.Constant(ScriptPath ?? string.Empty));

                //_compiler.Add(s1);

                //using (CurrentContext ctx = NewContext()) { }

                //using (var store = NewStore())
                //{

                //}


                // Start parsing

                var rule = item.Value.Accept(this) as Expression;

                if (!string.IsNullOrEmpty(item.InheritFrom))
                {
                    var ctx = BuildCtx;
                    var p = e.Location.AsConstant();
                    var r = Expression.Call(ctx.Context, RuntimeContext._evaluateFrom, item.InheritFrom.AsConstant(), p);

                }
                else
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

            return null;

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

                case PolicyOperator.And:
                    break;
                case PolicyOperator.Or:
                    break;

                case PolicyOperator.Undefined:
                case PolicyOperator.Not:
                default:
                    throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }


        public object VisitId(PolicyIdExpression e)
        {

            if (!string.IsNullOrEmpty(e.Source))
            {

            }

            return VisitConstant(e);

        }

        public object VisitConstant(PolicyConstant e)
        {
            if (this.ResolveVariable(e.Value, e.Location, out var result))
                return result.AsConstant();
            return e.Value.AsConstant();
        }

        public object VisitArray(PolicyArray e)
        {
            List<Expression> items = new List<Expression>();
            foreach (var item in e)
                items.Add((Expression)item.Accept(this));
            return Expression.NewArrayInit(typeof(string), items.ToArray());
        }

        public object VisitComment(PolicyComment e)
        {
            return null;
        }

        public object VisitRule(PolicyRule e)
        {
            throw new NotImplementedException();
        }

        public object VisitSubExpression(PolicySubExpression e)
        {
            throw new NotImplementedException();
        }

        public object VisitUnaryOperation(PolicyOperationUnary e)
        {
            throw new NotImplementedException();
        }

        public object VisitVariable(PolicyVariable e)
        {
            throw new NotImplementedException();
        }

        private bool ResolveVariable(string name, TextLocation location, out string alias)
        {
            var result = _container.ResolveVariable(name, out alias);
            //if (!result)
            //    _container.Diagnostics.AddError(location, name, $"failed to resolve variable {name}");
            return result;
        }

        private LocalMethodCompiler _compiler;
        private Stack<DisposingStorage> _pathStorages;

        private PolicyContainer _container;
        private readonly PolicyEvaluator _evaluator;
    }


    public class Sourcebuilder : IStoreSource
    {


        public Sourcebuilder(ScriptDiagnostics diagnostics, bool withDebug)
        {
            _diagnostics = diagnostics ?? throw new ArgumentNullException(nameof(_diagnostics));
            _withDebug = withDebug;
        }



        #region Context

        protected BuildContext BuildCtx
        {
            get => _stack.Peek();
        }

        public string ScriptPath { get; internal set; }

        protected CurrentContext NewContext()
        {

            var ctx = _stack.Peek();

            var cts = new BuildContext()
            {
                Argument = ctx.Argument,
                Context = ctx.Context,
                RootSource = ctx.RootSource,
                RootTarget = ctx.RootTarget,
                Source = ctx.Source,
            };

            _stack.Push(cts);

            Action act = () =>
            {
                _stack.Pop();
            };

            return new CurrentContext(act, cts);

        }

        protected BuildContext CurrentCtx()
        {
            var ctx = _stack.Peek();
            return ctx;
        }

        protected class CurrentContext : IDisposable
        {

            public CurrentContext(Action act, BuildContext current)
            {
                action = act;
                Current = current;
            }

            public void Dispose()
            {
                action();
            }

            private Action action;

            public BuildContext Current { get; }

        }

        protected Stack<BuildContext> _stack = new Stack<BuildContext>();

        protected class BuildContext
        {

            public BuildContext()
            {
                _dic = new Dictionary<string, object>();
            }

            public ParameterExpression Argument;

            public ParameterExpression Context;

            public Expression RootSource;

            public Expression RootTarget;

            public SourceCode Source;

            public KindGenerating IsKindGenerating { get; internal set; }

            public void AddInStorage(string key, object value)
            {
                _dic[key] = value;
            }

            public bool TryGetInStorage(string key, out object value)
            {
                return _dic.TryGetValue(key, out value);
            }

            private Dictionary<string, object> _dic;

        }

        protected enum KindGenerating
        {
            Undefined,
            Constructor,
            Method,
        }

        #endregion Context

        #region Stores

        /// <summary>
        /// Get the value from the store
        /// </summary>
        /// <param name="key">key for resolve the value</param>
        /// <param name="value">value stored</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool TryGetInStorage(string key, out object value)
        {
            var s = _pathStorages.Peek();
            if (s == null)
                throw new InvalidOperationException("No storage found");
            return s.TryGetInStorage(key, out value);
        }

        /// <summary>
        /// Get the value from the store
        /// </summary>
        /// <typeparam name="T">type of value to return</typeparam>
        /// <param name="key">key for resolve the value</param>
        /// <param name="value">value stored</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool TryGetInStorage<T>(string key, out T? value)
        {

            var s = _pathStorages.Peek();
            if (s == null)
                throw new InvalidOperationException("No storage found");

            if (s.TryGetInStorage(key, out var v))
            {
                value = (T)v;
                return true;
            }

            value = default;
            return false;

        }

        /// <summary>
        /// return true if a store contains an entry for the specified key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool ContainsInStorage(string key)
        {
            var s = _pathStorages.Peek();
            if (s == null)
                throw new InvalidOperationException("No storage found");
            return s.ContainsInStorage(key);
        }

        /// <summary>
        /// return true if a store is available
        /// </summary>
        /// <returns></returns>
        protected bool HasCurrentStore()
        {
            return _pathStorages.Count > 0;
        }

        /// <summary>
        /// Get the current store
        /// </summary>
        /// <returns></returns>
        protected IStore CurrentStore()
        {
            return _pathStorages.Peek();
        }

        /// <summary>
        /// Append a new layer for storing data
        /// </summary>
        void IStoreSource.StorePop()
        {
            _pathStorages.Pop();
        }

        /// <summary>
        /// remove the last layer for storing datas
        /// </summary>
        /// <param name="toDispose"></param>
        void IStoreSource.StorePush(object toDispose)
        {
            _pathStorages.Push((DisposingStorage)toDispose);
        }

        protected void Store(string key, object value)
        {
            _pathStorages.Peek().AddInStorage(key, value);
        }

        protected IStore NewStore()
        {
            return new DisposingStorage(this);
        }

        #endregion Stores

        protected int _indexMethod;
        protected ScriptDiagnostics Diagnostics { get { return _diagnostics; } }
        private ScriptDiagnostics _diagnostics;
        private Stack<DisposingStorage> _pathStorages;
        protected readonly bool _withDebug;

    }

}
