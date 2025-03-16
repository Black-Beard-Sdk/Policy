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
		DOT=1, COLON=2, NOT=3, PARENT_LEFT=4, PARENT_RIGHT=5, BRACKET_LEFT=6, 
		BRACKET_RIGHT=7, EQUAL=8, INEQUAL=9, OR=10, AND=11, COMMA=12, HAS=13, 
		HAS_NOT=14, IN=15, NOT_IN=16, ALIAS=17, POLICY=18, INHERIT=19, STRING=20, 
		MULTI_LINE_COMMENT=21, SINGLE_QUOTE_CODE_STRING=22, ID=23, IDQUOTED=24, 
		VARIABLE_NAME=25, IDLOWCASE=26;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"DOT", "COLON", "NOT", "PARENT_LEFT", "PARENT_RIGHT", "BRACKET_LEFT", 
		"BRACKET_RIGHT", "EQUAL", "INEQUAL", "OR", "AND", "COMMA", "HAS", "HAS_NOT", 
		"IN", "NOT_IN", "ALIAS", "POLICY", "INHERIT", "ESC", "STRING", "MULTI_LINE_COMMENT", 
		"SINGLE_QUOTE_CODE_STRING", "ID", "IDQUOTED", "VARIABLE_NAME", "IDLOWCASE", 
		"SAFECODEPOINT", "UNICODE", "HEX"
	};


	public PolicyLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public PolicyLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'.'", "':'", "'!'", "'('", "')'", "'['", "']'", "'='", "'!='", 
		"'|'", "'&'", "','", "'has'", "'!has'", "'in'", "'!in'", "'alias'", "'policy'", 
		"'inherit'", null, null, "'''"
	};
	private static readonly string[] _SymbolicNames = {
		null, "DOT", "COLON", "NOT", "PARENT_LEFT", "PARENT_RIGHT", "BRACKET_LEFT", 
		"BRACKET_RIGHT", "EQUAL", "INEQUAL", "OR", "AND", "COMMA", "HAS", "HAS_NOT", 
		"IN", "NOT_IN", "ALIAS", "POLICY", "INHERIT", "STRING", "MULTI_LINE_COMMENT", 
		"SINGLE_QUOTE_CODE_STRING", "ID", "IDQUOTED", "VARIABLE_NAME", "IDLOWCASE"
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
		4,0,26,191,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,2,27,7,27,2,28,
		7,28,2,29,7,29,1,0,1,0,1,1,1,1,1,2,1,2,1,3,1,3,1,4,1,4,1,5,1,5,1,6,1,6,
		1,7,1,7,1,8,1,8,1,8,1,9,1,9,1,10,1,10,1,11,1,11,1,12,1,12,1,12,1,12,1,
		13,1,13,1,13,1,13,1,13,1,14,1,14,1,14,1,15,1,15,1,15,1,15,1,16,1,16,1,
		16,1,16,1,16,1,16,1,17,1,17,1,17,1,17,1,17,1,17,1,17,1,18,1,18,1,18,1,
		18,1,18,1,18,1,18,1,18,1,19,1,19,1,19,3,19,127,8,19,1,20,1,20,1,20,5,20,
		132,8,20,10,20,12,20,135,9,20,1,20,1,20,1,21,1,21,1,21,1,21,5,21,143,8,
		21,10,21,12,21,146,9,21,1,21,1,21,1,21,1,21,1,21,1,22,1,22,1,23,1,23,5,
		23,157,8,23,10,23,12,23,160,9,23,1,24,1,24,1,24,1,24,1,25,1,25,5,25,168,
		8,25,10,25,12,25,171,9,25,1,25,1,25,1,26,1,26,5,26,177,8,26,10,26,12,26,
		180,9,26,1,27,1,27,1,28,1,28,1,28,1,28,1,28,1,28,1,29,1,29,1,144,0,30,
		1,1,3,2,5,3,7,4,9,5,11,6,13,7,15,8,17,9,19,10,21,11,23,12,25,13,27,14,
		29,15,31,16,33,17,35,18,37,19,39,0,41,20,43,21,45,22,47,23,49,24,51,25,
		53,26,55,0,57,0,59,0,1,0,7,8,0,34,34,47,47,92,92,98,98,102,102,110,110,
		114,114,116,116,3,0,65,90,95,95,97,122,4,0,48,57,65,90,95,95,97,122,2,
		0,95,95,97,122,3,0,48,57,95,95,97,122,3,0,0,31,34,34,92,92,3,0,48,57,65,
		70,97,102,193,0,1,1,0,0,0,0,3,1,0,0,0,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,
		0,0,0,11,1,0,0,0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,
		21,1,0,0,0,0,23,1,0,0,0,0,25,1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,
		0,0,0,0,33,1,0,0,0,0,35,1,0,0,0,0,37,1,0,0,0,0,41,1,0,0,0,0,43,1,0,0,0,
		0,45,1,0,0,0,0,47,1,0,0,0,0,49,1,0,0,0,0,51,1,0,0,0,0,53,1,0,0,0,1,61,
		1,0,0,0,3,63,1,0,0,0,5,65,1,0,0,0,7,67,1,0,0,0,9,69,1,0,0,0,11,71,1,0,
		0,0,13,73,1,0,0,0,15,75,1,0,0,0,17,77,1,0,0,0,19,80,1,0,0,0,21,82,1,0,
		0,0,23,84,1,0,0,0,25,86,1,0,0,0,27,90,1,0,0,0,29,95,1,0,0,0,31,98,1,0,
		0,0,33,102,1,0,0,0,35,108,1,0,0,0,37,115,1,0,0,0,39,123,1,0,0,0,41,128,
		1,0,0,0,43,138,1,0,0,0,45,152,1,0,0,0,47,154,1,0,0,0,49,161,1,0,0,0,51,
		165,1,0,0,0,53,174,1,0,0,0,55,181,1,0,0,0,57,183,1,0,0,0,59,189,1,0,0,
		0,61,62,5,46,0,0,62,2,1,0,0,0,63,64,5,58,0,0,64,4,1,0,0,0,65,66,5,33,0,
		0,66,6,1,0,0,0,67,68,5,40,0,0,68,8,1,0,0,0,69,70,5,41,0,0,70,10,1,0,0,
		0,71,72,5,91,0,0,72,12,1,0,0,0,73,74,5,93,0,0,74,14,1,0,0,0,75,76,5,61,
		0,0,76,16,1,0,0,0,77,78,5,33,0,0,78,79,5,61,0,0,79,18,1,0,0,0,80,81,5,
		124,0,0,81,20,1,0,0,0,82,83,5,38,0,0,83,22,1,0,0,0,84,85,5,44,0,0,85,24,
		1,0,0,0,86,87,5,104,0,0,87,88,5,97,0,0,88,89,5,115,0,0,89,26,1,0,0,0,90,
		91,5,33,0,0,91,92,5,104,0,0,92,93,5,97,0,0,93,94,5,115,0,0,94,28,1,0,0,
		0,95,96,5,105,0,0,96,97,5,110,0,0,97,30,1,0,0,0,98,99,5,33,0,0,99,100,
		5,105,0,0,100,101,5,110,0,0,101,32,1,0,0,0,102,103,5,97,0,0,103,104,5,
		108,0,0,104,105,5,105,0,0,105,106,5,97,0,0,106,107,5,115,0,0,107,34,1,
		0,0,0,108,109,5,112,0,0,109,110,5,111,0,0,110,111,5,108,0,0,111,112,5,
		105,0,0,112,113,5,99,0,0,113,114,5,121,0,0,114,36,1,0,0,0,115,116,5,105,
		0,0,116,117,5,110,0,0,117,118,5,104,0,0,118,119,5,101,0,0,119,120,5,114,
		0,0,120,121,5,105,0,0,121,122,5,116,0,0,122,38,1,0,0,0,123,126,5,92,0,
		0,124,127,7,0,0,0,125,127,3,57,28,0,126,124,1,0,0,0,126,125,1,0,0,0,127,
		40,1,0,0,0,128,133,5,34,0,0,129,132,3,39,19,0,130,132,3,55,27,0,131,129,
		1,0,0,0,131,130,1,0,0,0,132,135,1,0,0,0,133,131,1,0,0,0,133,134,1,0,0,
		0,134,136,1,0,0,0,135,133,1,0,0,0,136,137,5,34,0,0,137,42,1,0,0,0,138,
		139,5,47,0,0,139,140,5,42,0,0,140,144,1,0,0,0,141,143,9,0,0,0,142,141,
		1,0,0,0,143,146,1,0,0,0,144,145,1,0,0,0,144,142,1,0,0,0,145,147,1,0,0,
		0,146,144,1,0,0,0,147,148,5,42,0,0,148,149,5,47,0,0,149,150,1,0,0,0,150,
		151,6,21,0,0,151,44,1,0,0,0,152,153,5,39,0,0,153,46,1,0,0,0,154,158,7,
		1,0,0,155,157,7,2,0,0,156,155,1,0,0,0,157,160,1,0,0,0,158,156,1,0,0,0,
		158,159,1,0,0,0,159,48,1,0,0,0,160,158,1,0,0,0,161,162,5,39,0,0,162,163,
		3,47,23,0,163,164,5,39,0,0,164,50,1,0,0,0,165,169,7,1,0,0,166,168,7,2,
		0,0,167,166,1,0,0,0,168,171,1,0,0,0,169,167,1,0,0,0,169,170,1,0,0,0,170,
		172,1,0,0,0,171,169,1,0,0,0,172,173,3,3,1,0,173,52,1,0,0,0,174,178,7,3,
		0,0,175,177,7,4,0,0,176,175,1,0,0,0,177,180,1,0,0,0,178,176,1,0,0,0,178,
		179,1,0,0,0,179,54,1,0,0,0,180,178,1,0,0,0,181,182,8,5,0,0,182,56,1,0,
		0,0,183,184,5,117,0,0,184,185,3,59,29,0,185,186,3,59,29,0,186,187,3,59,
		29,0,187,188,3,59,29,0,188,58,1,0,0,0,189,190,7,6,0,0,190,60,1,0,0,0,8,
		0,126,131,133,144,158,169,178,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace Bb.Policies.Parser
