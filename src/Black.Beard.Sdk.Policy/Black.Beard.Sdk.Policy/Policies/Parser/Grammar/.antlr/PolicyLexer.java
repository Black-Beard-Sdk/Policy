// Generated from f:/srcs/Policy/src/Black.Beard.Sdk.Policy/Black.Beard.Sdk.Policy/Policies/Parser/Grammar/PolicyLexer.g4 by ANTLR 4.13.1
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
		QUESTION_MARK=1, COLON=2, NOT=3, PARENT_LEFT=4, PARENT_RIGHT=5, BRACKET_LEFT=6, 
		BRACKET_RIGHT=7, EQUAL=8, INEQUAL=9, OR=10, AND=11, COMMA=12, HAS=13, 
		HAS_NOT=14, IN=15, NOT_IN=16, ALIAS=17, POLICY=18, STRING=19, MULTI_LINE_COMMENT=20, 
		SINGLE_QUOTE_CODE_STRING=21, ID=22, IDQUOTED=23, VARIABLE_NAME=24, IDLOWCASE=25;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"QUESTION_MARK", "COLON", "NOT", "PARENT_LEFT", "PARENT_RIGHT", "BRACKET_LEFT", 
			"BRACKET_RIGHT", "EQUAL", "INEQUAL", "OR", "AND", "COMMA", "HAS", "HAS_NOT", 
			"IN", "NOT_IN", "ALIAS", "POLICY", "ESC", "STRING", "MULTI_LINE_COMMENT", 
			"SINGLE_QUOTE_CODE_STRING", "ID", "IDQUOTED", "VARIABLE_NAME", "IDLOWCASE", 
			"SAFECODEPOINT", "UNICODE", "HEX"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'?'", "':'", "'!'", "'('", "')'", "'['", "']'", "'='", "'!='", 
			"'|'", "'&'", "','", "'has'", "'!has'", "'in'", "'!in'", "'alias'", "'policy'", 
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
		"\u0004\u0000\u0019\u00b5\u0006\uffff\uffff\u0002\u0000\u0007\u0000\u0002"+
		"\u0001\u0007\u0001\u0002\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002"+
		"\u0004\u0007\u0004\u0002\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002"+
		"\u0007\u0007\u0007\u0002\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002"+
		"\u000b\u0007\u000b\u0002\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e"+
		"\u0002\u000f\u0007\u000f\u0002\u0010\u0007\u0010\u0002\u0011\u0007\u0011"+
		"\u0002\u0012\u0007\u0012\u0002\u0013\u0007\u0013\u0002\u0014\u0007\u0014"+
		"\u0002\u0015\u0007\u0015\u0002\u0016\u0007\u0016\u0002\u0017\u0007\u0017"+
		"\u0002\u0018\u0007\u0018\u0002\u0019\u0007\u0019\u0002\u001a\u0007\u001a"+
		"\u0002\u001b\u0007\u001b\u0002\u001c\u0007\u001c\u0001\u0000\u0001\u0000"+
		"\u0001\u0001\u0001\u0001\u0001\u0002\u0001\u0002\u0001\u0003\u0001\u0003"+
		"\u0001\u0004\u0001\u0004\u0001\u0005\u0001\u0005\u0001\u0006\u0001\u0006"+
		"\u0001\u0007\u0001\u0007\u0001\b\u0001\b\u0001\b\u0001\t\u0001\t\u0001"+
		"\n\u0001\n\u0001\u000b\u0001\u000b\u0001\f\u0001\f\u0001\f\u0001\f\u0001"+
		"\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\u000e\u0001\u000e\u0001\u000e"+
		"\u0001\u000f\u0001\u000f\u0001\u000f\u0001\u000f\u0001\u0010\u0001\u0010"+
		"\u0001\u0010\u0001\u0010\u0001\u0010\u0001\u0010\u0001\u0011\u0001\u0011"+
		"\u0001\u0011\u0001\u0011\u0001\u0011\u0001\u0011\u0001\u0011\u0001\u0012"+
		"\u0001\u0012\u0001\u0012\u0003\u0012u\b\u0012\u0001\u0013\u0001\u0013"+
		"\u0001\u0013\u0005\u0013z\b\u0013\n\u0013\f\u0013}\t\u0013\u0001\u0013"+
		"\u0001\u0013\u0001\u0014\u0001\u0014\u0001\u0014\u0001\u0014\u0005\u0014"+
		"\u0085\b\u0014\n\u0014\f\u0014\u0088\t\u0014\u0001\u0014\u0001\u0014\u0001"+
		"\u0014\u0001\u0014\u0001\u0014\u0001\u0015\u0001\u0015\u0001\u0016\u0001"+
		"\u0016\u0005\u0016\u0093\b\u0016\n\u0016\f\u0016\u0096\t\u0016\u0001\u0017"+
		"\u0001\u0017\u0001\u0017\u0001\u0017\u0001\u0018\u0001\u0018\u0005\u0018"+
		"\u009e\b\u0018\n\u0018\f\u0018\u00a1\t\u0018\u0001\u0018\u0001\u0018\u0001"+
		"\u0019\u0001\u0019\u0005\u0019\u00a7\b\u0019\n\u0019\f\u0019\u00aa\t\u0019"+
		"\u0001\u001a\u0001\u001a\u0001\u001b\u0001\u001b\u0001\u001b\u0001\u001b"+
		"\u0001\u001b\u0001\u001b\u0001\u001c\u0001\u001c\u0001\u0086\u0000\u001d"+
		"\u0001\u0001\u0003\u0002\u0005\u0003\u0007\u0004\t\u0005\u000b\u0006\r"+
		"\u0007\u000f\b\u0011\t\u0013\n\u0015\u000b\u0017\f\u0019\r\u001b\u000e"+
		"\u001d\u000f\u001f\u0010!\u0011#\u0012%\u0000\'\u0013)\u0014+\u0015-\u0016"+
		"/\u00171\u00183\u00195\u00007\u00009\u0000\u0001\u0000\u0007\b\u0000\""+
		"\"//\\\\bbffnnrrtt\u0003\u0000AZ__az\u0004\u000009AZ__az\u0002\u0000_"+
		"_az\u0003\u000009__az\u0003\u0000\u0000\u001f\"\"\\\\\u0003\u000009AF"+
		"af\u00b7\u0000\u0001\u0001\u0000\u0000\u0000\u0000\u0003\u0001\u0000\u0000"+
		"\u0000\u0000\u0005\u0001\u0000\u0000\u0000\u0000\u0007\u0001\u0000\u0000"+
		"\u0000\u0000\t\u0001\u0000\u0000\u0000\u0000\u000b\u0001\u0000\u0000\u0000"+
		"\u0000\r\u0001\u0000\u0000\u0000\u0000\u000f\u0001\u0000\u0000\u0000\u0000"+
		"\u0011\u0001\u0000\u0000\u0000\u0000\u0013\u0001\u0000\u0000\u0000\u0000"+
		"\u0015\u0001\u0000\u0000\u0000\u0000\u0017\u0001\u0000\u0000\u0000\u0000"+
		"\u0019\u0001\u0000\u0000\u0000\u0000\u001b\u0001\u0000\u0000\u0000\u0000"+
		"\u001d\u0001\u0000\u0000\u0000\u0000\u001f\u0001\u0000\u0000\u0000\u0000"+
		"!\u0001\u0000\u0000\u0000\u0000#\u0001\u0000\u0000\u0000\u0000\'\u0001"+
		"\u0000\u0000\u0000\u0000)\u0001\u0000\u0000\u0000\u0000+\u0001\u0000\u0000"+
		"\u0000\u0000-\u0001\u0000\u0000\u0000\u0000/\u0001\u0000\u0000\u0000\u0000"+
		"1\u0001\u0000\u0000\u0000\u00003\u0001\u0000\u0000\u0000\u0001;\u0001"+
		"\u0000\u0000\u0000\u0003=\u0001\u0000\u0000\u0000\u0005?\u0001\u0000\u0000"+
		"\u0000\u0007A\u0001\u0000\u0000\u0000\tC\u0001\u0000\u0000\u0000\u000b"+
		"E\u0001\u0000\u0000\u0000\rG\u0001\u0000\u0000\u0000\u000fI\u0001\u0000"+
		"\u0000\u0000\u0011K\u0001\u0000\u0000\u0000\u0013N\u0001\u0000\u0000\u0000"+
		"\u0015P\u0001\u0000\u0000\u0000\u0017R\u0001\u0000\u0000\u0000\u0019T"+
		"\u0001\u0000\u0000\u0000\u001bX\u0001\u0000\u0000\u0000\u001d]\u0001\u0000"+
		"\u0000\u0000\u001f`\u0001\u0000\u0000\u0000!d\u0001\u0000\u0000\u0000"+
		"#j\u0001\u0000\u0000\u0000%q\u0001\u0000\u0000\u0000\'v\u0001\u0000\u0000"+
		"\u0000)\u0080\u0001\u0000\u0000\u0000+\u008e\u0001\u0000\u0000\u0000-"+
		"\u0090\u0001\u0000\u0000\u0000/\u0097\u0001\u0000\u0000\u00001\u009b\u0001"+
		"\u0000\u0000\u00003\u00a4\u0001\u0000\u0000\u00005\u00ab\u0001\u0000\u0000"+
		"\u00007\u00ad\u0001\u0000\u0000\u00009\u00b3\u0001\u0000\u0000\u0000;"+
		"<\u0005?\u0000\u0000<\u0002\u0001\u0000\u0000\u0000=>\u0005:\u0000\u0000"+
		">\u0004\u0001\u0000\u0000\u0000?@\u0005!\u0000\u0000@\u0006\u0001\u0000"+
		"\u0000\u0000AB\u0005(\u0000\u0000B\b\u0001\u0000\u0000\u0000CD\u0005)"+
		"\u0000\u0000D\n\u0001\u0000\u0000\u0000EF\u0005[\u0000\u0000F\f\u0001"+
		"\u0000\u0000\u0000GH\u0005]\u0000\u0000H\u000e\u0001\u0000\u0000\u0000"+
		"IJ\u0005=\u0000\u0000J\u0010\u0001\u0000\u0000\u0000KL\u0005!\u0000\u0000"+
		"LM\u0005=\u0000\u0000M\u0012\u0001\u0000\u0000\u0000NO\u0005|\u0000\u0000"+
		"O\u0014\u0001\u0000\u0000\u0000PQ\u0005&\u0000\u0000Q\u0016\u0001\u0000"+
		"\u0000\u0000RS\u0005,\u0000\u0000S\u0018\u0001\u0000\u0000\u0000TU\u0005"+
		"h\u0000\u0000UV\u0005a\u0000\u0000VW\u0005s\u0000\u0000W\u001a\u0001\u0000"+
		"\u0000\u0000XY\u0005!\u0000\u0000YZ\u0005h\u0000\u0000Z[\u0005a\u0000"+
		"\u0000[\\\u0005s\u0000\u0000\\\u001c\u0001\u0000\u0000\u0000]^\u0005i"+
		"\u0000\u0000^_\u0005n\u0000\u0000_\u001e\u0001\u0000\u0000\u0000`a\u0005"+
		"!\u0000\u0000ab\u0005i\u0000\u0000bc\u0005n\u0000\u0000c \u0001\u0000"+
		"\u0000\u0000de\u0005a\u0000\u0000ef\u0005l\u0000\u0000fg\u0005i\u0000"+
		"\u0000gh\u0005a\u0000\u0000hi\u0005s\u0000\u0000i\"\u0001\u0000\u0000"+
		"\u0000jk\u0005p\u0000\u0000kl\u0005o\u0000\u0000lm\u0005l\u0000\u0000"+
		"mn\u0005i\u0000\u0000no\u0005c\u0000\u0000op\u0005y\u0000\u0000p$\u0001"+
		"\u0000\u0000\u0000qt\u0005\\\u0000\u0000ru\u0007\u0000\u0000\u0000su\u0003"+
		"7\u001b\u0000tr\u0001\u0000\u0000\u0000ts\u0001\u0000\u0000\u0000u&\u0001"+
		"\u0000\u0000\u0000v{\u0005\"\u0000\u0000wz\u0003%\u0012\u0000xz\u0003"+
		"5\u001a\u0000yw\u0001\u0000\u0000\u0000yx\u0001\u0000\u0000\u0000z}\u0001"+
		"\u0000\u0000\u0000{y\u0001\u0000\u0000\u0000{|\u0001\u0000\u0000\u0000"+
		"|~\u0001\u0000\u0000\u0000}{\u0001\u0000\u0000\u0000~\u007f\u0005\"\u0000"+
		"\u0000\u007f(\u0001\u0000\u0000\u0000\u0080\u0081\u0005/\u0000\u0000\u0081"+
		"\u0082\u0005*\u0000\u0000\u0082\u0086\u0001\u0000\u0000\u0000\u0083\u0085"+
		"\t\u0000\u0000\u0000\u0084\u0083\u0001\u0000\u0000\u0000\u0085\u0088\u0001"+
		"\u0000\u0000\u0000\u0086\u0087\u0001\u0000\u0000\u0000\u0086\u0084\u0001"+
		"\u0000\u0000\u0000\u0087\u0089\u0001\u0000\u0000\u0000\u0088\u0086\u0001"+
		"\u0000\u0000\u0000\u0089\u008a\u0005*\u0000\u0000\u008a\u008b\u0005/\u0000"+
		"\u0000\u008b\u008c\u0001\u0000\u0000\u0000\u008c\u008d\u0006\u0014\u0000"+
		"\u0000\u008d*\u0001\u0000\u0000\u0000\u008e\u008f\u0005\'\u0000\u0000"+
		"\u008f,\u0001\u0000\u0000\u0000\u0090\u0094\u0007\u0001\u0000\u0000\u0091"+
		"\u0093\u0007\u0002\u0000\u0000\u0092\u0091\u0001\u0000\u0000\u0000\u0093"+
		"\u0096\u0001\u0000\u0000\u0000\u0094\u0092\u0001\u0000\u0000\u0000\u0094"+
		"\u0095\u0001\u0000\u0000\u0000\u0095.\u0001\u0000\u0000\u0000\u0096\u0094"+
		"\u0001\u0000\u0000\u0000\u0097\u0098\u0005\'\u0000\u0000\u0098\u0099\u0003"+
		"-\u0016\u0000\u0099\u009a\u0005\'\u0000\u0000\u009a0\u0001\u0000\u0000"+
		"\u0000\u009b\u009f\u0007\u0001\u0000\u0000\u009c\u009e\u0007\u0002\u0000"+
		"\u0000\u009d\u009c\u0001\u0000\u0000\u0000\u009e\u00a1\u0001\u0000\u0000"+
		"\u0000\u009f\u009d\u0001\u0000\u0000\u0000\u009f\u00a0\u0001\u0000\u0000"+
		"\u0000\u00a0\u00a2\u0001\u0000\u0000\u0000\u00a1\u009f\u0001\u0000\u0000"+
		"\u0000\u00a2\u00a3\u0003\u0003\u0001\u0000\u00a32\u0001\u0000\u0000\u0000"+
		"\u00a4\u00a8\u0007\u0003\u0000\u0000\u00a5\u00a7\u0007\u0004\u0000\u0000"+
		"\u00a6\u00a5\u0001\u0000\u0000\u0000\u00a7\u00aa\u0001\u0000\u0000\u0000"+
		"\u00a8\u00a6\u0001\u0000\u0000\u0000\u00a8\u00a9\u0001\u0000\u0000\u0000"+
		"\u00a94\u0001\u0000\u0000\u0000\u00aa\u00a8\u0001\u0000\u0000\u0000\u00ab"+
		"\u00ac\b\u0005\u0000\u0000\u00ac6\u0001\u0000\u0000\u0000\u00ad\u00ae"+
		"\u0005u\u0000\u0000\u00ae\u00af\u00039\u001c\u0000\u00af\u00b0\u00039"+
		"\u001c\u0000\u00b0\u00b1\u00039\u001c\u0000\u00b1\u00b2\u00039\u001c\u0000"+
		"\u00b28\u0001\u0000\u0000\u0000\u00b3\u00b4\u0007\u0006\u0000\u0000\u00b4"+
		":\u0001\u0000\u0000\u0000\b\u0000ty{\u0086\u0094\u009f\u00a8\u0001\u0006"+
		"\u0000\u0000";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}