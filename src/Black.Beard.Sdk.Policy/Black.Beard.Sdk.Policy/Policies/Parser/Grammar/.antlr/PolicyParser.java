// Generated from f:/srcs/Policy/src/Black.Beard.Sdk.Policy/Black.Beard.Sdk.Policy/Policies/Parser/Grammar/PolicyParser.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue"})
public class PolicyParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		QUESTION_MARK=1, COLON=2, NOT=3, PARENT_LEFT=4, PARENT_RIGHT=5, BRACKET_LEFT=6, 
		BRACKET_RIGHT=7, EQUAL=8, INEQUAL=9, OR=10, AND=11, COMMA=12, HAS=13, 
		HAS_NOT=14, IN=15, NOT_IN=16, ALIAS=17, POLICY=18, STRING=19, MULTI_LINE_COMMENT=20, 
		SINGLE_QUOTE_CODE_STRING=21, ID=22, IDQUOTED=23, VARIABLE_NAME=24, IDLOWCASE=25;
	public static final int
		RULE_script = 0, RULE_pair = 1, RULE_pair_alias = 2, RULE_pair_policy = 3, 
		RULE_array = 4, RULE_operationBoolean = 5, RULE_operationEqual = 6, RULE_operationContains = 7, 
		RULE_expression = 8, RULE_item = 9, RULE_string = 10;
	private static String[] makeRuleNames() {
		return new String[] {
			"script", "pair", "pair_alias", "pair_policy", "array", "operationBoolean", 
			"operationEqual", "operationContains", "expression", "item", "string"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'?'", "':'", "'!'", "'('", "')'", "'['", "']'", "'='", "'!='", 
			"'|'", "'&'", "','", null, null, "'in'", "'!in'", "'alias'", "'policy'", 
			null, null, "'''"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "QUESTION_MARK", "COLON", "NOT", "PARENT_LEFT", "PARENT_RIGHT", 
			"BRACKET_LEFT", "BRACKET_RIGHT", "EQUAL", "INEQUAL", "OR", "AND", "COMMA", 
			"HAS", "HAS_NOT", "IN", "NOT_IN", "ALIAS", "POLICY", "STRING", "MULTI_LINE_COMMENT", 
			"SINGLE_QUOTE_CODE_STRING", "ID", "IDQUOTED", "VARIABLE_NAME", "IDLOWCASE"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "PolicyParser.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public PolicyParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ScriptContext extends ParserRuleContext {
		public TerminalNode EOF() { return getToken(PolicyParser.EOF, 0); }
		public List<PairContext> pair() {
			return getRuleContexts(PairContext.class);
		}
		public PairContext pair(int i) {
			return getRuleContext(PairContext.class,i);
		}
		public ScriptContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_script; }
	}

	public final ScriptContext script() throws RecognitionException {
		ScriptContext _localctx = new ScriptContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_script);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(25);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==ALIAS || _la==POLICY) {
				{
				{
				setState(22);
				pair();
				}
				}
				setState(27);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(28);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class PairContext extends ParserRuleContext {
		public Pair_aliasContext pair_alias() {
			return getRuleContext(Pair_aliasContext.class,0);
		}
		public Pair_policyContext pair_policy() {
			return getRuleContext(Pair_policyContext.class,0);
		}
		public PairContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_pair; }
	}

	public final PairContext pair() throws RecognitionException {
		PairContext _localctx = new PairContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_pair);
		try {
			setState(32);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ALIAS:
				enterOuterAlt(_localctx, 1);
				{
				setState(30);
				pair_alias();
				}
				break;
			case POLICY:
				enterOuterAlt(_localctx, 2);
				{
				setState(31);
				pair_policy();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class Pair_aliasContext extends ParserRuleContext {
		public TerminalNode ALIAS() { return getToken(PolicyParser.ALIAS, 0); }
		public TerminalNode ID() { return getToken(PolicyParser.ID, 0); }
		public TerminalNode COLON() { return getToken(PolicyParser.COLON, 0); }
		public StringContext string() {
			return getRuleContext(StringContext.class,0);
		}
		public Pair_aliasContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_pair_alias; }
	}

	public final Pair_aliasContext pair_alias() throws RecognitionException {
		Pair_aliasContext _localctx = new Pair_aliasContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_pair_alias);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(34);
			match(ALIAS);
			setState(35);
			match(ID);
			setState(36);
			match(COLON);
			setState(37);
			string();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class Pair_policyContext extends ParserRuleContext {
		public TerminalNode POLICY() { return getToken(PolicyParser.POLICY, 0); }
		public TerminalNode ID() { return getToken(PolicyParser.ID, 0); }
		public TerminalNode COLON() { return getToken(PolicyParser.COLON, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Pair_policyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_pair_policy; }
	}

	public final Pair_policyContext pair_policy() throws RecognitionException {
		Pair_policyContext _localctx = new Pair_policyContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_pair_policy);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(39);
			match(POLICY);
			setState(40);
			match(ID);
			setState(41);
			match(COLON);
			setState(42);
			expression(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ArrayContext extends ParserRuleContext {
		public TerminalNode BRACKET_LEFT() { return getToken(PolicyParser.BRACKET_LEFT, 0); }
		public List<ItemContext> item() {
			return getRuleContexts(ItemContext.class);
		}
		public ItemContext item(int i) {
			return getRuleContext(ItemContext.class,i);
		}
		public TerminalNode BRACKET_RIGHT() { return getToken(PolicyParser.BRACKET_RIGHT, 0); }
		public List<TerminalNode> COMMA() { return getTokens(PolicyParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(PolicyParser.COMMA, i);
		}
		public ArrayContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_array; }
	}

	public final ArrayContext array() throws RecognitionException {
		ArrayContext _localctx = new ArrayContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_array);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(44);
			match(BRACKET_LEFT);
			setState(45);
			item();
			setState(50);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(46);
				match(COMMA);
				setState(47);
				item();
				}
				}
				setState(52);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(53);
			match(BRACKET_RIGHT);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class OperationBooleanContext extends ParserRuleContext {
		public TerminalNode AND() { return getToken(PolicyParser.AND, 0); }
		public TerminalNode OR() { return getToken(PolicyParser.OR, 0); }
		public OperationBooleanContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_operationBoolean; }
	}

	public final OperationBooleanContext operationBoolean() throws RecognitionException {
		OperationBooleanContext _localctx = new OperationBooleanContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_operationBoolean);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(55);
			_la = _input.LA(1);
			if ( !(_la==OR || _la==AND) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class OperationEqualContext extends ParserRuleContext {
		public TerminalNode EQUAL() { return getToken(PolicyParser.EQUAL, 0); }
		public TerminalNode INEQUAL() { return getToken(PolicyParser.INEQUAL, 0); }
		public OperationEqualContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_operationEqual; }
	}

	public final OperationEqualContext operationEqual() throws RecognitionException {
		OperationEqualContext _localctx = new OperationEqualContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_operationEqual);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(57);
			_la = _input.LA(1);
			if ( !(_la==EQUAL || _la==INEQUAL) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class OperationContainsContext extends ParserRuleContext {
		public TerminalNode IN() { return getToken(PolicyParser.IN, 0); }
		public TerminalNode NOT_IN() { return getToken(PolicyParser.NOT_IN, 0); }
		public TerminalNode HAS() { return getToken(PolicyParser.HAS, 0); }
		public TerminalNode HAS_NOT() { return getToken(PolicyParser.HAS_NOT, 0); }
		public OperationContainsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_operationContains; }
	}

	public final OperationContainsContext operationContains() throws RecognitionException {
		OperationContainsContext _localctx = new OperationContainsContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_operationContains);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(59);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 122880L) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ExpressionContext extends ParserRuleContext {
		public List<ItemContext> item() {
			return getRuleContexts(ItemContext.class);
		}
		public ItemContext item(int i) {
			return getRuleContext(ItemContext.class,i);
		}
		public TerminalNode QUESTION_MARK() { return getToken(PolicyParser.QUESTION_MARK, 0); }
		public OperationEqualContext operationEqual() {
			return getRuleContext(OperationEqualContext.class,0);
		}
		public TerminalNode NOT() { return getToken(PolicyParser.NOT, 0); }
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public TerminalNode PARENT_LEFT() { return getToken(PolicyParser.PARENT_LEFT, 0); }
		public TerminalNode PARENT_RIGHT() { return getToken(PolicyParser.PARENT_RIGHT, 0); }
		public OperationBooleanContext operationBoolean() {
			return getRuleContext(OperationBooleanContext.class,0);
		}
		public OperationContainsContext operationContains() {
			return getRuleContext(OperationContainsContext.class,0);
		}
		public ArrayContext array() {
			return getRuleContext(ArrayContext.class,0);
		}
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	}

	public final ExpressionContext expression() throws RecognitionException {
		return expression(0);
	}

	private ExpressionContext expression(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExpressionContext _localctx = new ExpressionContext(_ctx, _parentState);
		ExpressionContext _prevctx = _localctx;
		int _startState = 16;
		enterRecursionRule(_localctx, 16, RULE_expression, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(77);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case STRING:
			case ID:
			case IDQUOTED:
				{
				setState(62);
				item();
				setState(64);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,3,_ctx) ) {
				case 1:
					{
					setState(63);
					match(QUESTION_MARK);
					}
					break;
				}
				setState(69);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,4,_ctx) ) {
				case 1:
					{
					setState(66);
					operationEqual();
					setState(67);
					item();
					}
					break;
				}
				}
				break;
			case NOT:
				{
				setState(71);
				match(NOT);
				setState(72);
				expression(4);
				}
				break;
			case PARENT_LEFT:
				{
				setState(73);
				match(PARENT_LEFT);
				setState(74);
				expression(0);
				setState(75);
				match(PARENT_RIGHT);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			_ctx.stop = _input.LT(-1);
			setState(89);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,7,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(87);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,6,_ctx) ) {
					case 1:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(79);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(80);
						operationBoolean();
						setState(81);
						expression(3);
						}
						break;
					case 2:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(83);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(84);
						operationContains();
						setState(85);
						array();
						}
						break;
					}
					} 
				}
				setState(91);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,7,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ItemContext extends ParserRuleContext {
		public StringContext string() {
			return getRuleContext(StringContext.class,0);
		}
		public TerminalNode IDQUOTED() { return getToken(PolicyParser.IDQUOTED, 0); }
		public TerminalNode ID() { return getToken(PolicyParser.ID, 0); }
		public ItemContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_item; }
	}

	public final ItemContext item() throws RecognitionException {
		ItemContext _localctx = new ItemContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_item);
		try {
			setState(95);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case STRING:
				enterOuterAlt(_localctx, 1);
				{
				setState(92);
				string();
				}
				break;
			case IDQUOTED:
				enterOuterAlt(_localctx, 2);
				{
				setState(93);
				match(IDQUOTED);
				}
				break;
			case ID:
				enterOuterAlt(_localctx, 3);
				{
				setState(94);
				match(ID);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class StringContext extends ParserRuleContext {
		public TerminalNode STRING() { return getToken(PolicyParser.STRING, 0); }
		public StringContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_string; }
	}

	public final StringContext string() throws RecognitionException {
		StringContext _localctx = new StringContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_string);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(97);
			match(STRING);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 8:
			return expression_sempred((ExpressionContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean expression_sempred(ExpressionContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 2);
		case 1:
			return precpred(_ctx, 1);
		}
		return true;
	}

	public static final String _serializedATN =
		"\u0004\u0001\u0019d\u0002\u0000\u0007\u0000\u0002\u0001\u0007\u0001\u0002"+
		"\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004\u0007\u0004\u0002"+
		"\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007\u0007\u0007\u0002"+
		"\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0001\u0000\u0005\u0000\u0018"+
		"\b\u0000\n\u0000\f\u0000\u001b\t\u0000\u0001\u0000\u0001\u0000\u0001\u0001"+
		"\u0001\u0001\u0003\u0001!\b\u0001\u0001\u0002\u0001\u0002\u0001\u0002"+
		"\u0001\u0002\u0001\u0002\u0001\u0003\u0001\u0003\u0001\u0003\u0001\u0003"+
		"\u0001\u0003\u0001\u0004\u0001\u0004\u0001\u0004\u0001\u0004\u0005\u0004"+
		"1\b\u0004\n\u0004\f\u00044\t\u0004\u0001\u0004\u0001\u0004\u0001\u0005"+
		"\u0001\u0005\u0001\u0006\u0001\u0006\u0001\u0007\u0001\u0007\u0001\b\u0001"+
		"\b\u0001\b\u0003\bA\b\b\u0001\b\u0001\b\u0001\b\u0003\bF\b\b\u0001\b\u0001"+
		"\b\u0001\b\u0001\b\u0001\b\u0001\b\u0003\bN\b\b\u0001\b\u0001\b\u0001"+
		"\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0005\bX\b\b\n\b\f\b[\t\b\u0001"+
		"\t\u0001\t\u0001\t\u0003\t`\b\t\u0001\n\u0001\n\u0001\n\u0000\u0001\u0010"+
		"\u000b\u0000\u0002\u0004\u0006\b\n\f\u000e\u0010\u0012\u0014\u0000\u0003"+
		"\u0001\u0000\n\u000b\u0001\u0000\b\t\u0001\u0000\r\u0010c\u0000\u0019"+
		"\u0001\u0000\u0000\u0000\u0002 \u0001\u0000\u0000\u0000\u0004\"\u0001"+
		"\u0000\u0000\u0000\u0006\'\u0001\u0000\u0000\u0000\b,\u0001\u0000\u0000"+
		"\u0000\n7\u0001\u0000\u0000\u0000\f9\u0001\u0000\u0000\u0000\u000e;\u0001"+
		"\u0000\u0000\u0000\u0010M\u0001\u0000\u0000\u0000\u0012_\u0001\u0000\u0000"+
		"\u0000\u0014a\u0001\u0000\u0000\u0000\u0016\u0018\u0003\u0002\u0001\u0000"+
		"\u0017\u0016\u0001\u0000\u0000\u0000\u0018\u001b\u0001\u0000\u0000\u0000"+
		"\u0019\u0017\u0001\u0000\u0000\u0000\u0019\u001a\u0001\u0000\u0000\u0000"+
		"\u001a\u001c\u0001\u0000\u0000\u0000\u001b\u0019\u0001\u0000\u0000\u0000"+
		"\u001c\u001d\u0005\u0000\u0000\u0001\u001d\u0001\u0001\u0000\u0000\u0000"+
		"\u001e!\u0003\u0004\u0002\u0000\u001f!\u0003\u0006\u0003\u0000 \u001e"+
		"\u0001\u0000\u0000\u0000 \u001f\u0001\u0000\u0000\u0000!\u0003\u0001\u0000"+
		"\u0000\u0000\"#\u0005\u0011\u0000\u0000#$\u0005\u0016\u0000\u0000$%\u0005"+
		"\u0002\u0000\u0000%&\u0003\u0014\n\u0000&\u0005\u0001\u0000\u0000\u0000"+
		"\'(\u0005\u0012\u0000\u0000()\u0005\u0016\u0000\u0000)*\u0005\u0002\u0000"+
		"\u0000*+\u0003\u0010\b\u0000+\u0007\u0001\u0000\u0000\u0000,-\u0005\u0006"+
		"\u0000\u0000-2\u0003\u0012\t\u0000./\u0005\f\u0000\u0000/1\u0003\u0012"+
		"\t\u00000.\u0001\u0000\u0000\u000014\u0001\u0000\u0000\u000020\u0001\u0000"+
		"\u0000\u000023\u0001\u0000\u0000\u000035\u0001\u0000\u0000\u000042\u0001"+
		"\u0000\u0000\u000056\u0005\u0007\u0000\u00006\t\u0001\u0000\u0000\u0000"+
		"78\u0007\u0000\u0000\u00008\u000b\u0001\u0000\u0000\u00009:\u0007\u0001"+
		"\u0000\u0000:\r\u0001\u0000\u0000\u0000;<\u0007\u0002\u0000\u0000<\u000f"+
		"\u0001\u0000\u0000\u0000=>\u0006\b\uffff\uffff\u0000>@\u0003\u0012\t\u0000"+
		"?A\u0005\u0001\u0000\u0000@?\u0001\u0000\u0000\u0000@A\u0001\u0000\u0000"+
		"\u0000AE\u0001\u0000\u0000\u0000BC\u0003\f\u0006\u0000CD\u0003\u0012\t"+
		"\u0000DF\u0001\u0000\u0000\u0000EB\u0001\u0000\u0000\u0000EF\u0001\u0000"+
		"\u0000\u0000FN\u0001\u0000\u0000\u0000GH\u0005\u0003\u0000\u0000HN\u0003"+
		"\u0010\b\u0004IJ\u0005\u0004\u0000\u0000JK\u0003\u0010\b\u0000KL\u0005"+
		"\u0005\u0000\u0000LN\u0001\u0000\u0000\u0000M=\u0001\u0000\u0000\u0000"+
		"MG\u0001\u0000\u0000\u0000MI\u0001\u0000\u0000\u0000NY\u0001\u0000\u0000"+
		"\u0000OP\n\u0002\u0000\u0000PQ\u0003\n\u0005\u0000QR\u0003\u0010\b\u0003"+
		"RX\u0001\u0000\u0000\u0000ST\n\u0001\u0000\u0000TU\u0003\u000e\u0007\u0000"+
		"UV\u0003\b\u0004\u0000VX\u0001\u0000\u0000\u0000WO\u0001\u0000\u0000\u0000"+
		"WS\u0001\u0000\u0000\u0000X[\u0001\u0000\u0000\u0000YW\u0001\u0000\u0000"+
		"\u0000YZ\u0001\u0000\u0000\u0000Z\u0011\u0001\u0000\u0000\u0000[Y\u0001"+
		"\u0000\u0000\u0000\\`\u0003\u0014\n\u0000]`\u0005\u0017\u0000\u0000^`"+
		"\u0005\u0016\u0000\u0000_\\\u0001\u0000\u0000\u0000_]\u0001\u0000\u0000"+
		"\u0000_^\u0001\u0000\u0000\u0000`\u0013\u0001\u0000\u0000\u0000ab\u0005"+
		"\u0013\u0000\u0000b\u0015\u0001\u0000\u0000\u0000\t\u0019 2@EMWY_";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}