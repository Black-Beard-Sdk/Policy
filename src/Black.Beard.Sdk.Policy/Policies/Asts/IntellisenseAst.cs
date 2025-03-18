using Bb.Analysis.DiagTraces;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;
using Antlr4.Runtime.Atn;
using System;

namespace Bb.Policies.Asts
{

    /// <summary>
    /// object to parse ast tree
    /// </summary>
    public struct IntellisenseAst
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="IntellisenseAst"/> struct.
        /// </summary>
        /// <param name="context">The context for intellisense.</param>
        /// <param name="item">The parse tree item.</param>
        public IntellisenseAst(IntellisenseContext context, IParseTree item)
        {
            this._context = context;
            this.NotNull = context.NotNull;
            _children = null;
            this.InError = false;
            this.IsTerminal = item is ITerminalNode;
            _item = item;
        }

        /// <summary>
        /// Gets the children of the current AST node.
        /// </summary>
        public IEnumerable<IntellisenseAst> Children
        {
            get
            {
                if (_children == null)
                    ParseTree();
                return _children;
            }
        }

        /// <summary>
        /// Parses the tree and populates the children of the current AST node.
        /// </summary>
        internal void ParseTree()
        {


            if (_item is ErrorNodeImpl e)
                AddError(e);

            else if (_item is ParserRuleContext r)
            {
                if (r.exception != null)
                    AddError(r);
            }

            else if (_item is ITerminalNode)
            {

            }

            else
            {

            }

            int c = _item.ChildCount;
            _children = new List<IntellisenseAst>(c);
            for (int i = 0; i < c; i++)
            {
                var dd = _item.GetChild(i);
                var cc = new IntellisenseAst(_context, dd);
                _children.Add(cc);
                if (cc.Type != -1)
                {
                    if (dd.ChildCount == 0)
                        cc._children = new List<IntellisenseAst>();
                }
            }
        }

        /// <summary>
        /// Gets the location of the current AST node.
        /// </summary>
        public TextLocation? Location
        {
            get
            {
                if (_location == null)
                {

                    if (this.IsTerminal)
                        _location = (_item as ITerminalNode).ToLocation();

                    else if (_item is ParserRuleContext r)
                        _location = r.ToLocation();

                    else if (_item is ErrorNodeImpl e)
                        _location = e.ToLocation();

                    else
                    {

                    }
                }

                return _location;

            }
        }

        /// <summary>
        /// Gets the type of the current AST node.
        /// </summary>
        public int Type
        {
            get
            {
                if (_item is ITerminalNode t)
                {
                    return t.Symbol.Type;
                }
                return 0;
            }
        }

        /// <summary>
        /// Gets the rule index of the current AST node.
        /// </summary>
        public int RuleIndex
        {
            get
            {
                if (_item is ParserRuleContext r)
                    return r.RuleIndex;
                return 0;
            }
        }

        /// <summary>
        /// Gets the state of the current AST node.
        /// </summary>
        public ATNState? State
        {
            get
            {
                if (_item is ParserRuleContext r)
                {
                    int stateId = r.invokingState;
                    if (stateId == -1)
                        stateId = r.exception.OffendingState;
                    return _context.Parser.Atn.states[stateId];
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the text of the current AST node.
        /// </summary>
        public string Text
        {
            get
            {

                if (this.IsTerminal)
                    _text = (_item as ITerminalNode).Symbol.Text;
                else
                    _text = _item.GetText();

                return _text;

            }
        }

        /// <summary>
        /// Gets the name of the current AST node.
        /// </summary>
        public string Name
        {
            get
            {

                if (_name == null)
                {

                    if (this.IsTerminal)
                        _name = _context.Parser.Vocabulary.GetSymbolicName((_item as ITerminalNode).Symbol.Type);

                    else if (_item is ErrorNodeImpl e)
                        _name = e.Symbol.Text;

                    else
                    {
                        var name = _item.GetType().Name;
                        if (name.EndsWith("Context"))
                            name = name.Substring(0, name.Length - 7);
                        _name = name;
                    }
                }

                return _name;

            }
        }

                /// <summary>
        /// Gets a value indicating whether the current AST node is not null.
        /// </summary>
        public bool NotNull { get; }

        /// <summary>
        /// Gets a value indicating whether the current AST node is in error.
        /// </summary>
        public bool InError { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the current AST node is a terminal node.
        /// </summary>
        public bool IsTerminal { get; private set; }

        /// <summary>
        /// Returns a string that represents the current AST node.
        /// </summary>
        /// <returns>A string that represents the current AST node.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Selects the AST nodes that match the specified name.
        /// </summary>
        /// <param name="name">The name to match.</param>
        /// <returns>A collection of matching AST nodes.</returns>
        /// <example>
        /// <code lang="C#">
        /// var ast = new IntellisenseAst(context, item);
        /// var selectedNodes = ast.Select("nodeName");
        /// </code>
        /// </example>
        public IEnumerable<IntellisenseAst> Select(string name)
        {
            var nn = name.ToLower();
            return this.Select(c => c.Name.ToLower() == nn);
        }

        /// <summary>
        /// Selects the AST nodes that match the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <returns>A collection of matching AST nodes.</returns>
        /// <example>
        /// <code lang="C#">
        /// var ast = new IntellisenseAst(context, item);
        /// var selectedNodes = ast.Select(node => node.Name == "nodeName");
        /// </code>
        /// </example>
        public IEnumerable<IntellisenseAst> Select(Predicate<IntellisenseAst> predicate)
        {

            if (predicate(this))
                yield return this;

            foreach (var item in Children)
                foreach (var t in item.Select(predicate))
                    yield return t;

        }

        /// <summary>
        /// Adds an error to the diagnostics for the specified error node.
        /// </summary>
        /// <param name="e">The error node.</param>
        /// <remarks>
        /// This method adds an error to the diagnostics for the specified error node.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when the error node is null.</exception>
        void AddError(ErrorNodeImpl e)
        {
            InError = true;
            _context.Diagnostics.AddError(e.Symbol.ToLocation(_context.ScriptPath), e.Symbol.Text,
                    $"Failed to parse script at position {e.Symbol.StartIndex}, line {e.Symbol.Line}, col {e.Symbol.Column} '{e.Symbol.Text}'"
            );
        }

        /// <summary>
        /// Adds an error to the diagnostics for the specified parser rule context.
        /// </summary>
        /// <param name="r">The parser rule context.</param>
        /// <remarks>
        /// This method adds an error to the diagnostics for the specified parser rule context.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when the parser rule context is null.</exception>
        void AddError(ParserRuleContext r)
        {
            InError = true;
            int stateId = r.invokingState;

            if (stateId == -1)
                stateId = r.exception.OffendingState;

            ATNState state = _context.Parser.Atn.states[stateId];
            string o0 = _context.Parser.RuleNames[state.ruleIndex];
            string o1 = _context.Parser.RuleNames[r.RuleIndex];

            _context.Diagnostics.AddError(r.Start.ToLocation(_context.ScriptPath), r.Start.Text, $"Failed to parse script. '{o0}' expect '{o1}'");

        }

        private IParseTree _item;
        private string _name = null;
        private string _text = null;
        private TextLocation _location = null;
        private List<IntellisenseAst> _children;
        private readonly IntellisenseContext _context;


    }

}
