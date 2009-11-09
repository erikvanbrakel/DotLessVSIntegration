using LessProject.DotLessIntegration.LexerSrc;
using Microsoft.VisualStudio.Package;

namespace LessProject.DotLessIntegration
{
    public class DotLessScanner : IScanner
    {
        private Lexer _lex;

        public void SetSource(string source, int offset)
        {
            _lex = new Lexer(new StringCharacterBuffer(source.Substring(offset), 3));
        }

        public bool ScanTokenAndProvideInfoAboutIt(TokenInfo tokenInfo, ref int state)
        {
            var token = _lex.GetNextToken();
            switch(token.Kind)
            {
                case TokenKind.EOF:
                    return false;
                case TokenKind.LEFTCURLY:
                case TokenKind.RIGHTCURLY:
                    tokenInfo.Color = TokenColor.String;
                    tokenInfo.Type = TokenType.String;
                    tokenInfo.Trigger = TokenTriggers.None;
                    tokenInfo.StartIndex = token.StartIndex;
                    tokenInfo.EndIndex = token.EndIndex;
                    break;
                case TokenKind.CLASS:
                case TokenKind.IDENTIFIER:
                case TokenKind.VARIABLE:
                    if (tokenInfo.EndIndex == token.Text.Length) return false;
                    tokenInfo.Color = TokenColor.Keyword;
                    tokenInfo.Type = TokenType.Keyword;
                    tokenInfo.Trigger = TokenTriggers.None;
                    tokenInfo.StartIndex = token.StartIndex;
                    tokenInfo.EndIndex = token.EndIndex;
                    break;
                case TokenKind.COLON:
                    tokenInfo.Trigger = TokenTriggers.MethodTip;
                    break;
            }
            return true;
        }
    }
}