using System.Linq;
using LessProject.DotLessIntegration.LexerWithPegSupport;
using Microsoft.VisualStudio.Package;

namespace LessProject.DotLessIntegration
{
    public class DotLessScanner : IScanner
    {
        private Lexer _lex;

        public void SetSource(string source, int offset)
        {
            _lex = new Lexer(source.Substring(offset));
        }

        public bool ScanTokenAndProvideInfoAboutIt(TokenInfo tokenInfo, ref int state)
        {
            var token = _lex.GetNextToken();
            if (token == null) return false;
            switch (token.TokenType)
            {
                case nLess.EnLess.LeftCurly:
                case nLess.EnLess.RightCurly:
                case nLess.EnLess.LeftSquare:
                case nLess.EnLess.RightSquare:
                case nLess.EnLess.SemiColon:
                case nLess.EnLess.Comma:
                    tokenInfo.Color = TokenColor.Identifier;
                    tokenInfo.Type = TokenType.Identifier;
                    tokenInfo.Trigger = TokenTriggers.None;
                    tokenInfo.StartIndex = token.Start;
                    tokenInfo.EndIndex = token.End;
                    state = (int)States.Default;
                    break;
                case nLess.EnLess.Colon:
                    state = (int) States.DefiningPropertyValue;
                    tokenInfo.Color = TokenColor.Identifier;
                    tokenInfo.Type = TokenType.Identifier;
                    tokenInfo.Trigger = TokenTriggers.MethodTip;
                    tokenInfo.StartIndex = token.Start;
                    tokenInfo.EndIndex = token.End;
                    break;
                case nLess.EnLess.Class:
                case nLess.EnLess.Id:
                case nLess.EnLess.Variable:
                    tokenInfo.Color = TokenColor.String;
                    tokenInfo.Type = TokenType.String;
                    tokenInfo.Trigger = TokenTriggers.None;
                    tokenInfo.StartIndex = token.Start;
                    tokenInfo.EndIndex = token.End;
                    state = (int)States.Default;
                    break;
                case nLess.EnLess.Ident:
                    if (state == (int)States.DefiningPropertyValue)
                    {
                        tokenInfo.Color = TokenColor.Keyword;
                        tokenInfo.Type = TokenType.Keyword;
                    }
                    else
                    {
                        tokenInfo.Color = TokenColor.String;
                        tokenInfo.Type = TokenType.String;
                    }
                    tokenInfo.Trigger = TokenTriggers.None;
                    tokenInfo.StartIndex = token.Start;
                    tokenInfo.EndIndex = token.End;
                    break; 
            }
            
            return true;
        }
        enum States
        {
            Default = 1,
            DefiningPropertyValue = 2
        }
    }
}