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
		HAS=14, HAS_NOT=15, IN=16, NOT_IN=17, ALIAS=18, POLICY=19, INHERIT=20, 
		STRING=21, MULTI_LINE_COMMENT=22, SINGLE_QUOTE_CODE_STRING=23, ID=24, 
		IDQUOTED=25, VARIABLE_NAME=26, IDLOWCASE=27;
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
			"HAS", "HAS_NOT", "IN", "NOT_IN", "ALIAS", "POLICY", "INHERIT", "ESC", 
			"STRING", "MULTI_LINE_COMMENT", "SINGLE_QUOTE_CODE_STRING", "ID", "IDQUOTED", 
			"VARIABLE_NAME", "IDLOWCASE", "SAFECODEPOINT", "UNICODE", "HEX"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'.'", "'?'", "':'", "'!'", "'('", "')'", "'['", "']'", "'='", 
			"'!='", "'|'", "'&'", "','", "'has'", "'!has'", "'in'", "'!in'", "'alias'", 
			"'policy'", "'inherit'", null, null, "'''"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "DOT", "QUESTION_MARK", "COLON", "NOT", "PARENT_LEFT", "PARENT_RIGHT", 
			"BRACKET_LEFT", "BRACKET_RIGHT", "EQUAL", "INEQUAL", "OR", "AND", "COMMA", 
			"HAS", "HAS_NOT", "IN", "NOT_IN", "ALIAS", "POLICY", "INHERIT", "STRING", 
			"MULTI_LINE_COMMENT", "SINGLE_QUOTE_CODE_STRING", "ID", "IDQUOTED", "VARIABLE_NAME", 
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
		"\u0004\u0000\u001b\u00c3\u0006\uffff\uffff\u0002\u0000\u0007\u0000\u0002"+
		"\u0001\u0007\u0001\u0002\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002"+
		"\u0004\u0007\u0004\u0002\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002"+
		"\u0007\u0007\u0007\u0002\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002"+
		"\u000b\u0007\u000b\u0002\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e"+
		"\u0002\u000f\u0007\u000f\u0002\u0010\u0007\u0010\u0002\u0011\u0007\u0011"+
		"\u0002\u0012\u0007\u0012\u0002\u0013\u0007\u0013\u0002\u0014\u0007\u0014"+
		"\u0002\u0015\u0007\u0015\u0002\u0016\u0007\u0016\u0002\u0017\u0007\u0017"+
		"\u0002\u0018\u0007\u0018\u0002\u0019\u0007\u0019\u0002\u001a\u0007\u001a"+
		"\u0002\u001b\u0007\u001b\u0002\u001c\u0007\u001c\u0002\u001d\u0007\u001d"+
		"\u0002\u001e\u0007\u001e\u0001\u0000\u0001\u0000\u0001\u0001\u0001\u0001"+
		"\u0001\u0002\u0001\u0002\u0001\u0003\u0001\u0003\u0001\u0004\u0001\u0004"+
		"\u0001\u0005\u0001\u0005\u0001\u0006\u0001\u0006\u0001\u0007\u0001\u0007"+
		"\u0001\b\u0001\b\u0001\t\u0001\t\u0001\t\u0001\n\u0001\n\u0001\u000b\u0001"+
		"\u000b\u0001\f\u0001\f\u0001\r\u0001\r\u0001\r\u0001\r\u0001\u000e\u0001"+
		"\u000e\u0001\u000e\u0001\u000e\u0001\u000e\u0001\u000f\u0001\u000f\u0001"+
		"\u000f\u0001\u0010\u0001\u0010\u0001\u0010\u0001\u0010\u0001\u0011\u0001"+
		"\u0011\u0001\u0011\u0001\u0011\u0001\u0011\u0001\u0011\u0001\u0012\u0001"+
		"\u0012\u0001\u0012\u0001\u0012\u0001\u0012\u0001\u0012\u0001\u0012\u0001"+
		"\u0013\u0001\u0013\u0001\u0013\u0001\u0013\u0001\u0013\u0001\u0013\u0001"+
		"\u0013\u0001\u0013\u0001\u0014\u0001\u0014\u0001\u0014\u0003\u0014\u0083"+
		"\b\u0014\u0001\u0015\u0001\u0015\u0001\u0015\u0005\u0015\u0088\b\u0015"+
		"\n\u0015\f\u0015\u008b\t\u0015\u0001\u0015\u0001\u0015\u0001\u0016\u0001"+
		"\u0016\u0001\u0016\u0001\u0016\u0005\u0016\u0093\b\u0016\n\u0016\f\u0016"+
		"\u0096\t\u0016\u0001\u0016\u0001\u0016\u0001\u0016\u0001\u0016\u0001\u0016"+
		"\u0001\u0017\u0001\u0017\u0001\u0018\u0001\u0018\u0005\u0018\u00a1\b\u0018"+
		"\n\u0018\f\u0018\u00a4\t\u0018\u0001\u0019\u0001\u0019\u0001\u0019\u0001"+
		"\u0019\u0001\u001a\u0001\u001a\u0005\u001a\u00ac\b\u001a\n\u001a\f\u001a"+
		"\u00af\t\u001a\u0001\u001a\u0001\u001a\u0001\u001b\u0001\u001b\u0005\u001b"+
		"\u00b5\b\u001b\n\u001b\f\u001b\u00b8\t\u001b\u0001\u001c\u0001\u001c\u0001"+
		"\u001d\u0001\u001d\u0001\u001d\u0001\u001d\u0001\u001d\u0001\u001d\u0001"+
		"\u001e\u0001\u001e\u0001\u0094\u0000\u001f\u0001\u0001\u0003\u0002\u0005"+
		"\u0003\u0007\u0004\t\u0005\u000b\u0006\r\u0007\u000f\b\u0011\t\u0013\n"+
		"\u0015\u000b\u0017\f\u0019\r\u001b\u000e\u001d\u000f\u001f\u0010!\u0011"+
		"#\u0012%\u0013\'\u0014)\u0000+\u0015-\u0016/\u00171\u00183\u00195\u001a"+
		"7\u001b9\u0000;\u0000=\u0000\u0001\u0000\u0007\b\u0000\"\"//\\\\bbffn"+
		"nrrtt\u0003\u0000AZ__az\u0004\u000009AZ__az\u0002\u0000__az\u0003\u0000"+
		"09__az\u0003\u0000\u0000\u001f\"\"\\\\\u0003\u000009AFaf\u00c5\u0000\u0001"+
		"\u0001\u0000\u0000\u0000\u0000\u0003\u0001\u0000\u0000\u0000\u0000\u0005"+
		"\u0001\u0000\u0000\u0000\u0000\u0007\u0001\u0000\u0000\u0000\u0000\t\u0001"+
		"\u0000\u0000\u0000\u0000\u000b\u0001\u0000\u0000\u0000\u0000\r\u0001\u0000"+
		"\u0000\u0000\u0000\u000f\u0001\u0000\u0000\u0000\u0000\u0011\u0001\u0000"+
		"\u0000\u0000\u0000\u0013\u0001\u0000\u0000\u0000\u0000\u0015\u0001\u0000"+
		"\u0000\u0000\u0000\u0017\u0001\u0000\u0000\u0000\u0000\u0019\u0001\u0000"+
		"\u0000\u0000\u0000\u001b\u0001\u0000\u0000\u0000\u0000\u001d\u0001\u0000"+
		"\u0000\u0000\u0000\u001f\u0001\u0000\u0000\u0000\u0000!\u0001\u0000\u0000"+
		"\u0000\u0000#\u0001\u0000\u0000\u0000\u0000%\u0001\u0000\u0000\u0000\u0000"+
		"\'\u0001\u0000\u0000\u0000\u0000+\u0001\u0000\u0000\u0000\u0000-\u0001"+
		"\u0000\u0000\u0000\u0000/\u0001\u0000\u0000\u0000\u00001\u0001\u0000\u0000"+
		"\u0000\u00003\u0001\u0000\u0000\u0000\u00005\u0001\u0000\u0000\u0000\u0000"+
		"7\u0001\u0000\u0000\u0000\u0001?\u0001\u0000\u0000\u0000\u0003A\u0001"+
		"\u0000\u0000\u0000\u0005C\u0001\u0000\u0000\u0000\u0007E\u0001\u0000\u0000"+
		"\u0000\tG\u0001\u0000\u0000\u0000\u000bI\u0001\u0000\u0000\u0000\rK\u0001"+
		"\u0000\u0000\u0000\u000fM\u0001\u0000\u0000\u0000\u0011O\u0001\u0000\u0000"+
		"\u0000\u0013Q\u0001\u0000\u0000\u0000\u0015T\u0001\u0000\u0000\u0000\u0017"+
		"V\u0001\u0000\u0000\u0000\u0019X\u0001\u0000\u0000\u0000\u001bZ\u0001"+
		"\u0000\u0000\u0000\u001d^\u0001\u0000\u0000\u0000\u001fc\u0001\u0000\u0000"+
		"\u0000!f\u0001\u0000\u0000\u0000#j\u0001\u0000\u0000\u0000%p\u0001\u0000"+
		"\u0000\u0000\'w\u0001\u0000\u0000\u0000)\u007f\u0001\u0000\u0000\u0000"+
		"+\u0084\u0001\u0000\u0000\u0000-\u008e\u0001\u0000\u0000\u0000/\u009c"+
		"\u0001\u0000\u0000\u00001\u009e\u0001\u0000\u0000\u00003\u00a5\u0001\u0000"+
		"\u0000\u00005\u00a9\u0001\u0000\u0000\u00007\u00b2\u0001\u0000\u0000\u0000"+
		"9\u00b9\u0001\u0000\u0000\u0000;\u00bb\u0001\u0000\u0000\u0000=\u00c1"+
		"\u0001\u0000\u0000\u0000?@\u0005.\u0000\u0000@\u0002\u0001\u0000\u0000"+
		"\u0000AB\u0005?\u0000\u0000B\u0004\u0001\u0000\u0000\u0000CD\u0005:\u0000"+
		"\u0000D\u0006\u0001\u0000\u0000\u0000EF\u0005!\u0000\u0000F\b\u0001\u0000"+
		"\u0000\u0000GH\u0005(\u0000\u0000H\n\u0001\u0000\u0000\u0000IJ\u0005)"+
		"\u0000\u0000J\f\u0001\u0000\u0000\u0000KL\u0005[\u0000\u0000L\u000e\u0001"+
		"\u0000\u0000\u0000MN\u0005]\u0000\u0000N\u0010\u0001\u0000\u0000\u0000"+
		"OP\u0005=\u0000\u0000P\u0012\u0001\u0000\u0000\u0000QR\u0005!\u0000\u0000"+
		"RS\u0005=\u0000\u0000S\u0014\u0001\u0000\u0000\u0000TU\u0005|\u0000\u0000"+
		"U\u0016\u0001\u0000\u0000\u0000VW\u0005&\u0000\u0000W\u0018\u0001\u0000"+
		"\u0000\u0000XY\u0005,\u0000\u0000Y\u001a\u0001\u0000\u0000\u0000Z[\u0005"+
		"h\u0000\u0000[\\\u0005a\u0000\u0000\\]\u0005s\u0000\u0000]\u001c\u0001"+
		"\u0000\u0000\u0000^_\u0005!\u0000\u0000_`\u0005h\u0000\u0000`a\u0005a"+
		"\u0000\u0000ab\u0005s\u0000\u0000b\u001e\u0001\u0000\u0000\u0000cd\u0005"+
		"i\u0000\u0000de\u0005n\u0000\u0000e \u0001\u0000\u0000\u0000fg\u0005!"+
		"\u0000\u0000gh\u0005i\u0000\u0000hi\u0005n\u0000\u0000i\"\u0001\u0000"+
		"\u0000\u0000jk\u0005a\u0000\u0000kl\u0005l\u0000\u0000lm\u0005i\u0000"+
		"\u0000mn\u0005a\u0000\u0000no\u0005s\u0000\u0000o$\u0001\u0000\u0000\u0000"+
		"pq\u0005p\u0000\u0000qr\u0005o\u0000\u0000rs\u0005l\u0000\u0000st\u0005"+
		"i\u0000\u0000tu\u0005c\u0000\u0000uv\u0005y\u0000\u0000v&\u0001\u0000"+
		"\u0000\u0000wx\u0005i\u0000\u0000xy\u0005n\u0000\u0000yz\u0005h\u0000"+
		"\u0000z{\u0005e\u0000\u0000{|\u0005r\u0000\u0000|}\u0005i\u0000\u0000"+
		"}~\u0005t\u0000\u0000~(\u0001\u0000\u0000\u0000\u007f\u0082\u0005\\\u0000"+
		"\u0000\u0080\u0083\u0007\u0000\u0000\u0000\u0081\u0083\u0003;\u001d\u0000"+
		"\u0082\u0080\u0001\u0000\u0000\u0000\u0082\u0081\u0001\u0000\u0000\u0000"+
		"\u0083*\u0001\u0000\u0000\u0000\u0084\u0089\u0005\"\u0000\u0000\u0085"+
		"\u0088\u0003)\u0014\u0000\u0086\u0088\u00039\u001c\u0000\u0087\u0085\u0001"+
		"\u0000\u0000\u0000\u0087\u0086\u0001\u0000\u0000\u0000\u0088\u008b\u0001"+
		"\u0000\u0000\u0000\u0089\u0087\u0001\u0000\u0000\u0000\u0089\u008a\u0001"+
		"\u0000\u0000\u0000\u008a\u008c\u0001\u0000\u0000\u0000\u008b\u0089\u0001"+
		"\u0000\u0000\u0000\u008c\u008d\u0005\"\u0000\u0000\u008d,\u0001\u0000"+
		"\u0000\u0000\u008e\u008f\u0005/\u0000\u0000\u008f\u0090\u0005*\u0000\u0000"+
		"\u0090\u0094\u0001\u0000\u0000\u0000\u0091\u0093\t\u0000\u0000\u0000\u0092"+
		"\u0091\u0001\u0000\u0000\u0000\u0093\u0096\u0001\u0000\u0000\u0000\u0094"+
		"\u0095\u0001\u0000\u0000\u0000\u0094\u0092\u0001\u0000\u0000\u0000\u0095"+
		"\u0097\u0001\u0000\u0000\u0000\u0096\u0094\u0001\u0000\u0000\u0000\u0097"+
		"\u0098\u0005*\u0000\u0000\u0098\u0099\u0005/\u0000\u0000\u0099\u009a\u0001"+
		"\u0000\u0000\u0000\u009a\u009b\u0006\u0016\u0000\u0000\u009b.\u0001\u0000"+
		"\u0000\u0000\u009c\u009d\u0005\'\u0000\u0000\u009d0\u0001\u0000\u0000"+
		"\u0000\u009e\u00a2\u0007\u0001\u0000\u0000\u009f\u00a1\u0007\u0002\u0000"+
		"\u0000\u00a0\u009f\u0001\u0000\u0000\u0000\u00a1\u00a4\u0001\u0000\u0000"+
		"\u0000\u00a2\u00a0\u0001\u0000\u0000\u0000\u00a2\u00a3\u0001\u0000\u0000"+
		"\u0000\u00a32\u0001\u0000\u0000\u0000\u00a4\u00a2\u0001\u0000\u0000\u0000"+
		"\u00a5\u00a6\u0005\'\u0000\u0000\u00a6\u00a7\u00031\u0018\u0000\u00a7"+
		"\u00a8\u0005\'\u0000\u0000\u00a84\u0001\u0000\u0000\u0000\u00a9\u00ad"+
		"\u0007\u0001\u0000\u0000\u00aa\u00ac\u0007\u0002\u0000\u0000\u00ab\u00aa"+
		"\u0001\u0000\u0000\u0000\u00ac\u00af\u0001\u0000\u0000\u0000\u00ad\u00ab"+
		"\u0001\u0000\u0000\u0000\u00ad\u00ae\u0001\u0000\u0000\u0000\u00ae\u00b0"+
		"\u0001\u0000\u0000\u0000\u00af\u00ad\u0001\u0000\u0000\u0000\u00b0\u00b1"+
		"\u0003\u0005\u0002\u0000\u00b16\u0001\u0000\u0000\u0000\u00b2\u00b6\u0007"+
		"\u0003\u0000\u0000\u00b3\u00b5\u0007\u0004\u0000\u0000\u00b4\u00b3\u0001"+
		"\u0000\u0000\u0000\u00b5\u00b8\u0001\u0000\u0000\u0000\u00b6\u00b4\u0001"+
		"\u0000\u0000\u0000\u00b6\u00b7\u0001\u0000\u0000\u0000\u00b78\u0001\u0000"+
		"\u0000\u0000\u00b8\u00b6\u0001\u0000\u0000\u0000\u00b9\u00ba\b\u0005\u0000"+
		"\u0000\u00ba:\u0001\u0000\u0000\u0000\u00bb\u00bc\u0005u\u0000\u0000\u00bc"+
		"\u00bd\u0003=\u001e\u0000\u00bd\u00be\u0003=\u001e\u0000\u00be\u00bf\u0003"+
		"=\u001e\u0000\u00bf\u00c0\u0003=\u001e\u0000\u00c0<\u0001\u0000\u0000"+
		"\u0000\u00c1\u00c2\u0007\u0006\u0000\u0000\u00c2>\u0001\u0000\u0000\u0000"+
		"\b\u0000\u0082\u0087\u0089\u0094\u00a2\u00ad\u00b6\u0001\u0006\u0000\u0000";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}