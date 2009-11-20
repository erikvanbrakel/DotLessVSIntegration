using LessProject.DotLessIntegration.LexerSrc;
using NUnit.Framework;

namespace TimeStamper.Tests
{
    [TestFixture]
    public class LexerFixtures
    {
        [Test]
        public void Can_Load_EOF_Token_While_Disregarding_WhiteSpace()
        {
            var lexer = new Lexer(new StringCharacterBuffer("  ", 3));
            Assert.AreEqual(TokenKind.EOF, lexer.GetNextToken().Kind);
        }
        [Test]
        public void Can_Load_VARIABLE_Token()
        {
            var lexer = new Lexer(new StringCharacterBuffer(" @Variable  ", 3));
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenKind.VARIABLE, token.Kind);
            Assert.AreEqual("Variable", token.Text);
        }

        [Test]
        public void Can_Load_Class_Token()
        {
            var lexer = new Lexer(new StringCharacterBuffer(" .class  ", 3));
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenKind.CLASS, token.Kind);
            Assert.AreEqual("class", token.Text);
        }

        [Test]
        public void Can_Retrieve_Token_Pos()
        {
            var lexer = new Lexer(new StringCharacterBuffer(".test{", 3));
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenKind.CLASS, token.Kind);
            Assert.AreEqual(1, token.StartIndex );
            Assert.AreEqual(5, token.EndIndex );


            token = lexer.GetNextToken();
        }
    }
}