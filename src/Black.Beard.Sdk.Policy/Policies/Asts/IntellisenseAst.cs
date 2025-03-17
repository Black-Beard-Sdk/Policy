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


        public IntellisenseAst(IntellisenseContext context, IParseTree item)
        {
            this._context = context;
            this.NotNull = context.NotNull;
            _children = null;
            this.InError = false;
            this.IsTerminal = item is ITerminalNode;
            _item = item;
        }


        public IEnumerable<IntellisenseAst> Children
        {
            get
            {
                if (_children == null)
                    ParseTree();
                return _children;
            }
        }


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

        public int RuleIndex
        {
            get
            {
                if (_item is ParserRuleContext r)
                    return r.RuleIndex;
                return 0;
            }
        }

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

        public bool NotNull { get; }

        public bool InError { get; private set; }
        public bool IsTerminal { get; private set; }

        public override string ToString()
        {
            return Name;
        }

        public IEnumerable<IntellisenseAst> Select(string name)
        {
            var nn = name.ToLower();
            return this.Select(c => c.Name.ToLower() == nn);
        }

        public IEnumerable<IntellisenseAst> Select(Predicate<IntellisenseAst> predicate)
        {

            if (predicate(this))
                yield return this;

            foreach (var item in Children)
                foreach (var t in item.Select(predicate))
                    yield return t;

        }


        void AddError(ErrorNodeImpl e)
        {
            InError = true;
            _context.Diagnostics.AddError(e.Symbol.ToLocation(_context.ScriptPath), e.Symbol.Text,
                    $"Failed to parse script at position {e.Symbol.StartIndex}, line {e.Symbol.Line}, col {e.Symbol.Column} '{e.Symbol.Text}'"
            );
        }

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
