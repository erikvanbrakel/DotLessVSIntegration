using LessProject.DotLessIntegration.LexerSrc;
using NUnit.Framework;

namespace TimeStamper.Tests
{
    [TestFixture]
    public class CharBufferFixtures
    {

        [Test]
        public void Can_Load_And_Peek_At_Three_Chars_From_Buffer()
        {
            ICharacterBuffer charBuffer = new StringCharacterBuffer("ABCDEFGHI", 3);
            charBuffer.Load(3);
            Assert.AreEqual((char)charBuffer.Peek(1), 'A');
            Assert.AreEqual((char)charBuffer.Peek(2), 'B');
            Assert.AreEqual((char)charBuffer.Peek(3), 'C');
        }

        [Test]
        public void Will_Not_Exceed_Buffer_If_Chars_Do_Not_Exist()
        {
            ICharacterBuffer charBuffer = new StringCharacterBuffer("ABC", 2);
            
            charBuffer.Load(2);
            Assert.AreEqual((char)charBuffer.Peek(1), 'A');
            Assert.AreEqual((char)charBuffer.Peek(2), 'B');

            charBuffer.Load(2);
            Assert.AreEqual((char)charBuffer.Peek(1), 'C');
            Assert.AreEqual(charBuffer.Peek(2), -1);
        }

        [Test]
        public void Cannot_Peek_Past_Buffer()
        {
            ICharacterBuffer charBuffer = new StringCharacterBuffer("ABCDEFGHI", 1);
            charBuffer.Load();
            Assert.AreEqual(0, charBuffer.Peek(2));
        }

    }
}