using System.Text;


namespace LessProject.DotLessIntegration.LexerSrc
{
    public class Lexer
    {
        /// <summary>
        /// Exposed for unit testing
        /// </summary>
        internal ICharacterBuffer buffer;

        public Lexer(ICharacterBuffer charBuffer)
        {
            buffer = charBuffer;
        }

        /// <summary>
        /// Gets the next Token
        /// </summary>
        /// <returns></returns>
        public Token GetNextToken()
        {
            Token returnToken = null;
            
            while (true)
            {
                int start = 0;
                string ident = string.Empty;
                switch (buffer.Peek(1))
                {
                    case ' ':
                        buffer.Load();
                        break;
                    case -1:
                        returnToken = new Token(TokenKind.EOF, "<eof>");
                        break;
                    case '{':
                        returnToken = new Token(TokenKind.LEFTCURLY, "<leftcurly>", buffer.CurrentPos - 1, buffer.CurrentPos);
                        buffer.Load();
                        break;
                    case '}':
                        returnToken = new Token(TokenKind.RIGHTCURLY, "<rightcurly>", buffer.CurrentPos - 1, buffer.CurrentPos);
                        buffer.Load();
                        break;
                    case ';':
                        returnToken = new Token(TokenKind.SEMICOLON, "<semicolon>", buffer.CurrentPos - 1, buffer.CurrentPos);
                        buffer.Load();
                        break;
                    case ':':
                        returnToken = new Token(TokenKind.COLON, "<:>", buffer.CurrentPos - 1, buffer.CurrentPos);
                        buffer.Load();
                        break;
                    case '@':
                        buffer.Load();
                        start = buffer.CurrentPos;
                        ident = ReadIdentifierToEnd();
                        returnToken = new Token(TokenKind.VARIABLE, ident, start - 1, start - 1 + ident.Length); buffer.Load();
                        break;
                    case '.':
                        buffer.Load();
                        start = buffer.CurrentPos;
                        ident = ReadIdentifierToEnd();
                        returnToken = new Token(TokenKind.CLASS, ident, start - 1, start - 1 + ident.Length);
                        buffer.Load();
                        break;
                    case '#':
                        buffer.Load();
                        start = buffer.CurrentPos;
                        ident = ReadIdentifierToEnd();
                        returnToken = new Token(TokenKind.IDENTIFIER, ident, start-1, start - 1 + ident.Length); buffer.Load();
                        break;
                    default:
                        if (char.IsNumber((char)buffer.Peek(1)))
                        {
                            return GetNumber();
                        }
                        buffer.Load();
                        break;
                }

                if (returnToken != null) return returnToken;
            }
        }

        /// <summary>
        /// Get keyword identifier/datetime variable
        /// </summary>
        /// <returns></returns>
        private Token GetIdentifier()
        {
            Token returnToken = null;
            var identifier = ReadIdentifierToEnd();

            if (identifier.StartsWith("@"))
                returnToken = new Token(TokenKind.VARIABLE, identifier);
   
            return returnToken;
        }
        /// <summary>
        /// Read Identifier to the end
        /// </summary>
        /// <returns></returns>
        private string ReadIdentifierToEnd()
        {
            var s = new StringBuilder();
            while (char.IsLetter((char)buffer.Peek(1)) || char.IsNumber((char)buffer.Peek(1)) || (char)buffer.Peek(1) == '_' || (char)buffer.Peek(1) == '/' || (char)buffer.Peek(1) == '-' || (char)buffer.Peek(1) == '.')
            {
                s.Append((char)buffer.Peek(1));
                buffer.Load();
            }
            return s.ToString();
        }

        /// <summary>
        /// Read the full number
        /// </summary>
        /// <returns></returns>
        private Token GetNumber()
        {
            var s = new StringBuilder();
            var c = (char)buffer.Peek(1);
            while (char.IsNumber(c) )
            {
                s.Append(c);
                buffer.Load();
                c = (char)buffer.Peek(1);
            }
            var stemp = s.ToString();
            return new Token(TokenKind.NUMBER, stemp);
        }
    }
}