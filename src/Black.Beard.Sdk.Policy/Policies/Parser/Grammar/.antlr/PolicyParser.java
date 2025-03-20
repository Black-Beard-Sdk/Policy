// Generated from f:/srcs/Policy/src/Black.Beard.Sdk.Policy/Policies/Parser/Grammar/PolicyParser.g4 by ANTLR 4.13.1
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
		TRUE=1, FALSE=2, PLUS=3, DOT=4, COLON=5, NOT=6, PARENT_LEFT=7, PARENT_RIGHT=8, 
		BRACKET_LEFT=9, BRACKET_RIGHT=10, EQUAL=11, INEQUAL=12, GREATER=13, LESSER=14, 
		GREATER_EQUAL=15, LESSER_EQUAL=16, OR=17, AND=18, COMMA=19, HAS=20, HAS_NOT=21, 
		IN=22, NOT_IN=23, ALIAS=24, POLICY=25, STRING=26, MULTI_LINE_COMMENT=27, 
		SINGLE_QUOTE_CODE_STRING=28, INT=29, ID=30, IDQUOTED=31, VARIABLE_NAME=32, 
		IDLOWCASE=33;
	public static final int
		RULE_script = 0, RULE_pair = 1, RULE_pair_alias = 2, RULE_pair_policy = 3, 
		RULE_categories = 4, RULE_array = 5, RULE_operationBoolean = 6, RULE_operationEqual = 7, 
		RULE_operationContains = 8, RULE_expression = 9, RULE_value_ref = 10, 
		RULE_source = 11, RULE_string = 12, RULE_integer = 13, RULE_alias_id = 14, 
		RULE_policy_id = 15, RULE_key_ref = 16, RULE_category = 17, RULE_boolean = 18;
	private static String[] makeRuleNames() {
		return new String[] {
			"script", "pair", "pair_alias", "pair_policy", "categories", "array", 
			"operationBoolean", "operationEqual", "operationContains", "expression", 
			"value_ref", "source", "string", "integer", "alias_id", "policy_id", 
			"key_ref", "category", "boolean"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'true'", "'false'", "'+'", "'.'", "':'", "'!'", "'('", "')'", 
			"'['", "']'", "'='", "'!='", "'>'", "'<'", "'>='", "'<='", "'|'", "'&'", 
			"','", "'has'", "'!has'", "'in'", "'!in'", "'alias'", "'policy'", null, 
			null, "'''"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "TRUE", "FALSE", "PLUS", "DOT", "COLON", "NOT", "PARENT_LEFT", 
			"PARENT_RIGHT", "BRACKET_LEFT", "BRACKET_RIGHT", "EQUAL", "INEQUAL", 
			"GREATER", "LESSER", "GREATER_EQUAL", "LESSER_EQUAL", "OR", "AND", "COMMA", 
			"HAS", "HAS_NOT", "IN", "NOT_IN", "ALIAS", "POLICY", "STRING", "MULTI_LINE_COMMENT", 
			"SINGLE_QUOTE_CODE_STRING", "INT", "ID", "IDQUOTED", "VARIABLE_NAME", 
			"IDLOWCASE"
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
			setState(41);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==ALIAS || _la==POLICY) {
				{
				{
				setState(38);
				pair();
				}
				}
				setState(43);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(44);
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
			setState(48);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ALIAS:
				enterOuterAlt(_localctx, 1);
				{
				setState(46);
				pair_alias();
				}
				break;
			case POLICY:
				enterOuterAlt(_localctx, 2);
				{
				setState(47);
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
		public Alias_idContext alias_id() {
			return getRuleContext(Alias_idContext.class,0);
		}
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
			setState(50);
			match(ALIAS);
			setState(51);
			alias_id();
			setState(52);
			match(COLON);
			setState(53);
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
		public Policy_idContext policy_id() {
			return getRuleContext(Policy_idContext.class,0);
		}
		public TerminalNode COLON() { return getToken(PolicyParser.COLON, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public CategoriesContext categories() {
			return getRuleContext(CategoriesContext.class,0);
		}
		public Pair_policyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_pair_policy; }
	}

	public final Pair_policyContext pair_policy() throws RecognitionException {
		Pair_policyContext _localctx = new Pair_policyContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_pair_policy);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(55);
			match(POLICY);
			setState(57);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==PARENT_LEFT) {
				{
				setState(56);
				categories();
				}
			}

			setState(59);
			policy_id();
			setState(60);
			match(COLON);
			setState(61);
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
	public static class CategoriesContext extends ParserRuleContext {
		public TerminalNode PARENT_LEFT() { return getToken(PolicyParser.PARENT_LEFT, 0); }
		public List<CategoryContext> category() {
			return getRuleContexts(CategoryContext.class);
		}
		public CategoryContext category(int i) {
			return getRuleContext(CategoryContext.class,i);
		}
		public TerminalNode PARENT_RIGHT() { return getToken(PolicyParser.PARENT_RIGHT, 0); }
		public List<TerminalNode> COMMA() { return getTokens(PolicyParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(PolicyParser.COMMA, i);
		}
		public CategoriesContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_categories; }
	}

	public final CategoriesContext categories() throws RecognitionException {
		CategoriesContext _localctx = new CategoriesContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_categories);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(63);
			match(PARENT_LEFT);
			setState(64);
			category();
			setState(69);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(65);
				match(COMMA);
				setState(66);
				category();
				}
				}
				setState(71);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(72);
			match(PARENT_RIGHT);
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
		public List<Value_refContext> value_ref() {
			return getRuleContexts(Value_refContext.class);
		}
		public Value_refContext value_ref(int i) {
			return getRuleContext(Value_refContext.class,i);
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
		enterRule(_localctx, 10, RULE_array);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(74);
			match(BRACKET_LEFT);
			setState(75);
			value_ref();
			setState(80);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(76);
				match(COMMA);
				setState(77);
				value_ref();
				}
				}
				setState(82);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(83);
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
		enterRule(_localctx, 12, RULE_operationBoolean);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(85);
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
		public TerminalNode GREATER() { return getToken(PolicyParser.GREATER, 0); }
		public TerminalNode LESSER() { return getToken(PolicyParser.LESSER, 0); }
		public TerminalNode GREATER_EQUAL() { return getToken(PolicyParser.GREATER_EQUAL, 0); }
		public TerminalNode LESSER_EQUAL() { return getToken(PolicyParser.LESSER_EQUAL, 0); }
		public OperationEqualContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_operationEqual; }
	}

	public final OperationEqualContext operationEqual() throws RecognitionException {
		OperationEqualContext _localctx = new OperationEqualContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_operationEqual);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(87);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 129024L) != 0)) ) {
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
		enterRule(_localctx, 16, RULE_operationContains);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(89);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 15728640L) != 0)) ) {
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
		public Key_refContext key_ref() {
			return getRuleContext(Key_refContext.class,0);
		}
		public SourceContext source() {
			return getRuleContext(SourceContext.class,0);
		}
		public TerminalNode PLUS() { return getToken(PolicyParser.PLUS, 0); }
		public OperationEqualContext operationEqual() {
			return getRuleContext(OperationEqualContext.class,0);
		}
		public Value_refContext value_ref() {
			return getRuleContext(Value_refContext.class,0);
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
		int _startState = 18;
		enterRecursionRule(_localctx, 18, RULE_expression, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(108);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ID:
			case IDQUOTED:
				{
				setState(93);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,5,_ctx) ) {
				case 1:
					{
					setState(92);
					source();
					}
					break;
				}
				setState(95);
				key_ref();
				setState(100);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,6,_ctx) ) {
				case 1:
					{
					setState(96);
					match(PLUS);
					}
					break;
				case 2:
					{
					setState(97);
					operationEqual();
					setState(98);
					value_ref();
					}
					break;
				}
				}
				break;
			case NOT:
				{
				setState(102);
				match(NOT);
				setState(103);
				expression(4);
				}
				break;
			case PARENT_LEFT:
				{
				setState(104);
				match(PARENT_LEFT);
				setState(105);
				expression(0);
				setState(106);
				match(PARENT_RIGHT);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			_ctx.stop = _input.LT(-1);
			setState(120);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,9,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(118);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,8,_ctx) ) {
					case 1:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(110);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(111);
						operationBoolean();
						setState(112);
						expression(3);
						}
						break;
					case 2:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(114);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(115);
						operationContains();
						setState(116);
						array();
						}
						break;
					}
					} 
				}
				setState(122);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,9,_ctx);
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
	public static class Value_refContext extends ParserRuleContext {
		public StringContext string() {
			return getRuleContext(StringContext.class,0);
		}
		public TerminalNode IDQUOTED() { return getToken(PolicyParser.IDQUOTED, 0); }
		public TerminalNode ID() { return getToken(PolicyParser.ID, 0); }
		public BooleanContext boolean_() {
			return getRuleContext(BooleanContext.class,0);
		}
		public IntegerContext integer() {
			return getRuleContext(IntegerContext.class,0);
		}
		public Value_refContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_value_ref; }
	}

	public final Value_refContext value_ref() throws RecognitionException {
		Value_refContext _localctx = new Value_refContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_value_ref);
		try {
			setState(128);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case STRING:
				enterOuterAlt(_localctx, 1);
				{
				setState(123);
				string();
				}
				break;
			case IDQUOTED:
				enterOuterAlt(_localctx, 2);
				{
				setState(124);
				match(IDQUOTED);
				}
				break;
			case ID:
				enterOuterAlt(_localctx, 3);
				{
				setState(125);
				match(ID);
				}
				break;
			case TRUE:
			case FALSE:
				enterOuterAlt(_localctx, 4);
				{
				setState(126);
				boolean_();
				}
				break;
			case INT:
				enterOuterAlt(_localctx, 5);
				{
				setState(127);
				integer();
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
	public static class SourceContext extends ParserRuleContext {
		public TerminalNode DOT() { return getToken(PolicyParser.DOT, 0); }
		public TerminalNode ID() { return getToken(PolicyParser.ID, 0); }
		public TerminalNode IDQUOTED() { return getToken(PolicyParser.IDQUOTED, 0); }
		public SourceContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_source; }
	}

	public final SourceContext source() throws RecognitionException {
		SourceContext _localctx = new SourceContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_source);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(130);
			_la = _input.LA(1);
			if ( !(_la==ID || _la==IDQUOTED) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			setState(131);
			match(DOT);
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
		enterRule(_localctx, 24, RULE_string);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(133);
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

	@SuppressWarnings("CheckReturnValue")
	public static class IntegerContext extends ParserRuleContext {
		public TerminalNode INT() { return getToken(PolicyParser.INT, 0); }
		public IntegerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_integer; }
	}

	public final IntegerContext integer() throws RecognitionException {
		IntegerContext _localctx = new IntegerContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_integer);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(135);
			match(INT);
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
	public static class Alias_idContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(PolicyParser.ID, 0); }
		public TerminalNode IDQUOTED() { return getToken(PolicyParser.IDQUOTED, 0); }
		public Alias_idContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_alias_id; }
	}

	public final Alias_idContext alias_id() throws RecognitionException {
		Alias_idContext _localctx = new Alias_idContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_alias_id);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(137);
			_la = _input.LA(1);
			if ( !(_la==ID || _la==IDQUOTED) ) {
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
	public static class Policy_idContext extends ParserRuleContext {
		public List<TerminalNode> ID() { return getTokens(PolicyParser.ID); }
		public TerminalNode ID(int i) {
			return getToken(PolicyParser.ID, i);
		}
		public List<TerminalNode> DOT() { return getTokens(PolicyParser.DOT); }
		public TerminalNode DOT(int i) {
			return getToken(PolicyParser.DOT, i);
		}
		public TerminalNode IDQUOTED() { return getToken(PolicyParser.IDQUOTED, 0); }
		public Policy_idContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_policy_id; }
	}

	public final Policy_idContext policy_id() throws RecognitionException {
		Policy_idContext _localctx = new Policy_idContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_policy_id);
		int _la;
		try {
			setState(148);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ID:
				enterOuterAlt(_localctx, 1);
				{
				setState(139);
				match(ID);
				setState(144);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==DOT) {
					{
					{
					setState(140);
					match(DOT);
					setState(141);
					match(ID);
					}
					}
					setState(146);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				break;
			case IDQUOTED:
				enterOuterAlt(_localctx, 2);
				{
				setState(147);
				match(IDQUOTED);
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
	public static class Key_refContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(PolicyParser.ID, 0); }
		public TerminalNode IDQUOTED() { return getToken(PolicyParser.IDQUOTED, 0); }
		public Key_refContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_key_ref; }
	}

	public final Key_refContext key_ref() throws RecognitionException {
		Key_refContext _localctx = new Key_refContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_key_ref);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(150);
			_la = _input.LA(1);
			if ( !(_la==ID || _la==IDQUOTED) ) {
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
	public static class CategoryContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(PolicyParser.ID, 0); }
		public CategoryContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_category; }
	}

	public final CategoryContext category() throws RecognitionException {
		CategoryContext _localctx = new CategoryContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_category);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(152);
			match(ID);
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
	public static class BooleanContext extends ParserRuleContext {
		public TerminalNode TRUE() { return getToken(PolicyParser.TRUE, 0); }
		public TerminalNode FALSE() { return getToken(PolicyParser.FALSE, 0); }
		public BooleanContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_boolean; }
	}

	public final BooleanContext boolean_() throws RecognitionException {
		BooleanContext _localctx = new BooleanContext(_ctx, getState());
		enterRule(_localctx, 36, RULE_boolean);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(154);
			_la = _input.LA(1);
			if ( !(_la==TRUE || _la==FALSE) ) {
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

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 9:
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
		"\u0004\u0001!\u009d\u0002\u0000\u0007\u0000\u0002\u0001\u0007\u0001\u0002"+
		"\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004\u0007\u0004\u0002"+
		"\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007\u0007\u0007\u0002"+
		"\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002\u000b\u0007\u000b\u0002"+
		"\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e\u0002\u000f\u0007\u000f"+
		"\u0002\u0010\u0007\u0010\u0002\u0011\u0007\u0011\u0002\u0012\u0007\u0012"+
		"\u0001\u0000\u0005\u0000(\b\u0000\n\u0000\f\u0000+\t\u0000\u0001\u0000"+
		"\u0001\u0000\u0001\u0001\u0001\u0001\u0003\u00011\b\u0001\u0001\u0002"+
		"\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0003\u0001\u0003"+
		"\u0003\u0003:\b\u0003\u0001\u0003\u0001\u0003\u0001\u0003\u0001\u0003"+
		"\u0001\u0004\u0001\u0004\u0001\u0004\u0001\u0004\u0005\u0004D\b\u0004"+
		"\n\u0004\f\u0004G\t\u0004\u0001\u0004\u0001\u0004\u0001\u0005\u0001\u0005"+
		"\u0001\u0005\u0001\u0005\u0005\u0005O\b\u0005\n\u0005\f\u0005R\t\u0005"+
		"\u0001\u0005\u0001\u0005\u0001\u0006\u0001\u0006\u0001\u0007\u0001\u0007"+
		"\u0001\b\u0001\b\u0001\t\u0001\t\u0003\t^\b\t\u0001\t\u0001\t\u0001\t"+
		"\u0001\t\u0001\t\u0003\te\b\t\u0001\t\u0001\t\u0001\t\u0001\t\u0001\t"+
		"\u0001\t\u0003\tm\b\t\u0001\t\u0001\t\u0001\t\u0001\t\u0001\t\u0001\t"+
		"\u0001\t\u0001\t\u0005\tw\b\t\n\t\f\tz\t\t\u0001\n\u0001\n\u0001\n\u0001"+
		"\n\u0001\n\u0003\n\u0081\b\n\u0001\u000b\u0001\u000b\u0001\u000b\u0001"+
		"\f\u0001\f\u0001\r\u0001\r\u0001\u000e\u0001\u000e\u0001\u000f\u0001\u000f"+
		"\u0001\u000f\u0005\u000f\u008f\b\u000f\n\u000f\f\u000f\u0092\t\u000f\u0001"+
		"\u000f\u0003\u000f\u0095\b\u000f\u0001\u0010\u0001\u0010\u0001\u0011\u0001"+
		"\u0011\u0001\u0012\u0001\u0012\u0001\u0012\u0000\u0001\u0012\u0013\u0000"+
		"\u0002\u0004\u0006\b\n\f\u000e\u0010\u0012\u0014\u0016\u0018\u001a\u001c"+
		"\u001e \"$\u0000\u0005\u0001\u0000\u0011\u0012\u0001\u0000\u000b\u0010"+
		"\u0001\u0000\u0014\u0017\u0001\u0000\u001e\u001f\u0001\u0000\u0001\u0002"+
		"\u009b\u0000)\u0001\u0000\u0000\u0000\u00020\u0001\u0000\u0000\u0000\u0004"+
		"2\u0001\u0000\u0000\u0000\u00067\u0001\u0000\u0000\u0000\b?\u0001\u0000"+
		"\u0000\u0000\nJ\u0001\u0000\u0000\u0000\fU\u0001\u0000\u0000\u0000\u000e"+
		"W\u0001\u0000\u0000\u0000\u0010Y\u0001\u0000\u0000\u0000\u0012l\u0001"+
		"\u0000\u0000\u0000\u0014\u0080\u0001\u0000\u0000\u0000\u0016\u0082\u0001"+
		"\u0000\u0000\u0000\u0018\u0085\u0001\u0000\u0000\u0000\u001a\u0087\u0001"+
		"\u0000\u0000\u0000\u001c\u0089\u0001\u0000\u0000\u0000\u001e\u0094\u0001"+
		"\u0000\u0000\u0000 \u0096\u0001\u0000\u0000\u0000\"\u0098\u0001\u0000"+
		"\u0000\u0000$\u009a\u0001\u0000\u0000\u0000&(\u0003\u0002\u0001\u0000"+
		"\'&\u0001\u0000\u0000\u0000(+\u0001\u0000\u0000\u0000)\'\u0001\u0000\u0000"+
		"\u0000)*\u0001\u0000\u0000\u0000*,\u0001\u0000\u0000\u0000+)\u0001\u0000"+
		"\u0000\u0000,-\u0005\u0000\u0000\u0001-\u0001\u0001\u0000\u0000\u0000"+
		".1\u0003\u0004\u0002\u0000/1\u0003\u0006\u0003\u00000.\u0001\u0000\u0000"+
		"\u00000/\u0001\u0000\u0000\u00001\u0003\u0001\u0000\u0000\u000023\u0005"+
		"\u0018\u0000\u000034\u0003\u001c\u000e\u000045\u0005\u0005\u0000\u0000"+
		"56\u0003\u0018\f\u00006\u0005\u0001\u0000\u0000\u000079\u0005\u0019\u0000"+
		"\u00008:\u0003\b\u0004\u000098\u0001\u0000\u0000\u00009:\u0001\u0000\u0000"+
		"\u0000:;\u0001\u0000\u0000\u0000;<\u0003\u001e\u000f\u0000<=\u0005\u0005"+
		"\u0000\u0000=>\u0003\u0012\t\u0000>\u0007\u0001\u0000\u0000\u0000?@\u0005"+
		"\u0007\u0000\u0000@E\u0003\"\u0011\u0000AB\u0005\u0013\u0000\u0000BD\u0003"+
		"\"\u0011\u0000CA\u0001\u0000\u0000\u0000DG\u0001\u0000\u0000\u0000EC\u0001"+
		"\u0000\u0000\u0000EF\u0001\u0000\u0000\u0000FH\u0001\u0000\u0000\u0000"+
		"GE\u0001\u0000\u0000\u0000HI\u0005\b\u0000\u0000I\t\u0001\u0000\u0000"+
		"\u0000JK\u0005\t\u0000\u0000KP\u0003\u0014\n\u0000LM\u0005\u0013\u0000"+
		"\u0000MO\u0003\u0014\n\u0000NL\u0001\u0000\u0000\u0000OR\u0001\u0000\u0000"+
		"\u0000PN\u0001\u0000\u0000\u0000PQ\u0001\u0000\u0000\u0000QS\u0001\u0000"+
		"\u0000\u0000RP\u0001\u0000\u0000\u0000ST\u0005\n\u0000\u0000T\u000b\u0001"+
		"\u0000\u0000\u0000UV\u0007\u0000\u0000\u0000V\r\u0001\u0000\u0000\u0000"+
		"WX\u0007\u0001\u0000\u0000X\u000f\u0001\u0000\u0000\u0000YZ\u0007\u0002"+
		"\u0000\u0000Z\u0011\u0001\u0000\u0000\u0000[]\u0006\t\uffff\uffff\u0000"+
		"\\^\u0003\u0016\u000b\u0000]\\\u0001\u0000\u0000\u0000]^\u0001\u0000\u0000"+
		"\u0000^_\u0001\u0000\u0000\u0000_d\u0003 \u0010\u0000`e\u0005\u0003\u0000"+
		"\u0000ab\u0003\u000e\u0007\u0000bc\u0003\u0014\n\u0000ce\u0001\u0000\u0000"+
		"\u0000d`\u0001\u0000\u0000\u0000da\u0001\u0000\u0000\u0000de\u0001\u0000"+
		"\u0000\u0000em\u0001\u0000\u0000\u0000fg\u0005\u0006\u0000\u0000gm\u0003"+
		"\u0012\t\u0004hi\u0005\u0007\u0000\u0000ij\u0003\u0012\t\u0000jk\u0005"+
		"\b\u0000\u0000km\u0001\u0000\u0000\u0000l[\u0001\u0000\u0000\u0000lf\u0001"+
		"\u0000\u0000\u0000lh\u0001\u0000\u0000\u0000mx\u0001\u0000\u0000\u0000"+
		"no\n\u0002\u0000\u0000op\u0003\f\u0006\u0000pq\u0003\u0012\t\u0003qw\u0001"+
		"\u0000\u0000\u0000rs\n\u0001\u0000\u0000st\u0003\u0010\b\u0000tu\u0003"+
		"\n\u0005\u0000uw\u0001\u0000\u0000\u0000vn\u0001\u0000\u0000\u0000vr\u0001"+
		"\u0000\u0000\u0000wz\u0001\u0000\u0000\u0000xv\u0001\u0000\u0000\u0000"+
		"xy\u0001\u0000\u0000\u0000y\u0013\u0001\u0000\u0000\u0000zx\u0001\u0000"+
		"\u0000\u0000{\u0081\u0003\u0018\f\u0000|\u0081\u0005\u001f\u0000\u0000"+
		"}\u0081\u0005\u001e\u0000\u0000~\u0081\u0003$\u0012\u0000\u007f\u0081"+
		"\u0003\u001a\r\u0000\u0080{\u0001\u0000\u0000\u0000\u0080|\u0001\u0000"+
		"\u0000\u0000\u0080}\u0001\u0000\u0000\u0000\u0080~\u0001\u0000\u0000\u0000"+
		"\u0080\u007f\u0001\u0000\u0000\u0000\u0081\u0015\u0001\u0000\u0000\u0000"+
		"\u0082\u0083\u0007\u0003\u0000\u0000\u0083\u0084\u0005\u0004\u0000\u0000"+
		"\u0084\u0017\u0001\u0000\u0000\u0000\u0085\u0086\u0005\u001a\u0000\u0000"+
		"\u0086\u0019\u0001\u0000\u0000\u0000\u0087\u0088\u0005\u001d\u0000\u0000"+
		"\u0088\u001b\u0001\u0000\u0000\u0000\u0089\u008a\u0007\u0003\u0000\u0000"+
		"\u008a\u001d\u0001\u0000\u0000\u0000\u008b\u0090\u0005\u001e\u0000\u0000"+
		"\u008c\u008d\u0005\u0004\u0000\u0000\u008d\u008f\u0005\u001e\u0000\u0000"+
		"\u008e\u008c\u0001\u0000\u0000\u0000\u008f\u0092\u0001\u0000\u0000\u0000"+
		"\u0090\u008e\u0001\u0000\u0000\u0000\u0090\u0091\u0001\u0000\u0000\u0000"+
		"\u0091\u0095\u0001\u0000\u0000\u0000\u0092\u0090\u0001\u0000\u0000\u0000"+
		"\u0093\u0095\u0005\u001f\u0000\u0000\u0094\u008b\u0001\u0000\u0000\u0000"+
		"\u0094\u0093\u0001\u0000\u0000\u0000\u0095\u001f\u0001\u0000\u0000\u0000"+
		"\u0096\u0097\u0007\u0003\u0000\u0000\u0097!\u0001\u0000\u0000\u0000\u0098"+
		"\u0099\u0005\u001e\u0000\u0000\u0099#\u0001\u0000\u0000\u0000\u009a\u009b"+
		"\u0007\u0004\u0000\u0000\u009b%\u0001\u0000\u0000\u0000\r)09EP]dlvx\u0080"+
		"\u0090\u0094";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}