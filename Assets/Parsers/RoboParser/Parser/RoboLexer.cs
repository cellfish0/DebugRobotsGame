//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\User\source\repos\ConsoleApp2\ConsoleApp2\Test\Robo.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace ConsoleApp2.Test {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class RoboLexer : Lexer {
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, COMMENT=7, MEMBER_ACCESS=8, 
		BOOL_OPERATOR=9, ASSIGN_OPERATOR=10, NEGATION_OPERATOR=11, EQUAL=12, NOT_EQUAL=13, 
		MORE_=14, LESS_=15, MOREOREQUAL=16, LESSOREQUAL=17, COMMENT_START=18, 
		DIVIDE=19, INTEGER=20, FLOAT=21, MINUS=22, STRING=23, BOOL=24, NULL=25, 
		OP_BRACKET=26, CL_BRACKET=27, NEWLINE=28, WS=29, WHILE=30, IF=31, ELSE=32, 
		GLOBAL_KEYWORD=33, IDENTIFIER=34;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "COMMENT", "MEMBER_ACCESS", 
		"BOOL_OPERATOR", "ASSIGN_OPERATOR", "NEGATION_OPERATOR", "EQUAL", "NOT_EQUAL", 
		"MORE_", "LESS_", "MOREOREQUAL", "LESSOREQUAL", "COMMENT_START", "DIVIDE", 
		"INTEGER", "FLOAT", "MINUS", "STRING", "BOOL", "NULL", "OP_BRACKET", "CL_BRACKET", 
		"NEWLINE", "WS", "WHILE", "IF", "ELSE", "GLOBAL_KEYWORD", "IDENTIFIER"
	};


	public RoboLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, "'('", "')'", "','", "'*'", "'%'", "'+'", null, "'.'", null, null, 
		null, "'=='", "'!='", "'>'", "'<'", "'>='", "'<='", "'//'", "'/'", null, 
		null, "'-'", null, null, "'null'", "'{'", "'}'", null, null, "'while'", 
		"'if'", "'else'", "'global'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, "COMMENT", "MEMBER_ACCESS", 
		"BOOL_OPERATOR", "ASSIGN_OPERATOR", "NEGATION_OPERATOR", "EQUAL", "NOT_EQUAL", 
		"MORE_", "LESS_", "MOREOREQUAL", "LESSOREQUAL", "COMMENT_START", "DIVIDE", 
		"INTEGER", "FLOAT", "MINUS", "STRING", "BOOL", "NULL", "OP_BRACKET", "CL_BRACKET", 
		"NEWLINE", "WS", "WHILE", "IF", "ELSE", "GLOBAL_KEYWORD", "IDENTIFIER"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[System.Obsolete("Use Vocabulary instead.")]
	public static readonly string[] tokenNames = GenerateTokenNames(DefaultVocabulary, _SymbolicNames.Length);

	private static string[] GenerateTokenNames(IVocabulary vocabulary, int length) {
		string[] tokenNames = new string[length];
		for (int i = 0; i < tokenNames.Length; i++) {
			tokenNames[i] = vocabulary.GetLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = vocabulary.GetSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}

		return tokenNames;
	}

	[System.Obsolete("Use IRecognizer.Vocabulary instead.")]
	public override string[] TokenNames
	{
		get
		{
			return tokenNames;
		}
	}

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Robo.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2$\xEB\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x3\x2\x3\x2\x3\x3\x3\x3\x3\x4\x3\x4\x3\x5\x3\x5\x3"+
		"\x6\x3\x6\x3\a\x3\a\x3\b\x3\b\a\bV\n\b\f\b\xE\bY\v\b\x3\b\x3\b\x3\t\x3"+
		"\t\x3\n\x3\n\x3\n\x3\n\x3\n\x5\n\x64\n\n\x3\v\x3\v\x3\v\x5\vi\n\v\x3\f"+
		"\x3\f\x3\f\x3\f\x5\fo\n\f\x3\r\x3\r\x3\r\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF"+
		"\x3\x10\x3\x10\x3\x11\x3\x11\x3\x11\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13"+
		"\x3\x13\x3\x14\x3\x14\x3\x15\x5\x15\x87\n\x15\x3\x15\x6\x15\x8A\n\x15"+
		"\r\x15\xE\x15\x8B\x3\x16\x5\x16\x8F\n\x16\x3\x16\x6\x16\x92\n\x16\r\x16"+
		"\xE\x16\x93\x3\x16\x3\x16\x6\x16\x98\n\x16\r\x16\xE\x16\x99\x3\x17\x3"+
		"\x17\x3\x18\x3\x18\a\x18\xA0\n\x18\f\x18\xE\x18\xA3\v\x18\x3\x18\x3\x18"+
		"\x3\x18\a\x18\xA8\n\x18\f\x18\xE\x18\xAB\v\x18\x3\x18\x5\x18\xAE\n\x18"+
		"\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x5\x19"+
		"\xB9\n\x19\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1C\x3"+
		"\x1C\x3\x1D\x3\x1D\x3\x1D\x5\x1D\xC7\n\x1D\x3\x1E\x6\x1E\xCA\n\x1E\r\x1E"+
		"\xE\x1E\xCB\x3\x1E\x3\x1E\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3"+
		" \x3 \x3 \x3!\x3!\x3!\x3!\x3!\x3\"\x3\"\x3\"\x3\"\x3\"\x3\"\x3\"\x3#\x3"+
		"#\a#\xE7\n#\f#\xE#\xEA\v#\x2\x2\x2$\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6"+
		"\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B"+
		"\x2\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16"+
		"+\x2\x17-\x2\x18/\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39"+
		"\x2\x1E;\x2\x1F=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$\x3\x2\t\x4\x2\f\f\xF"+
		"\xF\x3\x2\x32;\x3\x2$$\x3\x2))\x4\x2\v\v\"\"\x5\x2\x43\\\x61\x61\x63|"+
		"\x6\x2\x32;\x43\\\x61\x61\x63|\xFA\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2"+
		"\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2"+
		"\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2"+
		"\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3"+
		"\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2"+
		"\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2"+
		"\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2"+
		"\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2"+
		"\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2\x2\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2"+
		"\x2\x2\x3G\x3\x2\x2\x2\x5I\x3\x2\x2\x2\aK\x3\x2\x2\x2\tM\x3\x2\x2\x2\v"+
		"O\x3\x2\x2\x2\rQ\x3\x2\x2\x2\xFS\x3\x2\x2\x2\x11\\\x3\x2\x2\x2\x13\x63"+
		"\x3\x2\x2\x2\x15h\x3\x2\x2\x2\x17n\x3\x2\x2\x2\x19p\x3\x2\x2\x2\x1Bs\x3"+
		"\x2\x2\x2\x1Dv\x3\x2\x2\x2\x1Fx\x3\x2\x2\x2!z\x3\x2\x2\x2#}\x3\x2\x2\x2"+
		"%\x80\x3\x2\x2\x2\'\x83\x3\x2\x2\x2)\x86\x3\x2\x2\x2+\x8E\x3\x2\x2\x2"+
		"-\x9B\x3\x2\x2\x2/\xAD\x3\x2\x2\x2\x31\xB8\x3\x2\x2\x2\x33\xBA\x3\x2\x2"+
		"\x2\x35\xBF\x3\x2\x2\x2\x37\xC1\x3\x2\x2\x2\x39\xC6\x3\x2\x2\x2;\xC9\x3"+
		"\x2\x2\x2=\xCF\x3\x2\x2\x2?\xD5\x3\x2\x2\x2\x41\xD8\x3\x2\x2\x2\x43\xDD"+
		"\x3\x2\x2\x2\x45\xE4\x3\x2\x2\x2GH\a*\x2\x2H\x4\x3\x2\x2\x2IJ\a+\x2\x2"+
		"J\x6\x3\x2\x2\x2KL\a.\x2\x2L\b\x3\x2\x2\x2MN\a,\x2\x2N\n\x3\x2\x2\x2O"+
		"P\a\'\x2\x2P\f\x3\x2\x2\x2QR\a-\x2\x2R\xE\x3\x2\x2\x2SW\x5%\x13\x2TV\n"+
		"\x2\x2\x2UT\x3\x2\x2\x2VY\x3\x2\x2\x2WU\x3\x2\x2\x2WX\x3\x2\x2\x2XZ\x3"+
		"\x2\x2\x2YW\x3\x2\x2\x2Z[\b\b\x2\x2[\x10\x3\x2\x2\x2\\]\a\x30\x2\x2]\x12"+
		"\x3\x2\x2\x2^_\a\x63\x2\x2_`\ap\x2\x2`\x64\a\x66\x2\x2\x61\x62\aq\x2\x2"+
		"\x62\x64\at\x2\x2\x63^\x3\x2\x2\x2\x63\x61\x3\x2\x2\x2\x64\x14\x3\x2\x2"+
		"\x2\x65i\a?\x2\x2\x66g\a>\x2\x2gi\a/\x2\x2h\x65\x3\x2\x2\x2h\x66\x3\x2"+
		"\x2\x2i\x16\x3\x2\x2\x2jk\ap\x2\x2kl\aq\x2\x2lo\av\x2\x2mo\a#\x2\x2nj"+
		"\x3\x2\x2\x2nm\x3\x2\x2\x2o\x18\x3\x2\x2\x2pq\a?\x2\x2qr\a?\x2\x2r\x1A"+
		"\x3\x2\x2\x2st\a#\x2\x2tu\a?\x2\x2u\x1C\x3\x2\x2\x2vw\a@\x2\x2w\x1E\x3"+
		"\x2\x2\x2xy\a>\x2\x2y \x3\x2\x2\x2z{\a@\x2\x2{|\a?\x2\x2|\"\x3\x2\x2\x2"+
		"}~\a>\x2\x2~\x7F\a?\x2\x2\x7F$\x3\x2\x2\x2\x80\x81\a\x31\x2\x2\x81\x82"+
		"\a\x31\x2\x2\x82&\x3\x2\x2\x2\x83\x84\a\x31\x2\x2\x84(\x3\x2\x2\x2\x85"+
		"\x87\x5-\x17\x2\x86\x85\x3\x2\x2\x2\x86\x87\x3\x2\x2\x2\x87\x89\x3\x2"+
		"\x2\x2\x88\x8A\t\x3\x2\x2\x89\x88\x3\x2\x2\x2\x8A\x8B\x3\x2\x2\x2\x8B"+
		"\x89\x3\x2\x2\x2\x8B\x8C\x3\x2\x2\x2\x8C*\x3\x2\x2\x2\x8D\x8F\x5-\x17"+
		"\x2\x8E\x8D\x3\x2\x2\x2\x8E\x8F\x3\x2\x2\x2\x8F\x91\x3\x2\x2\x2\x90\x92"+
		"\t\x3\x2\x2\x91\x90\x3\x2\x2\x2\x92\x93\x3\x2\x2\x2\x93\x91\x3\x2\x2\x2"+
		"\x93\x94\x3\x2\x2\x2\x94\x95\x3\x2\x2\x2\x95\x97\a\x30\x2\x2\x96\x98\t"+
		"\x3\x2\x2\x97\x96\x3\x2\x2\x2\x98\x99\x3\x2\x2\x2\x99\x97\x3\x2\x2\x2"+
		"\x99\x9A\x3\x2\x2\x2\x9A,\x3\x2\x2\x2\x9B\x9C\a/\x2\x2\x9C.\x3\x2\x2\x2"+
		"\x9D\xA1\a$\x2\x2\x9E\xA0\n\x4\x2\x2\x9F\x9E\x3\x2\x2\x2\xA0\xA3\x3\x2"+
		"\x2\x2\xA1\x9F\x3\x2\x2\x2\xA1\xA2\x3\x2\x2\x2\xA2\xA4\x3\x2\x2\x2\xA3"+
		"\xA1\x3\x2\x2\x2\xA4\xAE\a$\x2\x2\xA5\xA9\a)\x2\x2\xA6\xA8\n\x5\x2\x2"+
		"\xA7\xA6\x3\x2\x2\x2\xA8\xAB\x3\x2\x2\x2\xA9\xA7\x3\x2\x2\x2\xA9\xAA\x3"+
		"\x2\x2\x2\xAA\xAC\x3\x2\x2\x2\xAB\xA9\x3\x2\x2\x2\xAC\xAE\a)\x2\x2\xAD"+
		"\x9D\x3\x2\x2\x2\xAD\xA5\x3\x2\x2\x2\xAE\x30\x3\x2\x2\x2\xAF\xB0\av\x2"+
		"\x2\xB0\xB1\at\x2\x2\xB1\xB2\aw\x2\x2\xB2\xB9\ag\x2\x2\xB3\xB4\ah\x2\x2"+
		"\xB4\xB5\a\x63\x2\x2\xB5\xB6\an\x2\x2\xB6\xB7\au\x2\x2\xB7\xB9\ag\x2\x2"+
		"\xB8\xAF\x3\x2\x2\x2\xB8\xB3\x3\x2\x2\x2\xB9\x32\x3\x2\x2\x2\xBA\xBB\a"+
		"p\x2\x2\xBB\xBC\aw\x2\x2\xBC\xBD\an\x2\x2\xBD\xBE\an\x2\x2\xBE\x34\x3"+
		"\x2\x2\x2\xBF\xC0\a}\x2\x2\xC0\x36\x3\x2\x2\x2\xC1\xC2\a\x7F\x2\x2\xC2"+
		"\x38\x3\x2\x2\x2\xC3\xC4\a\xF\x2\x2\xC4\xC7\a\f\x2\x2\xC5\xC7\t\x2\x2"+
		"\x2\xC6\xC3\x3\x2\x2\x2\xC6\xC5\x3\x2\x2\x2\xC7:\x3\x2\x2\x2\xC8\xCA\t"+
		"\x6\x2\x2\xC9\xC8\x3\x2\x2\x2\xCA\xCB\x3\x2\x2\x2\xCB\xC9\x3\x2\x2\x2"+
		"\xCB\xCC\x3\x2\x2\x2\xCC\xCD\x3\x2\x2\x2\xCD\xCE\b\x1E\x2\x2\xCE<\x3\x2"+
		"\x2\x2\xCF\xD0\ay\x2\x2\xD0\xD1\aj\x2\x2\xD1\xD2\ak\x2\x2\xD2\xD3\an\x2"+
		"\x2\xD3\xD4\ag\x2\x2\xD4>\x3\x2\x2\x2\xD5\xD6\ak\x2\x2\xD6\xD7\ah\x2\x2"+
		"\xD7@\x3\x2\x2\x2\xD8\xD9\ag\x2\x2\xD9\xDA\an\x2\x2\xDA\xDB\au\x2\x2\xDB"+
		"\xDC\ag\x2\x2\xDC\x42\x3\x2\x2\x2\xDD\xDE\ai\x2\x2\xDE\xDF\an\x2\x2\xDF"+
		"\xE0\aq\x2\x2\xE0\xE1\a\x64\x2\x2\xE1\xE2\a\x63\x2\x2\xE2\xE3\an\x2\x2"+
		"\xE3\x44\x3\x2\x2\x2\xE4\xE8\t\a\x2\x2\xE5\xE7\t\b\x2\x2\xE6\xE5\x3\x2"+
		"\x2\x2\xE7\xEA\x3\x2\x2\x2\xE8\xE6\x3\x2\x2\x2\xE8\xE9\x3\x2\x2\x2\xE9"+
		"\x46\x3\x2\x2\x2\xEA\xE8\x3\x2\x2\x2\x13\x2W\x63hn\x86\x8B\x8E\x93\x99"+
		"\xA1\xA9\xAD\xB8\xC6\xCB\xE8\x3\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace ConsoleApp2.Test
