
using System.Collections.Generic;
// the idea will be to parse as little as possible given the current lexing context
// will probably need some sort of lexet or something class to contain sub lexers

namespace Dalet
{
    public enum TType { Int, String }

    public class Token
    {
        public IEnumerable<string> Values { get; }
        public TType Type { get; }
        public Token( TType t, params string[] values )
        {
            Type = t;
            Values = values; 
        }
    }

    public class Lexer
    {
    }
}
