using System.Collections.Generic;
using System.Linq;
using LessProject.DotLessIntegration.LexerWithPegSupport;
using NUnit.Framework;


namespace LessSLParser.Tests
{
    /// <summary>
    /// Summary description for TokenRetrievalFixtures
    /// </summary>
    [TestFixture]
    public class TokenRetrievalFixtures
    {
        private static IList<Lexer.Token> GetTokens(string src)
        {
            return new Lexer(src).GetRootToken().Children;
        }

        private static Lexer.Token GetFirstToken(string src)
        {
            return GetTokens(src).First();
        }

        [Test]
        public void Can_Parse_All_Syntax_Elements()
        {
            Assert.AreEqual(nLess.EnLess.LeftCurly, GetFirstToken("{").TokenType);
            Assert.AreEqual(nLess.EnLess.RightCurly, GetFirstToken("}").TokenType);
            Assert.AreEqual(nLess.EnLess.LeftSquare, GetFirstToken("[").TokenType);
            Assert.AreEqual(nLess.EnLess.RightSquare, GetFirstToken("]").TokenType);
            Assert.AreEqual(nLess.EnLess.Comma, GetFirstToken(",").TokenType);
            Assert.AreEqual(nLess.EnLess.Colon, GetFirstToken(":").TokenType);
            Assert.AreEqual(nLess.EnLess.SemiColon, GetFirstToken(";").TokenType);
        }

        [Test]
        public void Can_Retrieve_Multipule_Syntax_Elements()
        {
            var tokens = GetTokens("{}  [");
            Assert.AreEqual(3, tokens.Count);
            Assert.IsTrue(tokens[0].Start == 0 && tokens[0].End==0 
                       && tokens[1].Start == 1 && tokens[1].End==1
                       && tokens[2].Start == 4 && tokens[2].End==4);
        }

        [Test]
        public void Can_Parse_Identifiers()
        {
            Assert.AreEqual(nLess.EnLess.Variable, GetFirstToken("@variable").TokenType);
            Assert.AreEqual(nLess.EnLess.Class, GetFirstToken(".class").TokenType);
            Assert.AreEqual(nLess.EnLess.Id, GetFirstToken("#id").TokenType);
            Assert.AreEqual(nLess.EnLess.Ident, GetFirstToken("p").TokenType);
            Assert.AreEqual(nLess.EnLess.Ident, GetFirstToken("background").TokenType);
        }

        [Test]
        public void Can_Parse_Comments()
        {
            Assert.AreEqual(nLess.EnLess.DoubleSlashComment, GetFirstToken("// comment comment coasdasd ").TokenType);
            Assert.AreEqual(1,GetTokens("// asd asd !£$%^&*()").Count);
            Assert.AreEqual(2, GetTokens(@"// asd asd !£$%^&*()
            .class").Count);

            Assert.AreEqual(2, GetTokens(@"/* asd asd !£$%^&*()
            .class
            .test 
            We are still in comment
            */").Count);
        }
    }

}
