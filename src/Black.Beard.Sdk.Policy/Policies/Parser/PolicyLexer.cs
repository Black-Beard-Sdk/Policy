//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from PolicyLexer.g4 by ANTLR 4.13.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Bb.Policies.Parser {
using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.2")]
[System.CLSCompliant(false)]
public partial class PolicyLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		TRUE=1, FALSE=2, PLUS=3, DOT=4, COLON=5, NOT=6, PARENT_LEFT=7, PARENT_RIGHT=8, 
		BRACKET_LEFT=9, BRACKET_RIGHT=10, EQUAL=11, INEQUAL=12, OR=13, AND=14, 
		COMMA=15, HAS=16, HAS_NOT=17, IN=18, NOT_IN=19, ALIAS=20, POLICY=21, INHERIT=22, 
		STRING=23, MULTI_LINE_COMMENT=24, SINGLE_QUOTE_CODE_STRING=25, ID=26, 
		IDQUOTED=27, VARIABLE_NAME=28, IDLOWCASE=29;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"TRUE", "FALSE", "PLUS", "DOT", "COLON", "NOT", "PARENT_LEFT", "PARENT_RIGHT", 
		"BRACKET_LEFT", "BRACKET_RIGHT", "EQUAL", "INEQUAL", "OR", "AND", "COMMA", 
		"HAS", "HAS_NOT", "IN", "NOT_IN", "ALIAS", "POLICY", "INHERIT", "ESC", 
		"STRING", "MULTI_LINE_COMMENT", "SINGLE_QUOTE_CODE_STRING", "ID", "IDQUOTED", 
		"VARIABLE_NAME", "IDLOWCASE", "SAFECODEPOINT", "UNICODE", "HEX"
	};


	public PolicyLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public PolicyLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'true'", "'false'", "'+'", "'.'", "':'", "'!'", "'('", "')'", "'['", 
		"']'", "'='", "'!='", "'|'", "'&'", "','", "'has'", "'!has'", "'in'", 
		"'!in'", "'alias'", "'policy'", "'inherit'", null, null, "'''"
	};
	private static readonly string[] _SymbolicNames = {
		null, "TRUE", "FALSE", "PLUS", "DOT", "COLON", "NOT", "PARENT_LEFT", "PARENT_RIGHT", 
		"BRACKET_LEFT", "BRACKET_RIGHT", "EQUAL", "INEQUAL", "OR", "AND", "COMMA", 
		"HAS", "HAS_NOT", "IN", "NOT_IN", "ALIAS", "POLICY", "INHERIT", "STRING", 
		"MULTI_LINE_COMMENT", "SINGLE_QUOTE_CODE_STRING", "ID", "IDQUOTED", "VARIABLE_NAME", 
		"IDLOWCASE"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "PolicyLexer.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static PolicyLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,29,210,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,2,27,7,27,2,28,
		7,28,2,29,7,29,2,30,7,30,2,31,7,31,2,32,7,32,1,0,1,0,1,0,1,0,1,0,1,1,1,
		1,1,1,1,1,1,1,1,1,1,2,1,2,1,3,1,3,1,4,1,4,1,5,1,5,1,6,1,6,1,7,1,7,1,8,
		1,8,1,9,1,9,1,10,1,10,1,11,1,11,1,11,1,12,1,12,1,13,1,13,1,14,1,14,1,15,
		1,15,1,15,1,15,1,16,1,16,1,16,1,16,1,16,1,17,1,17,1,17,1,18,1,18,1,18,
		1,18,1,19,1,19,1,19,1,19,1,19,1,19,1,20,1,20,1,20,1,20,1,20,1,20,1,20,
		1,21,1,21,1,21,1,21,1,21,1,21,1,21,1,21,1,22,1,22,1,22,3,22,146,8,22,1,
		23,1,23,1,23,5,23,151,8,23,10,23,12,23,154,9,23,1,23,1,23,1,24,1,24,1,
		24,1,24,5,24,162,8,24,10,24,12,24,165,9,24,1,24,1,24,1,24,1,24,1,24,1,
		25,1,25,1,26,1,26,5,26,176,8,26,10,26,12,26,179,9,26,1,27,1,27,1,27,1,
		27,1,28,1,28,5,28,187,8,28,10,28,12,28,190,9,28,1,28,1,28,1,29,1,29,5,
		29,196,8,29,10,29,12,29,199,9,29,1,30,1,30,1,31,1,31,1,31,1,31,1,31,1,
		31,1,32,1,32,1,163,0,33,1,1,3,2,5,3,7,4,9,5,11,6,13,7,15,8,17,9,19,10,
		21,11,23,12,25,13,27,14,29,15,31,16,33,17,35,18,37,19,39,20,41,21,43,22,
		45,0,47,23,49,24,51,25,53,26,55,27,57,28,59,29,61,0,63,0,65,0,1,0,7,8,
		0,34,34,47,47,92,92,98,98,102,102,110,110,114,114,116,116,3,0,65,90,95,
		95,97,122,4,0,48,57,65,90,95,95,97,122,2,0,95,95,97,122,3,0,48,57,95,95,
		97,122,3,0,0,31,34,34,92,92,3,0,48,57,65,70,97,102,212,0,1,1,0,0,0,0,3,
		1,0,0,0,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,0,0,0,11,1,0,0,0,0,13,1,0,0,0,
		0,15,1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,21,1,0,0,0,0,23,1,0,0,0,0,25,
		1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,0,0,0,0,33,1,0,0,0,0,35,1,0,0,
		0,0,37,1,0,0,0,0,39,1,0,0,0,0,41,1,0,0,0,0,43,1,0,0,0,0,47,1,0,0,0,0,49,
		1,0,0,0,0,51,1,0,0,0,0,53,1,0,0,0,0,55,1,0,0,0,0,57,1,0,0,0,0,59,1,0,0,
		0,1,67,1,0,0,0,3,72,1,0,0,0,5,78,1,0,0,0,7,80,1,0,0,0,9,82,1,0,0,0,11,
		84,1,0,0,0,13,86,1,0,0,0,15,88,1,0,0,0,17,90,1,0,0,0,19,92,1,0,0,0,21,
		94,1,0,0,0,23,96,1,0,0,0,25,99,1,0,0,0,27,101,1,0,0,0,29,103,1,0,0,0,31,
		105,1,0,0,0,33,109,1,0,0,0,35,114,1,0,0,0,37,117,1,0,0,0,39,121,1,0,0,
		0,41,127,1,0,0,0,43,134,1,0,0,0,45,142,1,0,0,0,47,147,1,0,0,0,49,157,1,
		0,0,0,51,171,1,0,0,0,53,173,1,0,0,0,55,180,1,0,0,0,57,184,1,0,0,0,59,193,
		1,0,0,0,61,200,1,0,0,0,63,202,1,0,0,0,65,208,1,0,0,0,67,68,5,116,0,0,68,
		69,5,114,0,0,69,70,5,117,0,0,70,71,5,101,0,0,71,2,1,0,0,0,72,73,5,102,
		0,0,73,74,5,97,0,0,74,75,5,108,0,0,75,76,5,115,0,0,76,77,5,101,0,0,77,
		4,1,0,0,0,78,79,5,43,0,0,79,6,1,0,0,0,80,81,5,46,0,0,81,8,1,0,0,0,82,83,
		5,58,0,0,83,10,1,0,0,0,84,85,5,33,0,0,85,12,1,0,0,0,86,87,5,40,0,0,87,
		14,1,0,0,0,88,89,5,41,0,0,89,16,1,0,0,0,90,91,5,91,0,0,91,18,1,0,0,0,92,
		93,5,93,0,0,93,20,1,0,0,0,94,95,5,61,0,0,95,22,1,0,0,0,96,97,5,33,0,0,
		97,98,5,61,0,0,98,24,1,0,0,0,99,100,5,124,0,0,100,26,1,0,0,0,101,102,5,
		38,0,0,102,28,1,0,0,0,103,104,5,44,0,0,104,30,1,0,0,0,105,106,5,104,0,
		0,106,107,5,97,0,0,107,108,5,115,0,0,108,32,1,0,0,0,109,110,5,33,0,0,110,
		111,5,104,0,0,111,112,5,97,0,0,112,113,5,115,0,0,113,34,1,0,0,0,114,115,
		5,105,0,0,115,116,5,110,0,0,116,36,1,0,0,0,117,118,5,33,0,0,118,119,5,
		105,0,0,119,120,5,110,0,0,120,38,1,0,0,0,121,122,5,97,0,0,122,123,5,108,
		0,0,123,124,5,105,0,0,124,125,5,97,0,0,125,126,5,115,0,0,126,40,1,0,0,
		0,127,128,5,112,0,0,128,129,5,111,0,0,129,130,5,108,0,0,130,131,5,105,
		0,0,131,132,5,99,0,0,132,133,5,121,0,0,133,42,1,0,0,0,134,135,5,105,0,
		0,135,136,5,110,0,0,136,137,5,104,0,0,137,138,5,101,0,0,138,139,5,114,
		0,0,139,140,5,105,0,0,140,141,5,116,0,0,141,44,1,0,0,0,142,145,5,92,0,
		0,143,146,7,0,0,0,144,146,3,63,31,0,145,143,1,0,0,0,145,144,1,0,0,0,146,
		46,1,0,0,0,147,152,5,34,0,0,148,151,3,45,22,0,149,151,3,61,30,0,150,148,
		1,0,0,0,150,149,1,0,0,0,151,154,1,0,0,0,152,150,1,0,0,0,152,153,1,0,0,
		0,153,155,1,0,0,0,154,152,1,0,0,0,155,156,5,34,0,0,156,48,1,0,0,0,157,
		158,5,47,0,0,158,159,5,42,0,0,159,163,1,0,0,0,160,162,9,0,0,0,161,160,
		1,0,0,0,162,165,1,0,0,0,163,164,1,0,0,0,163,161,1,0,0,0,164,166,1,0,0,
		0,165,163,1,0,0,0,166,167,5,42,0,0,167,168,5,47,0,0,168,169,1,0,0,0,169,
		170,6,24,0,0,170,50,1,0,0,0,171,172,5,39,0,0,172,52,1,0,0,0,173,177,7,
		1,0,0,174,176,7,2,0,0,175,174,1,0,0,0,176,179,1,0,0,0,177,175,1,0,0,0,
		177,178,1,0,0,0,178,54,1,0,0,0,179,177,1,0,0,0,180,181,5,39,0,0,181,182,
		3,53,26,0,182,183,5,39,0,0,183,56,1,0,0,0,184,188,7,1,0,0,185,187,7,2,
		0,0,186,185,1,0,0,0,187,190,1,0,0,0,188,186,1,0,0,0,188,189,1,0,0,0,189,
		191,1,0,0,0,190,188,1,0,0,0,191,192,3,9,4,0,192,58,1,0,0,0,193,197,7,3,
		0,0,194,196,7,4,0,0,195,194,1,0,0,0,196,199,1,0,0,0,197,195,1,0,0,0,197,
		198,1,0,0,0,198,60,1,0,0,0,199,197,1,0,0,0,200,201,8,5,0,0,201,62,1,0,
		0,0,202,203,5,117,0,0,203,204,3,65,32,0,204,205,3,65,32,0,205,206,3,65,
		32,0,206,207,3,65,32,0,207,64,1,0,0,0,208,209,7,6,0,0,209,66,1,0,0,0,8,
		0,145,150,152,163,177,188,197,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace Bb.Policies.Parser
