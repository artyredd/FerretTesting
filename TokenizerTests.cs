using Ferret;
using static Ferret.TokenExtensions;
namespace FerretTesting
{
    public class TokenizerTests
    {
        Tokenizer Tokenizer { get; set; } = new("using System;\nnamespace Ferret\n{\n\tclass MyClass\n\t{\n\t\tint Number;\n\t}\n};;;;");

        [Test]
        public void Count()
        {
            var tokens = Tokenizer.Parse();
            TestContext.WriteLine(tokens.ToReadableString());
            Assert.AreEqual(29, tokens.Count);
            Assert.Pass();
        }

        [Test]
        public void String()
        {
            var tokens = Tokenizer.Parse();
            string expected = "12132121232121232121323233333";
            Assert.AreEqual(expected, tokens.ToString());
        }

        [Test]
        public void Split()
        {
            var token = new Token(TokenType.Text,"aba");
            var tokens = token.Split("b");
            Assert.AreEqual(3, tokens.Length);
            Assert.AreEqual(new Token(TokenType.Text,"a"), tokens[0]);

            token = new Token(TokenType.Text, "aba");
            tokens = token.Split("a");
            Assert.AreEqual(3,tokens.Length);
            Assert.AreEqual(new Token(TokenType.Text, "a"), tokens[0]);
            Assert.AreEqual(new Token(TokenType.Text, "b"), tokens[1]);
            Assert.AreEqual(new Token(TokenType.Text, "a"), tokens[2]);

        }
    }
}