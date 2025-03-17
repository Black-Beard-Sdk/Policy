using Bb.Analysis.DiagTraces;
using Bb.Analysis.Tools;
using Bb.Expressions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bb.Policies
{
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
