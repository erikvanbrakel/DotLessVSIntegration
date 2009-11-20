/* Copyright 2009 dotless project, http://www.dotlesscss.com
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *     
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License. */

using System;

namespace LessProject.DotLessIntegration.LexerSrc
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