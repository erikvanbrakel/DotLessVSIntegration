using LessProject.DotLessIntegration.LexerSrc;
using NUnit.Framework;

namespace TimeStamper.Tests
{
    [TestFixture]
    public class TokenBufferFixtures
    {

        [Test]
        public void Can_Load_And_Peek_At_Two_Tokens_From_Buffer()
        {
            var tokenBuffer = new TokenBuffer(new StringCharacterBuffer("@Variable", 3), 2);
            Assert.AreEqual(tokenBuffer.Peek(1).Kind, TokenKind.VARIABLE);
            Assert.AreEqual(tokenBuffer.Peek(2).Kind, TokenKind.EOF);
        }

    }
}