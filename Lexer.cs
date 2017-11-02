
using System;
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
        public int StartIndex { get; }
        public int EndIndex { get; }
        public Token( TType t, int si, int ei, params string[] values )
        {
            Type = t;
            Values = values; 
            StartIndex = si;
            EndIndex = ei;
        }
    }

    public class Lexer
    {
        private readonly string _text;
        private int _index;
        public Lexer( string text )
        {
            _text = text;
            _index = 0;
        }
        private char Previous => _text[_index - 1];
        private char Current => _text[_index];
        private bool EndText => _text.Length <= _index;
        private void Next() // TODO need to handle the end of file case
        {
            _index++;    
        }
        private bool Try(Func<char, bool> p)
        {
            if ( p(Current) )
            {
                Next();
                return true;
            }
            return false;
        }
        private bool Try(char c)
        {
            if ( c == Current )
            {
                Next();
                return true;
            }
            return false;
        }
        private void Is(Func<char, bool> p)
        {
            if ( !p(Current) )
            {
                throw new Exception( "error" ); // TODO error handling message
            }
            Next();
        }
        private void Is(char c)
        {
            if ( c != Current )
            {
                throw new Exception( "error" ); // TODO error handling message
            }
        }

        private Token Base10Int( char init )
        {
            var start = _index - 1;
            var ds = new List<char> { init }; 
            
            while( !EndText && Try( Char.IsDigit ) )
            {
                ds.Add( Previous );
            }
            
            var end = _index;
            return new Token( TType.Int, start, end, new string( ds.ToArray() ) );
        }
        
        public IEnumerable<Token> Lex()
        {
            while ( !EndText )
            {
                if( Try( Char.IsDigit ) )
                {
                    yield return Base10Int( Previous );
                }
            }
            // TODO will need to handle white space
            yield break;
        }
    }
}
