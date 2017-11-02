
using System.Collections.Generic;

namespace Dalet.Util
{
    public class DisplayError 
    {
        private List<string> Text { get; }
        public DisplayError( string text )
        {
            Text = text;
        }

        private static IEnumerable<string> Lines( string t )
        {
            var line = UntilEndLine( t ).ToArray(); 
            
            yield return new string( line );

        }
        
        private static IEnumerable<char> UntilEndLine( string t )
        {
            for( var i = 0; i < t.Length; i++ )
            {
                if ( t.Length > i && t[i] == '\r' && t[i+1] == '\n' )
                {
                    yield return '\r';
                    yield return '\n';
                    yield break;
                }
                else if ( t[i] == '\r' )
                {
                    yield return '\r';
                    yield break;
                }
                else if ( t[i] == '\n' )
                {
                    yield return '\n';
                    yield break;
                }
                else
                {
                    yield return t[i];
                }
            }
        }

    // need to handle tabs (probably by just inserting my own?)
    // \r\n (don't put that \n on it's own line), \n


    }
}
