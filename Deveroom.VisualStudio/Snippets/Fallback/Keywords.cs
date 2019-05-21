﻿namespace Deveroom.VisualStudio.Snippets.Fallback
{
    // imported from SpecFlow v2.4
    using System.Collections.Generic;

    public static class Keywords
    {
        private static List<string> FSharp { get; } = new List<string>
        {
            "abstract",
"and",
"as",
"assert",
"base",
"begin",
"class",
"default",
"delegate",
"do",
"done",
"downcast",
"downto",
"elif",
"else",
"end",
"exception",
"extern",
"FALSE",
"finally",
"for",
"fun",
"function",
"global",
"if",
"in",
"inherit",
"inline",
"interface",
"internal",
"lazy",
"let",
"let!",
"match",
"member",
"module",
"mutable",
"namespace",
"new",
"not",
"null",
"of",
"open",
"or",
"override",
"private",
"public",
"rec",
"return",
"return!",
"select",
"static",
"struct",
"then",
"to",
"TRUE",
"try",
"type",
"upcast",
"use",
"use!",
"val",
"void",
"when",
"while",
"with",
"yield",
"yield!"
        };
        private static List<string> VB { get; } = new List<string>
        {
            "addhandler",
"addressof",
"alias",
"and",
"andalso",
"as",
"boolean",
"byref",
"byte",
"byval",
"call",
"case",
"catch",
"cbool",
"cbyte",
"cchar",
"cdate",
"cdec",
"cdbl",
"char",
"cint",
"class",
"clng",
"cobj",
"const",
"continue",
"csbyte",
"cshort",
"csng",
"cstr",
"ctype",
"cuint",
"culng",
"cushort",
"date",
"decimal",
"declare",
"default",
"delegate",
"dim",
"directcast",
"do",
"double",
"each",
"else",
"elseif",
"end",
"endif",
"enum",
"erase",
"error",
"event",
"exit",
"false",
"finally",
"for",
"friend",
"function",
"get",
"gettype",
"getxmlnamespace",
"global",
"gosub",
"goto",
"handles",
"if",
"implements",
"imports",
"in",
"inherits",
"integer",
"interface",
"is",
"isnot",
"let",
"lib",
"like",
"long",
"loop",
"me",
"mod",
"module",
"mustinherit",
"mustoverride",
"mybase",
"myclass",
"namespace",
"narrowing",
"new",
"next",
"not",
"nothing",
"notinheritable",
"notoverridable",
"object",
"of",
"on",
"operator",
"option",
"optional",
"or",
"orelse",
"overloads",
"overridable",
"overrides",
"paramarray",
"partial",
"private",
"property",
"protected",
"public",
"raiseevent",
"readonly",
"redim",
"rem",
"removehandler",
"resume",
"return",
"sbyte",
"select",
"set",
"shadows",
"shared",
"short",
"single",
"static",
"step",
"stop",
"string",
"structure",
"sub",
"synclock",
"then",
"throw",
"to",
"true",
"try",
"trycast",
"typeof",
"variant",
"wend",
"uinteger",
"ulong",
"ushort",
"using",
"when",
"while",
"widening",
"with",
"withevents",
"writeonly",
"xor"
        };
        private static List<string> CSharp { get; } = new List<string>()
        {
            "abstract",
            "as",
            "base",
            "bool",
            "break",
            "byte",
            "case",
            "catch",
            "char",
            "checked",
            "class",
            "const",
            "continue",
            "decimal",
            "default",
            "delegate",
            "do",
            "double",
            "else",
            "enum",
            "event",
            "explicit",
            "extern",
            "false",
            "finally",
            "fixed",
            "float",
            "for",
            "foreach",
            "goto",
            "if",
            "implicit",
            "in",
            "int",
            "interface",
            "internal",
            "is",
            "lock",
            "long",
            "namespace",
            "new",
            "null",
            "object",
            "operator",
            "out",
            "override",
            "params",
            "private",
            "protected",
            "public",
            "readonly",
            "ref",
            "return",
            "sbyte",
            "sealed",
            "short",
            "sizeof",
            "stackalloc",
            "static",
            "string",
            "struct",
            "switch",
            "this",
            "throw",
            "true",
            "try",
            "typeof",
            "uint",
            "ulong",
            "unchecked",
            "unsafe",
            "ushort",
            "using",
            "virtual",
            "void",
            "volatile",
            "while"
        };

        public static string EscapeCSharpKeyword(string argumentName)
        {
            if (CSharp.Contains(argumentName))
            {
                return $"@{argumentName}";
            }
            return argumentName;
        }

        public static string EscapeVBKeyword(string argumentName)
        {
            if (VB.Contains(argumentName.ToLower()))
            {
                return $"[{argumentName}]";
            }
            return argumentName;
        }

        public static string EscapeFSharpKeyword(string argumentName)
        {
            if (FSharp.Contains(argumentName))
            {
                return $"``{argumentName}``";
            }
            return argumentName;
        }
    }
}