// Generated from f:/srcs/Policy/src/Black.Beard.Sdk.Policy/Policies/Parser/Grammar/PolicyLexer.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue", "this-escape"})
public class PolicyLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		DOT=1, QUESTION_MARK=2, COLON=3, NOT=4, PARENT_LEFT=5, PARENT_RIGHT=6, 
		BRACKET_LEFT=7, BRACKET_RIGHT=8, EQUAL=9, INEQUAL=10, OR=11, AND=12, COMMA=13, 
		HAS=14, HAS_NOT=15, IN=16, NOT_IN=17, ALIAS=18, POLICY=19, STRING=20, 
		MULTI_LINE_COMMENT=21, SINGLE_QUOTE_CODE_STRING=22, ID=23, IDQUOTED=24, 
		VARIABLE_NAME=25, IDLOWCASE=26;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"DOT", "QUESTION_MARK", "COLON", "NOT", "PARENT_LEFT", "PARENT_RIGHT", 
			"BRACKET_LEFT", "BRACKET_RIGHT", "EQUAL", "INEQUAL", "OR", "AND", "COMMA", 
			"HAS", "HAS_NOT", "IN", "NOT_IN", "ALIAS", "POLICY", "ESC", "STRING", 
			"MULTI_LINE_COMMENT", "SINGLE_QUOTE_CODE_STRING", "ID", "IDQUOTED", "VARIABLE_NAME", 
			"IDLOWCASE", "SAFECODEPOINT", "UNICODE", "HEX"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'.'", "'?'", "':'", "'!'", "'('", "')'", "'['", "']'", "'='", 
			"'!='", "'|'", "'&'", "','", "'has'", "'!has'", "'in'", "'!in'", "'alias'", 
			"'policy'", null, null, "'''"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "DOT", "QUESTION_MARK", "COLON", "NOT", "PARENT_LEFT", "PARENT_RIGHT", 
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


	public PolicyLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "PolicyLexer.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\u0004\u0000\u001a\u00b9\u0006\uffff\uffff\u0002\u0000\u0007\u0000\u0002"+
		"\u0001\u0007\u0001\u0002\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002"+
		"\u0004\u0007\u0004\u0002\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002"+
		"\u0007\u0007\u0007\u0002\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002"+
		"\u000b\u0007\u000b\u0002\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e"+
		"\u0002\u000f\u0007\u000f\u0002\u0010\u0007\u0010\u0002\u0011\u0007\u0011"+
		"\u0002\u0012\u0007\u0012\u0002\u0013\u0007\u0013\u0002\u0014\u0007\u0014"+
		"\u0002\u0015\u0007\u0015\u0002\u0016\u0007\u0016\u0002\u0017\u0007\u0017"+
		"\u0002\u0018\u0007\u0018\u0002\u0019\u0007\u0019\u0002\u001a\u0007\u001a"+
		"\u0002\u001b\u0007\u001b\u0002\u001c\u0007\u001c\u0002\u001d\u0007\u001d"+
		"\u0001\u0000\u0001\u0000\u0001\u0001\u0001\u0001\u0001\u0002\u0001\u0002"+
		"\u0001\u0003\u0001\u0003\u0001\u0004\u0001\u0004\u0001\u0005\u0001\u0005"+
		"\u0001\u0006\u0001\u0006\u0001\u0007\u0001\u0007\u0001\b\u0001\b\u0001"+
		"\t\u0001\t\u0001\t\u0001\n\u0001\n\u0001\u000b\u0001\u000b\u0001\f\u0001"+
		"\f\u0001\r\u0001\r\u0001\r\u0001\r\u0001\u000e\u0001\u000e\u0001\u000e"+
		"\u0001\u000e\u0001\u000e\u0001\u000f\u0001\u000f\u0001\u000f\u0001\u0010"+
		"\u0001\u0010\u0001\u0010\u0001\u0010\u0001\u0011\u0001\u0011\u0001\u0011"+
		"\u0001\u0011\u0001\u0011\u0001\u0011\u0001\u0012\u0001\u0012\u0001\u0012"+
		"\u0001\u0012\u0001\u0012\u0001\u0012\u0001\u0012\u0001\u0013\u0001\u0013"+
		"\u0001\u0013\u0003\u0013y\b\u0013\u0001\u0014\u0001\u0014\u0001\u0014"+
		"\u0005\u0014~\b\u0014\n\u0014\f\u0014\u0081\t\u0014\u0001\u0014\u0001"+
		"\u0014\u0001\u0015\u0001\u0015\u0001\u0015\u0001\u0015\u0005\u0015\u0089"+
		"\b\u0015\n\u0015\f\u0015\u008c\t\u0015\u0001\u0015\u0001\u0015\u0001\u0015"+
		"\u0001\u0015\u0001\u0015\u0001\u0016\u0001\u0016\u0001\u0017\u0001\u0017"+
		"\u0005\u0017\u0097\b\u0017\n\u0017\f\u0017\u009a\t\u0017\u0001\u0018\u0001"+
		"\u0018\u0001\u0018\u0001\u0018\u0001\u0019\u0001\u0019\u0005\u0019\u00a2"+
		"\b\u0019\n\u0019\f\u0019\u00a5\t\u0019\u0001\u0019\u0001\u0019\u0001\u001a"+
		"\u0001\u001a\u0005\u001a\u00ab\b\u001a\n\u001a\f\u001a\u00ae\t\u001a\u0001"+
		"\u001b\u0001\u001b\u0001\u001c\u0001\u001c\u0001\u001c\u0001\u001c\u0001"+
		"\u001c\u0001\u001c\u0001\u001d\u0001\u001d\u0001\u008a\u0000\u001e\u0001"+
		"\u0001\u0003\u0002\u0005\u0003\u0007\u0004\t\u0005\u000b\u0006\r\u0007"+
		"\u000f\b\u0011\t\u0013\n\u0015\u000b\u0017\f\u0019\r\u001b\u000e\u001d"+
		"\u000f\u001f\u0010!\u0011#\u0012%\u0013\'\u0000)\u0014+\u0015-\u0016/"+
		"\u00171\u00183\u00195\u001a7\u00009\u0000;\u0000\u0001\u0000\u0007\b\u0000"+
		"\"\"//\\\\bbffnnrrtt\u0003\u0000AZ__az\u0004\u000009AZ__az\u0002\u0000"+
		"__az\u0003\u000009__az\u0003\u0000\u0000\u001f\"\"\\\\\u0003\u000009A"+
		"Faf\u00bb\u0000\u0001\u0001\u0000\u0000\u0000\u0000\u0003\u0001\u0000"+
		"\u0000\u0000\u0000\u0005\u0001\u0000\u0000\u0000\u0000\u0007\u0001\u0000"+
		"\u0000\u0000\u0000\t\u0001\u0000\u0000\u0000\u0000\u000b\u0001\u0000\u0000"+
		"\u0000\u0000\r\u0001\u0000\u0000\u0000\u0000\u000f\u0001\u0000\u0000\u0000"+
		"\u0000\u0011\u0001\u0000\u0000\u0000\u0000\u0013\u0001\u0000\u0000\u0000"+
		"\u0000\u0015\u0001\u0000\u0000\u0000\u0000\u0017\u0001\u0000\u0000\u0000"+
		"\u0000\u0019\u0001\u0000\u0000\u0000\u0000\u001b\u0001\u0000\u0000\u0000"+
		"\u0000\u001d\u0001\u0000\u0000\u0000\u0000\u001f\u0001\u0000\u0000\u0000"+
		"\u0000!\u0001\u0000\u0000\u0000\u0000#\u0001\u0000\u0000\u0000\u0000%"+
		"\u0001\u0000\u0000\u0000\u0000)\u0001\u0000\u0000\u0000\u0000+\u0001\u0000"+
		"\u0000\u0000\u0000-\u0001\u0000\u0000\u0000\u0000/\u0001\u0000\u0000\u0000"+
		"\u00001\u0001\u0000\u0000\u0000\u00003\u0001\u0000\u0000\u0000\u00005"+
		"\u0001\u0000\u0000\u0000\u0001=\u0001\u0000\u0000\u0000\u0003?\u0001\u0000"+
		"\u0000\u0000\u0005A\u0001\u0000\u0000\u0000\u0007C\u0001\u0000\u0000\u0000"+
		"\tE\u0001\u0000\u0000\u0000\u000bG\u0001\u0000\u0000\u0000\rI\u0001\u0000"+
		"\u0000\u0000\u000fK\u0001\u0000\u0000\u0000\u0011M\u0001\u0000\u0000\u0000"+
		"\u0013O\u0001\u0000\u0000\u0000\u0015R\u0001\u0000\u0000\u0000\u0017T"+
		"\u0001\u0000\u0000\u0000\u0019V\u0001\u0000\u0000\u0000\u001bX\u0001\u0000"+
		"\u0000\u0000\u001d\\\u0001\u0000\u0000\u0000\u001fa\u0001\u0000\u0000"+
		"\u0000!d\u0001\u0000\u0000\u0000#h\u0001\u0000\u0000\u0000%n\u0001\u0000"+
		"\u0000\u0000\'u\u0001\u0000\u0000\u0000)z\u0001\u0000\u0000\u0000+\u0084"+
		"\u0001\u0000\u0000\u0000-\u0092\u0001\u0000\u0000\u0000/\u0094\u0001\u0000"+
		"\u0000\u00001\u009b\u0001\u0000\u0000\u00003\u009f\u0001\u0000\u0000\u0000"+
		"5\u00a8\u0001\u0000\u0000\u00007\u00af\u0001\u0000\u0000\u00009\u00b1"+
		"\u0001\u0000\u0000\u0000;\u00b7\u0001\u0000\u0000\u0000=>\u0005.\u0000"+
		"\u0000>\u0002\u0001\u0000\u0000\u0000?@\u0005?\u0000\u0000@\u0004\u0001"+
		"\u0000\u0000\u0000AB\u0005:\u0000\u0000B\u0006\u0001\u0000\u0000\u0000"+
		"CD\u0005!\u0000\u0000D\b\u0001\u0000\u0000\u0000EF\u0005(\u0000\u0000"+
		"F\n\u0001\u0000\u0000\u0000GH\u0005)\u0000\u0000H\f\u0001\u0000\u0000"+
		"\u0000IJ\u0005[\u0000\u0000J\u000e\u0001\u0000\u0000\u0000KL\u0005]\u0000"+
		"\u0000L\u0010\u0001\u0000\u0000\u0000MN\u0005=\u0000\u0000N\u0012\u0001"+
		"\u0000\u0000\u0000OP\u0005!\u0000\u0000PQ\u0005=\u0000\u0000Q\u0014\u0001"+
		"\u0000\u0000\u0000RS\u0005|\u0000\u0000S\u0016\u0001\u0000\u0000\u0000"+
		"TU\u0005&\u0000\u0000U\u0018\u0001\u0000\u0000\u0000VW\u0005,\u0000\u0000"+
		"W\u001a\u0001\u0000\u0000\u0000XY\u0005h\u0000\u0000YZ\u0005a\u0000\u0000"+
		"Z[\u0005s\u0000\u0000[\u001c\u0001\u0000\u0000\u0000\\]\u0005!\u0000\u0000"+
		"]^\u0005h\u0000\u0000^_\u0005a\u0000\u0000_`\u0005s\u0000\u0000`\u001e"+
		"\u0001\u0000\u0000\u0000ab\u0005i\u0000\u0000bc\u0005n\u0000\u0000c \u0001"+
		"\u0000\u0000\u0000de\u0005!\u0000\u0000ef\u0005i\u0000\u0000fg\u0005n"+
		"\u0000\u0000g\"\u0001\u0000\u0000\u0000hi\u0005a\u0000\u0000ij\u0005l"+
		"\u0000\u0000jk\u0005i\u0000\u0000kl\u0005a\u0000\u0000lm\u0005s\u0000"+
		"\u0000m$\u0001\u0000\u0000\u0000no\u0005p\u0000\u0000op\u0005o\u0000\u0000"+
		"pq\u0005l\u0000\u0000qr\u0005i\u0000\u0000rs\u0005c\u0000\u0000st\u0005"+
		"y\u0000\u0000t&\u0001\u0000\u0000\u0000ux\u0005\\\u0000\u0000vy\u0007"+
		"\u0000\u0000\u0000wy\u00039\u001c\u0000xv\u0001\u0000\u0000\u0000xw\u0001"+
		"\u0000\u0000\u0000y(\u0001\u0000\u0000\u0000z\u007f\u0005\"\u0000\u0000"+
		"{~\u0003\'\u0013\u0000|~\u00037\u001b\u0000}{\u0001\u0000\u0000\u0000"+
		"}|\u0001\u0000\u0000\u0000~\u0081\u0001\u0000\u0000\u0000\u007f}\u0001"+
		"\u0000\u0000\u0000\u007f\u0080\u0001\u0000\u0000\u0000\u0080\u0082\u0001"+
		"\u0000\u0000\u0000\u0081\u007f\u0001\u0000\u0000\u0000\u0082\u0083\u0005"+
		"\"\u0000\u0000\u0083*\u0001\u0000\u0000\u0000\u0084\u0085\u0005/\u0000"+
		"\u0000\u0085\u0086\u0005*\u0000\u0000\u0086\u008a\u0001\u0000\u0000\u0000"+
		"\u0087\u0089\t\u0000\u0000\u0000\u0088\u0087\u0001\u0000\u0000\u0000\u0089"+
		"\u008c\u0001\u0000\u0000\u0000\u008a\u008b\u0001\u0000\u0000\u0000\u008a"+
		"\u0088\u0001\u0000\u0000\u0000\u008b\u008d\u0001\u0000\u0000\u0000\u008c"+
		"\u008a\u0001\u0000\u0000\u0000\u008d\u008e\u0005*\u0000\u0000\u008e\u008f"+
		"\u0005/\u0000\u0000\u008f\u0090\u0001\u0000\u0000\u0000\u0090\u0091\u0006"+
		"\u0015\u0000\u0000\u0091,\u0001\u0000\u0000\u0000\u0092\u0093\u0005\'"+
		"\u0000\u0000\u0093.\u0001\u0000\u0000\u0000\u0094\u0098\u0007\u0001\u0000"+
		"\u0000\u0095\u0097\u0007\u0002\u0000\u0000\u0096\u0095\u0001\u0000\u0000"+
		"\u0000\u0097\u009a\u0001\u0000\u0000\u0000\u0098\u0096\u0001\u0000\u0000"+
		"\u0000\u0098\u0099\u0001\u0000\u0000\u0000\u00990\u0001\u0000\u0000\u0000"+
		"\u009a\u0098\u0001\u0000\u0000\u0000\u009b\u009c\u0005\'\u0000\u0000\u009c"+
		"\u009d\u0003/\u0017\u0000\u009d\u009e\u0005\'\u0000\u0000\u009e2\u0001"+
		"\u0000\u0000\u0000\u009f\u00a3\u0007\u0001\u0000\u0000\u00a0\u00a2\u0007"+
		"\u0002\u0000\u0000\u00a1\u00a0\u0001\u0000\u0000\u0000\u00a2\u00a5\u0001"+
		"\u0000\u0000\u0000\u00a3\u00a1\u0001\u0000\u0000\u0000\u00a3\u00a4\u0001"+
		"\u0000\u0000\u0000\u00a4\u00a6\u0001\u0000\u0000\u0000\u00a5\u00a3\u0001"+
		"\u0000\u0000\u0000\u00a6\u00a7\u0003\u0005\u0002\u0000\u00a74\u0001\u0000"+
		"\u0000\u0000\u00a8\u00ac\u0007\u0003\u0000\u0000\u00a9\u00ab\u0007\u0004"+
		"\u0000\u0000\u00aa\u00a9\u0001\u0000\u0000\u0000\u00ab\u00ae\u0001\u0000"+
		"\u0000\u0000\u00ac\u00aa\u0001\u0000\u0000\u0000\u00ac\u00ad\u0001\u0000"+
		"\u0000\u0000\u00ad6\u0001\u0000\u0000\u0000\u00ae\u00ac\u0001\u0000\u0000"+
		"\u0000\u00af\u00b0\b\u0005\u0000\u0000\u00b08\u0001\u0000\u0000\u0000"+
		"\u00b1\u00b2\u0005u\u0000\u0000\u00b2\u00b3\u0003;\u001d\u0000\u00b3\u00b4"+
		"\u0003;\u001d\u0000\u00b4\u00b5\u0003;\u001d\u0000\u00b5\u00b6\u0003;"+
		"\u001d\u0000\u00b6:\u0001\u0000\u0000\u0000\u00b7\u00b8\u0007\u0006\u0000"+
		"\u0000\u00b8<\u0001\u0000\u0000\u0000\b\u0000x}\u007f\u008a\u0098\u00a3"+
		"\u00ac\u0001\u0006\u0000\u0000";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}