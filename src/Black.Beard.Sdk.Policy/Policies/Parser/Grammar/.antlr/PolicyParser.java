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
		DOT=1, COLON=2, NOT=3, PARENT_LEFT=4, PARENT_RIGHT=5, BRACKET_LEFT=6, 
		BRACKET_RIGHT=7, EQUAL=8, INEQUAL=9, OR=10, AND=11, COMMA=12, HAS=13, 
		HAS_NOT=14, IN=15, NOT_IN=16, ALIAS=17, POLICY=18, INHERIT=19, STRING=20, 
		MULTI_LINE_COMMENT=21, SINGLE_QUOTE_CODE_STRING=22, ID=23, IDQUOTED=24, 
		VARIABLE_NAME=25, IDLOWCASE=26;
	public static final int
		RULE_script = 0, RULE_pair = 1, RULE_pair_alias = 2, RULE_pair_policy = 3, 
		RULE_inherit = 4, RULE_array = 5, RULE_operationBoolean = 6, RULE_operationEqual = 7, 
		RULE_operationContains = 8, RULE_expression = 9, RULE_value_ref = 10, 
		RULE_source = 11, RULE_string = 12, RULE_alias_id = 13, RULE_policy_id = 14, 
		RULE_policy_ref = 15, RULE_key_ref = 16;
	private static String[] makeRuleNames() {
		return new String[] {
			"script", "pair", "pair_alias", "pair_policy", "inherit", "array", "operationBoolean", 
			"operationEqual", "operationContains", "expression", "value_ref", "source", 
			"string", "alias_id", "policy_id", "policy_ref", "key_ref"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'.'", "':'", "'!'", "'('", "')'", "'['", "']'", "'='", "'!='", 
			"'|'", "'&'", "','", "'has'", "'!has'", "'in'", "'!in'", "'alias'", "'policy'", 
			"'inherit'", null, null, "'''"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "DOT", "COLON", "NOT", "PARENT_LEFT", "PARENT_RIGHT", "BRACKET_LEFT", 
			"BRACKET_RIGHT", "EQUAL", "INEQUAL", "OR", "AND", "COMMA", "HAS", "HAS_NOT", 
			"IN", "NOT_IN", "ALIAS", "POLICY", "INHERIT", "STRING", "MULTI_LINE_COMMENT", 
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
			setState(37);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==ALIAS || _la==POLICY) {
				{
				{
				setState(34);
				pair();
				}
				}
				setState(39);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(40);
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
			setState(44);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ALIAS:
				enterOuterAlt(_localctx, 1);
				{
				setState(42);
				pair_alias();
				}
				break;
			case POLICY:
				enterOuterAlt(_localctx, 2);
				{
				setState(43);
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
			setState(46);
			match(ALIAS);
			setState(47);
			alias_id();
			setState(48);
			match(COLON);
			setState(49);
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
		public InheritContext inherit() {
			return getRuleContext(InheritContext.class,0);
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
			setState(51);
			match(POLICY);
			setState(52);
			policy_id();
			setState(54);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==INHERIT) {
				{
				setState(53);
				inherit();
				}
			}

			setState(56);
			match(COLON);
			setState(57);
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
	public static class InheritContext extends ParserRuleContext {
		public TerminalNode INHERIT() { return getToken(PolicyParser.INHERIT, 0); }
		public Policy_refContext policy_ref() {
			return getRuleContext(Policy_refContext.class,0);
		}
		public InheritContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_inherit; }
	}

	public final InheritContext inherit() throws RecognitionException {
		InheritContext _localctx = new InheritContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_inherit);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(59);
			match(INHERIT);
			setState(60);
			policy_ref();
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
			setState(62);
			match(BRACKET_LEFT);
			setState(63);
			value_ref();
			setState(68);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(64);
				match(COMMA);
				setState(65);
				value_ref();
				}
				}
				setState(70);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(71);
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
			setState(73);
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
		enterRule(_localctx, 14, RULE_operationEqual);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(75);
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
		enterRule(_localctx, 16, RULE_operationContains);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(77);
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
		public Key_refContext key_ref() {
			return getRuleContext(Key_refContext.class,0);
		}
		public SourceContext source() {
			return getRuleContext(SourceContext.class,0);
		}
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
			setState(95);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ID:
			case IDQUOTED:
				{
				setState(81);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,4,_ctx) ) {
				case 1:
					{
					setState(80);
					source();
					}
					break;
				}
				setState(83);
				key_ref();
				setState(87);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,5,_ctx) ) {
				case 1:
					{
					setState(84);
					operationEqual();
					setState(85);
					value_ref();
					}
					break;
				}
				}
				break;
			case NOT:
				{
				setState(89);
				match(NOT);
				setState(90);
				expression(4);
				}
				break;
			case PARENT_LEFT:
				{
				setState(91);
				match(PARENT_LEFT);
				setState(92);
				expression(0);
				setState(93);
				match(PARENT_RIGHT);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			_ctx.stop = _input.LT(-1);
			setState(107);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,8,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(105);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,7,_ctx) ) {
					case 1:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(97);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(98);
						operationBoolean();
						setState(99);
						expression(3);
						}
						break;
					case 2:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(101);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(102);
						operationContains();
						setState(103);
						array();
						}
						break;
					}
					} 
				}
				setState(109);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,8,_ctx);
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
		public Value_refContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_value_ref; }
	}

	public final Value_refContext value_ref() throws RecognitionException {
		Value_refContext _localctx = new Value_refContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_value_ref);
		try {
			setState(113);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case STRING:
				enterOuterAlt(_localctx, 1);
				{
				setState(110);
				string();
				}
				break;
			case IDQUOTED:
				enterOuterAlt(_localctx, 2);
				{
				setState(111);
				match(IDQUOTED);
				}
				break;
			case ID:
				enterOuterAlt(_localctx, 3);
				{
				setState(112);
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
			setState(115);
			_la = _input.LA(1);
			if ( !(_la==ID || _la==IDQUOTED) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			setState(116);
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
			setState(118);
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
		enterRule(_localctx, 26, RULE_alias_id);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(120);
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
		public TerminalNode ID() { return getToken(PolicyParser.ID, 0); }
		public TerminalNode IDQUOTED() { return getToken(PolicyParser.IDQUOTED, 0); }
		public Policy_idContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_policy_id; }
	}

	public final Policy_idContext policy_id() throws RecognitionException {
		Policy_idContext _localctx = new Policy_idContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_policy_id);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(122);
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
	public static class Policy_refContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(PolicyParser.ID, 0); }
		public TerminalNode IDQUOTED() { return getToken(PolicyParser.IDQUOTED, 0); }
		public Policy_refContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_policy_ref; }
	}

	public final Policy_refContext policy_ref() throws RecognitionException {
		Policy_refContext _localctx = new Policy_refContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_policy_ref);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(124);
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
			setState(126);
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
		"\u0004\u0001\u001a\u0081\u0002\u0000\u0007\u0000\u0002\u0001\u0007\u0001"+
		"\u0002\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004\u0007\u0004"+
		"\u0002\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007\u0007\u0007"+
		"\u0002\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002\u000b\u0007\u000b"+
		"\u0002\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e\u0002\u000f\u0007"+
		"\u000f\u0002\u0010\u0007\u0010\u0001\u0000\u0005\u0000$\b\u0000\n\u0000"+
		"\f\u0000\'\t\u0000\u0001\u0000\u0001\u0000\u0001\u0001\u0001\u0001\u0003"+
		"\u0001-\b\u0001\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001"+
		"\u0002\u0001\u0003\u0001\u0003\u0001\u0003\u0003\u00037\b\u0003\u0001"+
		"\u0003\u0001\u0003\u0001\u0003\u0001\u0004\u0001\u0004\u0001\u0004\u0001"+
		"\u0005\u0001\u0005\u0001\u0005\u0001\u0005\u0005\u0005C\b\u0005\n\u0005"+
		"\f\u0005F\t\u0005\u0001\u0005\u0001\u0005\u0001\u0006\u0001\u0006\u0001"+
		"\u0007\u0001\u0007\u0001\b\u0001\b\u0001\t\u0001\t\u0003\tR\b\t\u0001"+
		"\t\u0001\t\u0001\t\u0001\t\u0003\tX\b\t\u0001\t\u0001\t\u0001\t\u0001"+
		"\t\u0001\t\u0001\t\u0003\t`\b\t\u0001\t\u0001\t\u0001\t\u0001\t\u0001"+
		"\t\u0001\t\u0001\t\u0001\t\u0005\tj\b\t\n\t\f\tm\t\t\u0001\n\u0001\n\u0001"+
		"\n\u0003\nr\b\n\u0001\u000b\u0001\u000b\u0001\u000b\u0001\f\u0001\f\u0001"+
		"\r\u0001\r\u0001\u000e\u0001\u000e\u0001\u000f\u0001\u000f\u0001\u0010"+
		"\u0001\u0010\u0001\u0010\u0000\u0001\u0012\u0011\u0000\u0002\u0004\u0006"+
		"\b\n\f\u000e\u0010\u0012\u0014\u0016\u0018\u001a\u001c\u001e \u0000\u0004"+
		"\u0001\u0000\n\u000b\u0001\u0000\b\t\u0001\u0000\r\u0010\u0001\u0000\u0017"+
		"\u0018{\u0000%\u0001\u0000\u0000\u0000\u0002,\u0001\u0000\u0000\u0000"+
		"\u0004.\u0001\u0000\u0000\u0000\u00063\u0001\u0000\u0000\u0000\b;\u0001"+
		"\u0000\u0000\u0000\n>\u0001\u0000\u0000\u0000\fI\u0001\u0000\u0000\u0000"+
		"\u000eK\u0001\u0000\u0000\u0000\u0010M\u0001\u0000\u0000\u0000\u0012_"+
		"\u0001\u0000\u0000\u0000\u0014q\u0001\u0000\u0000\u0000\u0016s\u0001\u0000"+
		"\u0000\u0000\u0018v\u0001\u0000\u0000\u0000\u001ax\u0001\u0000\u0000\u0000"+
		"\u001cz\u0001\u0000\u0000\u0000\u001e|\u0001\u0000\u0000\u0000 ~\u0001"+
		"\u0000\u0000\u0000\"$\u0003\u0002\u0001\u0000#\"\u0001\u0000\u0000\u0000"+
		"$\'\u0001\u0000\u0000\u0000%#\u0001\u0000\u0000\u0000%&\u0001\u0000\u0000"+
		"\u0000&(\u0001\u0000\u0000\u0000\'%\u0001\u0000\u0000\u0000()\u0005\u0000"+
		"\u0000\u0001)\u0001\u0001\u0000\u0000\u0000*-\u0003\u0004\u0002\u0000"+
		"+-\u0003\u0006\u0003\u0000,*\u0001\u0000\u0000\u0000,+\u0001\u0000\u0000"+
		"\u0000-\u0003\u0001\u0000\u0000\u0000./\u0005\u0011\u0000\u0000/0\u0003"+
		"\u001a\r\u000001\u0005\u0002\u0000\u000012\u0003\u0018\f\u00002\u0005"+
		"\u0001\u0000\u0000\u000034\u0005\u0012\u0000\u000046\u0003\u001c\u000e"+
		"\u000057\u0003\b\u0004\u000065\u0001\u0000\u0000\u000067\u0001\u0000\u0000"+
		"\u000078\u0001\u0000\u0000\u000089\u0005\u0002\u0000\u00009:\u0003\u0012"+
		"\t\u0000:\u0007\u0001\u0000\u0000\u0000;<\u0005\u0013\u0000\u0000<=\u0003"+
		"\u001e\u000f\u0000=\t\u0001\u0000\u0000\u0000>?\u0005\u0006\u0000\u0000"+
		"?D\u0003\u0014\n\u0000@A\u0005\f\u0000\u0000AC\u0003\u0014\n\u0000B@\u0001"+
		"\u0000\u0000\u0000CF\u0001\u0000\u0000\u0000DB\u0001\u0000\u0000\u0000"+
		"DE\u0001\u0000\u0000\u0000EG\u0001\u0000\u0000\u0000FD\u0001\u0000\u0000"+
		"\u0000GH\u0005\u0007\u0000\u0000H\u000b\u0001\u0000\u0000\u0000IJ\u0007"+
		"\u0000\u0000\u0000J\r\u0001\u0000\u0000\u0000KL\u0007\u0001\u0000\u0000"+
		"L\u000f\u0001\u0000\u0000\u0000MN\u0007\u0002\u0000\u0000N\u0011\u0001"+
		"\u0000\u0000\u0000OQ\u0006\t\uffff\uffff\u0000PR\u0003\u0016\u000b\u0000"+
		"QP\u0001\u0000\u0000\u0000QR\u0001\u0000\u0000\u0000RS\u0001\u0000\u0000"+
		"\u0000SW\u0003 \u0010\u0000TU\u0003\u000e\u0007\u0000UV\u0003\u0014\n"+
		"\u0000VX\u0001\u0000\u0000\u0000WT\u0001\u0000\u0000\u0000WX\u0001\u0000"+
		"\u0000\u0000X`\u0001\u0000\u0000\u0000YZ\u0005\u0003\u0000\u0000Z`\u0003"+
		"\u0012\t\u0004[\\\u0005\u0004\u0000\u0000\\]\u0003\u0012\t\u0000]^\u0005"+
		"\u0005\u0000\u0000^`\u0001\u0000\u0000\u0000_O\u0001\u0000\u0000\u0000"+
		"_Y\u0001\u0000\u0000\u0000_[\u0001\u0000\u0000\u0000`k\u0001\u0000\u0000"+
		"\u0000ab\n\u0002\u0000\u0000bc\u0003\f\u0006\u0000cd\u0003\u0012\t\u0003"+
		"dj\u0001\u0000\u0000\u0000ef\n\u0001\u0000\u0000fg\u0003\u0010\b\u0000"+
		"gh\u0003\n\u0005\u0000hj\u0001\u0000\u0000\u0000ia\u0001\u0000\u0000\u0000"+
		"ie\u0001\u0000\u0000\u0000jm\u0001\u0000\u0000\u0000ki\u0001\u0000\u0000"+
		"\u0000kl\u0001\u0000\u0000\u0000l\u0013\u0001\u0000\u0000\u0000mk\u0001"+
		"\u0000\u0000\u0000nr\u0003\u0018\f\u0000or\u0005\u0018\u0000\u0000pr\u0005"+
		"\u0017\u0000\u0000qn\u0001\u0000\u0000\u0000qo\u0001\u0000\u0000\u0000"+
		"qp\u0001\u0000\u0000\u0000r\u0015\u0001\u0000\u0000\u0000st\u0007\u0003"+
		"\u0000\u0000tu\u0005\u0001\u0000\u0000u\u0017\u0001\u0000\u0000\u0000"+
		"vw\u0005\u0014\u0000\u0000w\u0019\u0001\u0000\u0000\u0000xy\u0007\u0003"+
		"\u0000\u0000y\u001b\u0001\u0000\u0000\u0000z{\u0007\u0003\u0000\u0000"+
		"{\u001d\u0001\u0000\u0000\u0000|}\u0007\u0003\u0000\u0000}\u001f\u0001"+
		"\u0000\u0000\u0000~\u007f\u0007\u0003\u0000\u0000\u007f!\u0001\u0000\u0000"+
		"\u0000\n%,6DQW_ikq";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}