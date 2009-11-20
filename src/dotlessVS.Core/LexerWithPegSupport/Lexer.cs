using System;
using System.Collections.Generic;
using System.Linq;
using nLess;
using Peg.Base;

namespace LessProject.DotLessIntegration.LexerWithPegSupport
{
    public class Lexer
    {
        private string _source;
        private nLess.nLess _parser;
 
        public Lexer(string source)
        {
            _source = source;
            ResetParser();
        }

        private void ResetParser()
        {
            _parser = new nLess.nLess(_source, Console.Out);
            SetTokens();
        }

        private IEnumerator<Token> _tokens;
        
        public void SetTokens()
        {
            if (!_parser.Parse()) return;
            var token = GetRootToken();
            _tokens = token.Children.GetEnumerator();
        }

        public Token GetRootToken()
        {
            return new Token(_parser.GetRoot(), _source);
        }

        public Token GetNextToken()
        {
            if (_tokens != null)
            {
                _tokens.MoveNext();
                return _tokens.Current;
            }
            return null;
        }
        public class Token
        {
            public int Start { get; set; }
            public int End { get; set; }
            internal PegNode PegNode;
            public EnLess TokenType { get; private set; }
            public string Text { get; private set; }
            private string _src;

            public Token(PegNode peg, string src)
            {
                _src = src;
                PegNode = peg;
                TokenType = (EnLess)Enum.Parse(typeof(EnLess), PegNode.id_.ToString());
                Text = PegNode.GetAsString(src);
                Start = peg.match_.posBeg_;
                End = peg.match_.posEnd_-1;
            }

            private IList<Token> _children;
            public IList<Token> Children
            {
                get
                {
                    if(_children==null)
                    {
                        _children = new List<Token>();
                        var child = PegNode.child_;
                        if(child!=null)
                        {
                            _children.Add(new Token(child, _src));
                            while(child.next_!=null)
                            {
                                child = child.next_;
                                _children.Add(new Token(child, _src));
                            }
                        }
                        
                    }
                    return _children;
                }
            }
        }
    }
}