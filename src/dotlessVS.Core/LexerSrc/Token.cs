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