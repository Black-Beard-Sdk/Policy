using Bb.Analysis.DiagTraces;
using Bb.Analysis.Tools;
using Bb.Expressions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bb.Policies
{
    /// <summary>
    /// Provides a base class for building source code and managing context during policy compilation.
    /// </summary>
    /// <remarks>
    /// Sourcebuilder manages build contexts and storage for generating code during policy evaluation.
    /// It provides mechanisms for context tracking and hierarchical storage of intermediate values.
    /// </remarks>
    public class Sourcebuilder : IStoreSource
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Sourcebuilder"/> class.
        /// </summary>
        /// <param name="diagnostics">Container for diagnostic messages. Must not be null.</param>
        /// <param name="withDebug">Whether to include debug information in the generated code.</param>
        /// <remarks>
        /// This constructor initializes a new source builder with the specified diagnostics and debug settings.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when diagnostics is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var diagnostics = new ScriptDiagnostics();
        /// var builder = new Sourcebuilder(diagnostics, true);
        /// </code>
        /// </example>
        public Sourcebuilder(ScriptDiagnostics diagnostics, bool withDebug)
        {
            _diagnostics = diagnostics ?? throw new ArgumentNullException(nameof(_diagnostics));
            _withDebug = withDebug;
        }


        #region Context

        /// <summary>
        /// Gets the current build context from the top of the context stack.
        /// </summary>
        /// <remarks>
        /// This property provides access to the current build context, which contains
        /// information needed for code generation such as parameters, expressions, and source code.
        /// </remarks>
        /// <returns>
        /// The current <see cref="BuildContext"/> from the top of the stack.
        /// </returns>
        protected BuildContext BuildCtx
        {
            get => _stack.Peek();
        }

        /// <summary>
        /// Gets or sets the path of the script being processed.
        /// </summary>
        /// <remarks>
        /// This property stores the file path of the script being compiled,
        /// which is useful for diagnostics and source reference.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var builder = new Sourcebuilder(diagnostics, true);
        /// builder.ScriptPath = @"C:\Policies\access_policy.txt";
        /// </code>
        /// </example>
        public string ScriptPath { get; internal set; }

        /// <summary>
        /// Creates a new build context based on the current context and pushes it onto the context stack.
        /// </summary>
        /// <remarks>
        /// This method creates a new context that inherits properties from the current context,
        /// pushes it onto the stack, and returns a disposable wrapper that will pop the context
        /// when disposed.
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">Thrown when the context stack is empty.</exception>
        /// <returns>
        /// A <see cref="CurrentContext"/> object that will restore the previous context when disposed.
        /// </returns>
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

        /// <summary>
        /// Gets the current build context from the top of the context stack.
        /// </summary>
        /// <remarks>
        /// This method provides access to the current build context without creating a new one.
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">Thrown when the context stack is empty.</exception>
        /// <returns>
        /// The current <see cref="BuildContext"/> from the top of the stack.
        /// </returns>
        protected BuildContext CurrentCtx()
        {
            var ctx = _stack.Peek();
            return ctx;
        }

        /// <summary>
        /// Provides a disposable wrapper for a build context.
        /// </summary>
        /// <remarks>
        /// This class automatically restores the previous build context when disposed,
        /// making it easy to use build contexts in a scope-based manner.
        /// </remarks>
        protected class CurrentContext : IDisposable
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CurrentContext"/> class.
            /// </summary>
            /// <param name="act">The action to execute when this context is disposed. Must not be null.</param>
            /// <param name="current">The current build context. Must not be null.</param>
            /// <remarks>
            /// This constructor creates a new context wrapper with the specified cleanup action.
            /// </remarks>
            public CurrentContext(Action act, BuildContext current)
            {
                action = act;
                Current = current;
            }

            /// <summary>
            /// Disposes of the context and restores the previous context.
            /// </summary>
            /// <remarks>
            /// This method executes the cleanup action specified in the constructor,
            /// which typically pops the context from the stack.
            /// </remarks>
            public void Dispose()
            {
                action();
            }

            /// <summary>
            /// The action to execute when this context is disposed.
            /// </summary>
            /// <remarks>
            /// This action typically pops the current build context from the stack.
            /// </remarks>
            private Action action;

            /// <summary>
            /// Gets the build context wrapped by this instance.
            /// </summary>
            /// <remarks>
            /// This property provides access to the current build context.
            /// </remarks>
            public BuildContext Current { get; }
        }

        /// <summary>
        /// Stack of build contexts used for tracking the current compilation state.
        /// </summary>
        /// <remarks>
        /// This stack maintains a hierarchical structure of build contexts,
        /// allowing for nested processing of expressions and statements.
        /// </remarks>
        protected Stack<BuildContext> _stack = new Stack<BuildContext>();

        /// <summary>
        /// Represents a context for code generation during policy compilation.
        /// </summary>
        /// <remarks>
        /// BuildContext contains information needed for code generation such as parameters,
        /// expressions, and source code, as well as a dictionary for storing temporary values.
        /// </remarks>
        protected class BuildContext
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="BuildContext"/> class.
            /// </summary>
            /// <remarks>
            /// This constructor initializes a new build context with an empty storage dictionary.
            /// </remarks>
            public BuildContext()
            {
                _dic = new Dictionary<string, object>();
            }

            /// <summary>
            /// The parameter expression representing the method argument.
            /// </summary>
            /// <remarks>
            /// This field stores a reference to the parameter expression used in the generated code.
            /// </remarks>
            public ParameterExpression Argument;

            /// <summary>
            /// The parameter expression representing the context parameter.
            /// </summary>
            /// <remarks>
            /// This field stores a reference to the context parameter expression used in the generated code.
            /// </remarks>
            public ParameterExpression Context;

            /// <summary>
            /// The root source expression.
            /// </summary>
            /// <remarks>
            /// This field stores the expression that represents the source object being evaluated.
            /// </remarks>
            public Expression RootSource;

            /// <summary>
            /// The root target expression.
            /// </summary>
            /// <remarks>
            /// This field stores the expression that represents the target of the generated code.
            /// </remarks>
            public Expression RootTarget;

            /// <summary>
            /// The source code being generated.
            /// </summary>
            /// <remarks>
            /// This field stores the source code object that is being built during compilation.
            /// </remarks>
            public SourceCode Source;

            /// <summary>
            /// Gets or sets the kind of code being generated.
            /// </summary>
            /// <remarks>
            /// This property indicates whether the code being generated is for a constructor, method, or undefined.
            /// </remarks>
            public KindGenerating IsKindGenerating { get; internal set; }

            /// <summary>
            /// Adds a value to the context's storage dictionary.
            /// </summary>
            /// <param name="key">The key to store the value under. Must not be null.</param>
            /// <param name="value">The value to store.</param>
            /// <remarks>
            /// This method stores a value in the context's dictionary under the specified key.
            /// If a value already exists for the key, it is replaced.
            /// </remarks>
            /// <exception cref="System.ArgumentNullException">Thrown when key is null.</exception>
            public void AddInStorage(string key, object value)
            {
                _dic[key] = value;
            }

            /// <summary>
            /// Tries to get a value from the context's storage dictionary.
            /// </summary>
            /// <param name="key">The key to look up. Must not be null.</param>
            /// <param name="value">When this method returns, contains the value associated with the key, if found.</param>
            /// <returns>True if the key was found; otherwise, false.</returns>
            /// <remarks>
            /// This method attempts to retrieve a value from the context's dictionary using the specified key.
            /// </remarks>
            /// <exception cref="System.ArgumentNullException">Thrown when key is null.</exception>
            public bool TryGetInStorage(string key, out object value)
            {
                return _dic.TryGetValue(key, out value);
            }

            /// <summary>
            /// Dictionary for storing temporary values in the build context.
            /// </summary>
            /// <remarks>
            /// This dictionary stores values that need to be accessible within the current build context.
            /// </remarks>
            private Dictionary<string, object> _dic;
        }

        /// <summary>
        /// Specifies the kind of code being generated.
        /// </summary>
        /// <remarks>
        /// This enumeration indicates whether the code being generated is for a constructor, method, or undefined.
        /// </remarks>
        protected enum KindGenerating
        {
            /// <summary>
            /// The kind of code being generated is not specified.
            /// </summary>
            Undefined,

            /// <summary>
            /// The code being generated is for a constructor.
            /// </summary>
            Constructor,

            /// <summary>
            /// The code being generated is for a method.
            /// </summary>
            Method,
        }

        #endregion Context

        #region Stores

        /// <summary>
        /// Tries to get a value from the current storage.
        /// </summary>
        /// <param name="key">The key to look up. Must not be null.</param>
        /// <param name="value">When this method returns, contains the value associated with the key, if found.</param>
        /// <returns>True if the key was found; otherwise, false.</returns>
        /// <remarks>
        /// This method attempts to retrieve a value from the current store using the specified key.
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">Thrown when no storage is available.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when key is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var builder = new Sourcebuilder(diagnostics, true);
        /// // Push a new store
        /// using (var store = builder.NewStore())
        /// {
        ///     store.AddInStorage("key1", "value1");
        ///     
        ///     // Later, try to retrieve the value
        ///     if (builder.TryGetInStorage("key1", out object value))
        ///     {
        ///         Console.WriteLine($"Found value: {value}");
        ///     }
        /// }
        /// </code>
        /// </example>
        public bool TryGetInStorage(string key, out object value)
        {
            var s = _pathStorages.Peek();
            if (s == null)
                throw new InvalidOperationException("No storage found");
            return s.TryGetInStorage(key, out value);
        }

        /// <summary>
        /// Tries to get a value of a specific type from the current storage.
        /// </summary>
        /// <typeparam name="T">The type of value to retrieve.</typeparam>
        /// <param name="key">The key to look up. Must not be null.</param>
        /// <param name="value">When this method returns, contains the value associated with the key, if found and of the correct type.</param>
        /// <returns>True if the key was found and the value could be cast to the specified type; otherwise, false.</returns>
        /// <remarks>
        /// This method attempts to retrieve a value from the current store using the specified key,
        /// and casts it to the specified type if found.
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">Thrown when no storage is available.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when key is null.</exception>
        /// <exception cref="System.InvalidCastException">Thrown when the value cannot be cast to the specified type.</exception>
        /// <example>
        /// <code lang="C#">
        /// var builder = new Sourcebuilder(diagnostics, true);
        /// // Push a new store
        /// using (var store = builder.NewStore())
        /// {
        ///     store.AddInStorage("maxRetries", 3);
        ///     
        ///     // Later, try to retrieve the value as a specific type
        ///     if (builder.TryGetInStorage&lt;int&gt;("maxRetries", out int maxRetries))
        ///     {
        ///         Console.WriteLine($"Max retries: {maxRetries}");
        ///     }
        /// }
        /// </code>
        /// </example>
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
        /// Determines whether the current storage contains a value for the specified key.
        /// </summary>
        /// <param name="key">The key to check for. Must not be null.</param>
        /// <returns>True if the storage contains a value for the key; otherwise, false.</returns>
        /// <remarks>
        /// This method checks if the current store contains a value with the specified key.
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">Thrown when no storage is available.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when key is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var builder = new Sourcebuilder(diagnostics, true);
        /// // Push a new store
        /// using (var store = builder.NewStore())
        /// {
        ///     store.AddInStorage("key1", "value1");
        ///     
        ///     // Check if the key exists
        ///     if (builder.ContainsInStorage("key1"))
        ///     {
        ///         Console.WriteLine("Key exists in storage");
        ///     }
        /// }
        /// </code>
        /// </example>
        public bool ContainsInStorage(string key)
        {
            var s = _pathStorages.Peek();
            if (s == null)
                throw new InvalidOperationException("No storage found");
            return s.ContainsInStorage(key);
        }

        /// <summary>
        /// Determines whether a store is currently available.
        /// </summary>
        /// <returns>True if at least one store is available; otherwise, false.</returns>
        /// <remarks>
        /// This method checks if there are any stores available for storage operations.
        /// </remarks>
        protected bool HasCurrentStore()
        {
            return _pathStorages.Count > 0;
        }

        /// <summary>
        /// Gets the current store from the top of the stack.
        /// </summary>
        /// <returns>The current store.</returns>
        /// <remarks>
        /// This method retrieves the current store without removing it from the stack.
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">Thrown when no store is available.</exception>
        /// <returns>
        /// An <see cref="IStore"/> object representing the current store.
        /// </returns>
        protected IStore CurrentStore()
        {
            return _pathStorages.Peek();
        }

        /// <summary>
        /// Removes the top store from the stack.
        /// </summary>
        /// <remarks>
        /// This method removes the most recently pushed store from the stack.
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">Thrown when no storage is available.</exception>
        void IStoreSource.StorePop()
        {
            _pathStorages.Pop();
        }

        /// <summary>
        /// Pushes a store onto the stack.
        /// </summary>
        /// <param name="toDispose">The store to push. Must be a DisposingStorage instance.</param>
        /// <remarks>
        /// This method adds a new store to the top of the stack.
        /// </remarks>
        /// <exception cref="System.ArgumentException">Thrown when toDispose is not a DisposingStorage instance.</exception>
        void IStoreSource.StorePush(object toDispose)
        {
            _pathStorages.Push((DisposingStorage)toDispose);
        }

        /// <summary>
        /// Stores a value in the current store.
        /// </summary>
        /// <param name="key">The key to store the value under. Must not be null.</param>
        /// <param name="value">The value to store.</param>
        /// <remarks>
        /// This method adds a value to the current store under the specified key.
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">Thrown when no storage is available.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when key is null.</exception>
        protected void Store(string key, object value)
        {
            _pathStorages.Peek().AddInStorage(key, value);
        }

        /// <summary>
        /// Creates a new disposable store.
        /// </summary>
        /// <returns>A new disposable store that will be automatically added to and removed from the stack.</returns>
        /// <remarks>
        /// This method creates a new store that can be used in a using statement to automatically
        /// handle adding it to and removing it from the stack.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// protected void ProcessExpression(Expression expr)
        /// {
        ///     // Create a new store for this method's scope
        ///     using (var store = NewStore())
        ///     {
        ///         // Store values in the new store
        ///         store.AddInStorage("expression", expr);
        ///         
        ///         // Process the expression
        ///         // ...
        ///     }
        ///     // The store is automatically removed from the stack when the using block ends
        /// }
        /// </code>
        /// </example>
        /// <returns>
        /// An <see cref="IStore"/> object that will be automatically added to and removed from the stack.
        /// </returns>
        protected IStore NewStore()
        {
            return new DisposingStorage(this);
        }

        #endregion Stores

        /// <summary>
        /// Index counter used for generating unique method names.
        /// </summary>
        /// <remarks>
        /// This field stores a counter that increments with each method generated,
        /// allowing for the creation of unique method names.
        /// </remarks>
        protected int _indexMethod;

        /// <summary>
        /// Gets the diagnostics container for reporting errors and warnings.
        /// </summary>
        /// <remarks>
        /// This property provides access to the diagnostics container for reporting
        /// errors, warnings, and informational messages during source generation.
        /// </remarks>
        /// <returns>
        /// The <see cref="ScriptDiagnostics"/> container for reporting messages.
        /// </returns>
        protected ScriptDiagnostics Diagnostics { get { return _diagnostics; } }

        /// <summary>
        /// The diagnostics container for reporting errors and warnings.
        /// </summary>
        /// <remarks>
        /// This field stores the diagnostics container used for reporting issues during source generation.
        /// </remarks>
        private ScriptDiagnostics _diagnostics;

        /// <summary>
        /// Stack of storage containers for hierarchical value storage.
        /// </summary>
        /// <remarks>
        /// This field stores a stack of storage containers, allowing for hierarchical
        /// storage of values where values can be pushed and popped as scope changes.
        /// </remarks>
        private Stack<DisposingStorage> _pathStorages;

        /// <summary>
        /// Indicates whether debug information should be included in generated code.
        /// </summary>
        /// <remarks>
        /// This field determines whether the source builder should generate additional
        /// code for debugging, such as trace statements and location information.
        /// </remarks>
        protected readonly bool _withDebug;
    }
}
