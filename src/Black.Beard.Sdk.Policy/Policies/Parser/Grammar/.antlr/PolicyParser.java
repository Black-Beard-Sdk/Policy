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
		BRACKET_LEFT=9, BRACKET_RIGHT=10, EQUAL=11, INEQUAL=12, OR=13, AND=14, 
		COMMA=15, HAS=16, HAS_NOT=17, IN=18, NOT_IN=19, ALIAS=20, POLICY=21, INHERIT=22, 
		STRING=23, MULTI_LINE_COMMENT=24, SINGLE_QUOTE_CODE_STRING=25, ID=26, 
		IDQUOTED=27, VARIABLE_NAME=28, IDLOWCASE=29;
	public static final int
		RULE_script = 0, RULE_pair = 1, RULE_pair_alias = 2, RULE_pair_policy = 3, 
		RULE_inherit = 4, RULE_categories = 5, RULE_array = 6, RULE_operationBoolean = 7, 
		RULE_operationEqual = 8, RULE_operationContains = 9, RULE_expression = 10, 
		RULE_value_ref = 11, RULE_source = 12, RULE_string = 13, RULE_alias_id = 14, 
		RULE_policy_id = 15, RULE_policy_ref = 16, RULE_key_ref = 17, RULE_category = 18, 
		RULE_boolean = 19;
	private static String[] makeRuleNames() {
		return new String[] {
			"script", "pair", "pair_alias", "pair_policy", "inherit", "categories", 
			"array", "operationBoolean", "operationEqual", "operationContains", "expression", 
			"value_ref", "source", "string", "alias_id", "policy_id", "policy_ref", 
			"key_ref", "category", "boolean"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'true'", "'false'", "'+'", "'.'", "':'", "'!'", "'('", "')'", 
			"'['", "']'", "'='", "'!='", "'|'", "'&'", "','", "'has'", "'!has'", 
			"'in'", "'!in'", "'alias'", "'policy'", "'inherit'", null, null, "'''"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "TRUE", "FALSE", "PLUS", "DOT", "COLON", "NOT", "PARENT_LEFT", 
			"PARENT_RIGHT", "BRACKET_LEFT", "BRACKET_RIGHT", "EQUAL", "INEQUAL", 
			"OR", "AND", "COMMA", "HAS", "HAS_NOT", "IN", "NOT_IN", "ALIAS", "POLICY", 
			"INHERIT", "STRING", "MULTI_LINE_COMMENT", "SINGLE_QUOTE_CODE_STRING", 
			"ID", "IDQUOTED", "VARIABLE_NAME", "IDLOWCASE"
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
			setState(43);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==ALIAS || _la==POLICY) {
				{
				{
				setState(40);
				pair();
				}
				}
				setState(45);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(46);
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
			setState(50);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ALIAS:
				enterOuterAlt(_localctx, 1);
				{
				setState(48);
				pair_alias();
				}
				break;
			case POLICY:
				enterOuterAlt(_localctx, 2);
				{
				setState(49);
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
			setState(52);
			match(ALIAS);
			setState(53);
			alias_id();
			setState(54);
			match(COLON);
			setState(55);
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
			setState(57);
			match(POLICY);
			setState(59);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==PARENT_LEFT) {
				{
				setState(58);
				categories();
				}
			}

			setState(61);
			policy_id();
			setState(63);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==INHERIT) {
				{
				setState(62);
				inherit();
				}
			}

			setState(65);
			match(COLON);
			setState(66);
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
			setState(68);
			match(INHERIT);
			setState(69);
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
		enterRule(_localctx, 10, RULE_categories);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(71);
			match(PARENT_LEFT);
			setState(72);
			category();
			setState(77);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(73);
				match(COMMA);
				setState(74);
				category();
				}
				}
				setState(79);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(80);
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
		enterRule(_localctx, 12, RULE_array);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(82);
			match(BRACKET_LEFT);
			setState(83);
			value_ref();
			setState(88);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(84);
				match(COMMA);
				setState(85);
				value_ref();
				}
				}
				setState(90);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(91);
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
		enterRule(_localctx, 14, RULE_operationBoolean);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(93);
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
		enterRule(_localctx, 16, RULE_operationEqual);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(95);
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
		enterRule(_localctx, 18, RULE_operationContains);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(97);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 983040L) != 0)) ) {
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
		int _startState = 20;
		enterRecursionRule(_localctx, 20, RULE_expression, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(116);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ID:
			case IDQUOTED:
				{
				setState(101);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,6,_ctx) ) {
				case 1:
					{
					setState(100);
					source();
					}
					break;
				}
				setState(103);
				key_ref();
				setState(108);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,7,_ctx) ) {
				case 1:
					{
					setState(104);
					match(PLUS);
					}
					break;
				case 2:
					{
					setState(105);
					operationEqual();
					setState(106);
					value_ref();
					}
					break;
				}
				}
				break;
			case NOT:
				{
				setState(110);
				match(NOT);
				setState(111);
				expression(4);
				}
				break;
			case PARENT_LEFT:
				{
				setState(112);
				match(PARENT_LEFT);
				setState(113);
				expression(0);
				setState(114);
				match(PARENT_RIGHT);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			_ctx.stop = _input.LT(-1);
			setState(128);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,10,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(126);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,9,_ctx) ) {
					case 1:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(118);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(119);
						operationBoolean();
						setState(120);
						expression(3);
						}
						break;
					case 2:
						{
						_localctx = new ExpressionContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(122);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(123);
						operationContains();
						setState(124);
						array();
						}
						break;
					}
					} 
				}
				setState(130);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,10,_ctx);
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
		public Value_refContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_value_ref; }
	}

	public final Value_refContext value_ref() throws RecognitionException {
		Value_refContext _localctx = new Value_refContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_value_ref);
		try {
			setState(135);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case STRING:
				enterOuterAlt(_localctx, 1);
				{
				setState(131);
				string();
				}
				break;
			case IDQUOTED:
				enterOuterAlt(_localctx, 2);
				{
				setState(132);
				match(IDQUOTED);
				}
				break;
			case ID:
				enterOuterAlt(_localctx, 3);
				{
				setState(133);
				match(ID);
				}
				break;
			case TRUE:
			case FALSE:
				enterOuterAlt(_localctx, 4);
				{
				setState(134);
				boolean_();
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
		enterRule(_localctx, 24, RULE_source);
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
			setState(138);
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
		enterRule(_localctx, 26, RULE_string);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(140);
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
		enterRule(_localctx, 28, RULE_alias_id);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(142);
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
		enterRule(_localctx, 30, RULE_policy_id);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(144);
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
		enterRule(_localctx, 32, RULE_policy_ref);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(146);
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
		enterRule(_localctx, 34, RULE_key_ref);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(148);
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
		enterRule(_localctx, 36, RULE_category);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(150);
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
		enterRule(_localctx, 38, RULE_boolean);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(152);
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
		case 10:
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
		"\u0004\u0001\u001d\u009b\u0002\u0000\u0007\u0000\u0002\u0001\u0007\u0001"+
		"\u0002\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004\u0007\u0004"+
		"\u0002\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007\u0007\u0007"+
		"\u0002\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002\u000b\u0007\u000b"+
		"\u0002\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e\u0002\u000f\u0007"+
		"\u000f\u0002\u0010\u0007\u0010\u0002\u0011\u0007\u0011\u0002\u0012\u0007"+
		"\u0012\u0002\u0013\u0007\u0013\u0001\u0000\u0005\u0000*\b\u0000\n\u0000"+
		"\f\u0000-\t\u0000\u0001\u0000\u0001\u0000\u0001\u0001\u0001\u0001\u0003"+
		"\u00013\b\u0001\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001"+
		"\u0002\u0001\u0003\u0001\u0003\u0003\u0003<\b\u0003\u0001\u0003\u0001"+
		"\u0003\u0003\u0003@\b\u0003\u0001\u0003\u0001\u0003\u0001\u0003\u0001"+
		"\u0004\u0001\u0004\u0001\u0004\u0001\u0005\u0001\u0005\u0001\u0005\u0001"+
		"\u0005\u0005\u0005L\b\u0005\n\u0005\f\u0005O\t\u0005\u0001\u0005\u0001"+
		"\u0005\u0001\u0006\u0001\u0006\u0001\u0006\u0001\u0006\u0005\u0006W\b"+
		"\u0006\n\u0006\f\u0006Z\t\u0006\u0001\u0006\u0001\u0006\u0001\u0007\u0001"+
		"\u0007\u0001\b\u0001\b\u0001\t\u0001\t\u0001\n\u0001\n\u0003\nf\b\n\u0001"+
		"\n\u0001\n\u0001\n\u0001\n\u0001\n\u0003\nm\b\n\u0001\n\u0001\n\u0001"+
		"\n\u0001\n\u0001\n\u0001\n\u0003\nu\b\n\u0001\n\u0001\n\u0001\n\u0001"+
		"\n\u0001\n\u0001\n\u0001\n\u0001\n\u0005\n\u007f\b\n\n\n\f\n\u0082\t\n"+
		"\u0001\u000b\u0001\u000b\u0001\u000b\u0001\u000b\u0003\u000b\u0088\b\u000b"+
		"\u0001\f\u0001\f\u0001\f\u0001\r\u0001\r\u0001\u000e\u0001\u000e\u0001"+
		"\u000f\u0001\u000f\u0001\u0010\u0001\u0010\u0001\u0011\u0001\u0011\u0001"+
		"\u0012\u0001\u0012\u0001\u0013\u0001\u0013\u0001\u0013\u0000\u0001\u0014"+
		"\u0014\u0000\u0002\u0004\u0006\b\n\f\u000e\u0010\u0012\u0014\u0016\u0018"+
		"\u001a\u001c\u001e \"$&\u0000\u0005\u0001\u0000\r\u000e\u0001\u0000\u000b"+
		"\f\u0001\u0000\u0010\u0013\u0001\u0000\u001a\u001b\u0001\u0000\u0001\u0002"+
		"\u0096\u0000+\u0001\u0000\u0000\u0000\u00022\u0001\u0000\u0000\u0000\u0004"+
		"4\u0001\u0000\u0000\u0000\u00069\u0001\u0000\u0000\u0000\bD\u0001\u0000"+
		"\u0000\u0000\nG\u0001\u0000\u0000\u0000\fR\u0001\u0000\u0000\u0000\u000e"+
		"]\u0001\u0000\u0000\u0000\u0010_\u0001\u0000\u0000\u0000\u0012a\u0001"+
		"\u0000\u0000\u0000\u0014t\u0001\u0000\u0000\u0000\u0016\u0087\u0001\u0000"+
		"\u0000\u0000\u0018\u0089\u0001\u0000\u0000\u0000\u001a\u008c\u0001\u0000"+
		"\u0000\u0000\u001c\u008e\u0001\u0000\u0000\u0000\u001e\u0090\u0001\u0000"+
		"\u0000\u0000 \u0092\u0001\u0000\u0000\u0000\"\u0094\u0001\u0000\u0000"+
		"\u0000$\u0096\u0001\u0000\u0000\u0000&\u0098\u0001\u0000\u0000\u0000("+
		"*\u0003\u0002\u0001\u0000)(\u0001\u0000\u0000\u0000*-\u0001\u0000\u0000"+
		"\u0000+)\u0001\u0000\u0000\u0000+,\u0001\u0000\u0000\u0000,.\u0001\u0000"+
		"\u0000\u0000-+\u0001\u0000\u0000\u0000./\u0005\u0000\u0000\u0001/\u0001"+
		"\u0001\u0000\u0000\u000003\u0003\u0004\u0002\u000013\u0003\u0006\u0003"+
		"\u000020\u0001\u0000\u0000\u000021\u0001\u0000\u0000\u00003\u0003\u0001"+
		"\u0000\u0000\u000045\u0005\u0014\u0000\u000056\u0003\u001c\u000e\u0000"+
		"67\u0005\u0005\u0000\u000078\u0003\u001a\r\u00008\u0005\u0001\u0000\u0000"+
		"\u00009;\u0005\u0015\u0000\u0000:<\u0003\n\u0005\u0000;:\u0001\u0000\u0000"+
		"\u0000;<\u0001\u0000\u0000\u0000<=\u0001\u0000\u0000\u0000=?\u0003\u001e"+
		"\u000f\u0000>@\u0003\b\u0004\u0000?>\u0001\u0000\u0000\u0000?@\u0001\u0000"+
		"\u0000\u0000@A\u0001\u0000\u0000\u0000AB\u0005\u0005\u0000\u0000BC\u0003"+
		"\u0014\n\u0000C\u0007\u0001\u0000\u0000\u0000DE\u0005\u0016\u0000\u0000"+
		"EF\u0003 \u0010\u0000F\t\u0001\u0000\u0000\u0000GH\u0005\u0007\u0000\u0000"+
		"HM\u0003$\u0012\u0000IJ\u0005\u000f\u0000\u0000JL\u0003$\u0012\u0000K"+
		"I\u0001\u0000\u0000\u0000LO\u0001\u0000\u0000\u0000MK\u0001\u0000\u0000"+
		"\u0000MN\u0001\u0000\u0000\u0000NP\u0001\u0000\u0000\u0000OM\u0001\u0000"+
		"\u0000\u0000PQ\u0005\b\u0000\u0000Q\u000b\u0001\u0000\u0000\u0000RS\u0005"+
		"\t\u0000\u0000SX\u0003\u0016\u000b\u0000TU\u0005\u000f\u0000\u0000UW\u0003"+
		"\u0016\u000b\u0000VT\u0001\u0000\u0000\u0000WZ\u0001\u0000\u0000\u0000"+
		"XV\u0001\u0000\u0000\u0000XY\u0001\u0000\u0000\u0000Y[\u0001\u0000\u0000"+
		"\u0000ZX\u0001\u0000\u0000\u0000[\\\u0005\n\u0000\u0000\\\r\u0001\u0000"+
		"\u0000\u0000]^\u0007\u0000\u0000\u0000^\u000f\u0001\u0000\u0000\u0000"+
		"_`\u0007\u0001\u0000\u0000`\u0011\u0001\u0000\u0000\u0000ab\u0007\u0002"+
		"\u0000\u0000b\u0013\u0001\u0000\u0000\u0000ce\u0006\n\uffff\uffff\u0000"+
		"df\u0003\u0018\f\u0000ed\u0001\u0000\u0000\u0000ef\u0001\u0000\u0000\u0000"+
		"fg\u0001\u0000\u0000\u0000gl\u0003\"\u0011\u0000hm\u0005\u0003\u0000\u0000"+
		"ij\u0003\u0010\b\u0000jk\u0003\u0016\u000b\u0000km\u0001\u0000\u0000\u0000"+
		"lh\u0001\u0000\u0000\u0000li\u0001\u0000\u0000\u0000lm\u0001\u0000\u0000"+
		"\u0000mu\u0001\u0000\u0000\u0000no\u0005\u0006\u0000\u0000ou\u0003\u0014"+
		"\n\u0004pq\u0005\u0007\u0000\u0000qr\u0003\u0014\n\u0000rs\u0005\b\u0000"+
		"\u0000su\u0001\u0000\u0000\u0000tc\u0001\u0000\u0000\u0000tn\u0001\u0000"+
		"\u0000\u0000tp\u0001\u0000\u0000\u0000u\u0080\u0001\u0000\u0000\u0000"+
		"vw\n\u0002\u0000\u0000wx\u0003\u000e\u0007\u0000xy\u0003\u0014\n\u0003"+
		"y\u007f\u0001\u0000\u0000\u0000z{\n\u0001\u0000\u0000{|\u0003\u0012\t"+
		"\u0000|}\u0003\f\u0006\u0000}\u007f\u0001\u0000\u0000\u0000~v\u0001\u0000"+
		"\u0000\u0000~z\u0001\u0000\u0000\u0000\u007f\u0082\u0001\u0000\u0000\u0000"+
		"\u0080~\u0001\u0000\u0000\u0000\u0080\u0081\u0001\u0000\u0000\u0000\u0081"+
		"\u0015\u0001\u0000\u0000\u0000\u0082\u0080\u0001\u0000\u0000\u0000\u0083"+
		"\u0088\u0003\u001a\r\u0000\u0084\u0088\u0005\u001b\u0000\u0000\u0085\u0088"+
		"\u0005\u001a\u0000\u0000\u0086\u0088\u0003&\u0013\u0000\u0087\u0083\u0001"+
		"\u0000\u0000\u0000\u0087\u0084\u0001\u0000\u0000\u0000\u0087\u0085\u0001"+
		"\u0000\u0000\u0000\u0087\u0086\u0001\u0000\u0000\u0000\u0088\u0017\u0001"+
		"\u0000\u0000\u0000\u0089\u008a\u0007\u0003\u0000\u0000\u008a\u008b\u0005"+
		"\u0004\u0000\u0000\u008b\u0019\u0001\u0000\u0000\u0000\u008c\u008d\u0005"+
		"\u0017\u0000\u0000\u008d\u001b\u0001\u0000\u0000\u0000\u008e\u008f\u0007"+
		"\u0003\u0000\u0000\u008f\u001d\u0001\u0000\u0000\u0000\u0090\u0091\u0007"+
		"\u0003\u0000\u0000\u0091\u001f\u0001\u0000\u0000\u0000\u0092\u0093\u0007"+
		"\u0003\u0000\u0000\u0093!\u0001\u0000\u0000\u0000\u0094\u0095\u0007\u0003"+
		"\u0000\u0000\u0095#\u0001\u0000\u0000\u0000\u0096\u0097\u0005\u001a\u0000"+
		"\u0000\u0097%\u0001\u0000\u0000\u0000\u0098\u0099\u0007\u0004\u0000\u0000"+
		"\u0099\'\u0001\u0000\u0000\u0000\f+2;?MXelt~\u0080\u0087";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}