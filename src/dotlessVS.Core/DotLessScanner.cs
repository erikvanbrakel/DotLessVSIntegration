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

                if (state == (int)States.InComment)
                {
                    if(token.TokenType == nLess.EnLess.SlashStarCommentEnd)
                        state = (int) States.Default;
                        tokenInfo.Color = TokenColor.Comment;
                        tokenInfo.Type = TokenType.Comment;
                        tokenInfo.Trigger = TokenTriggers.None;
                        tokenInfo.StartIndex = token.Start;
                        tokenInfo.EndIndex = token.End;
                }
                else
                {
                    switch (token.TokenType)
                    {
                        case nLess.EnLess.DoubleSlashComment:
                            tokenInfo.Color = TokenColor.Comment;
                            tokenInfo.Type = TokenType.Comment;
                            tokenInfo.Trigger = TokenTriggers.None;
                            tokenInfo.StartIndex = token.Start;
                            tokenInfo.EndIndex = token.End;
                            break;
                        case nLess.EnLess.SlashStarCommentStart:
                            tokenInfo.Color = TokenColor.Comment;
                            tokenInfo.Type = TokenType.Comment;
                            tokenInfo.Trigger = TokenTriggers.None;
                            tokenInfo.StartIndex = token.Start;
                            tokenInfo.EndIndex = token.End;
                            state = (int) States.InComment;
                            break;
                        case nLess.EnLess.LeftCurly:
                        case nLess.EnLess.RightCurly:
                        case nLess.EnLess.LeftSquare:
                        case nLess.EnLess.RightSquare:
                        case nLess.EnLess.SemiColon:
                        case nLess.EnLess.Comma:
                            tokenInfo.Color = TokenColor.Identifier;
                            tokenInfo.Type = TokenType.Identifier;
                            tokenInfo.Trigger = TokenTriggers.MatchBraces;
                            tokenInfo.StartIndex = token.Start;
                            tokenInfo.EndIndex = token.End;
                            state = (int) States.Default;
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
                            state = (int) States.Default;
                            break;
                        case nLess.EnLess.Ident:
                            if (state == (int) States.DefiningPropertyValue)
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
            }
            return true;
        }
        enum States
        {
            Default = 1,
            DefiningPropertyValue = 2,
            InComment
        }
    }
}