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

namespace LessProject.DotLessIntegration.LexerSrc
{
    public class Token
    {
        public string Text { get; set; }
        public TokenKind Kind { get; set; }

        public Token(TokenKind kind, string text) : this(kind, text, 0,0)
        {
        }


        public Token(TokenKind kind, string text, int start, int end)
        {
            StartIndex = start;
            EndIndex = end;
            Kind = kind;
            Text = text;
        }

        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
    }
}