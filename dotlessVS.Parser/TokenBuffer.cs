using System;
using TimeStamper.DSL;

namespace TimeStamper.DSL
{
    public class TokenBuffer
    {
        private readonly Token[] buf;
        private readonly int size;
        private readonly Lexer lexer;


        public TokenBuffer(ICharacterBuffer characterBuffer, int bufferSize)
            : this(new Lexer(characterBuffer), bufferSize)
        {  
        }

        public TokenBuffer(Lexer lex, int bufferSize)
        {
            size = bufferSize;
            buf = new Token[bufferSize];
            lexer = lex;
            InitBuffer(bufferSize);
        }

        private void InitBuffer(int bufferSize)
        {
            try{
                for (var i = 0; i < bufferSize; i++)
                    buf[i] = lexer.GetNextToken();
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Peek at position in token buffer
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Token Peek(int pos)
        {
            if (pos >= 1 && pos <= size)
                return buf[pos - 1];

            return null;
        }

        /// <summary>
        /// Load X tokens into the buffer
        /// </summary>
        public void Load(int length)
        {
            if (length > size) length = size;
            for (var i = 1; i <= length; i++)
                Load();
        }

        /// <summary>
        /// Load next token
        /// </summary>
        public void Load()
        {
            for (var i = 0; i < size - 1; i++)
                buf[i] = buf[i + 1];

            try{
                buf[size - 1] = lexer.GetNextToken();
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}