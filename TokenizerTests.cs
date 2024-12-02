using Ferret;
using static Ferret.TokenExtensions;
namespace FerretTesting
{
    public class TokenizerTests
    {
        Tokenizer Tokenizer { get; set; } = new("using System;\nnamespace Ferret\n{\n\tclass MyClass\n\t{\n\t\tint Number;\n\t}\n}");

        [Test]
        public void Count()
        {
            var tokens = Tokenizer.Parse();
            TestContext.WriteLine(tokens.ToReadableString());
            Assert.AreEqual(25, tokens.Count);
            Assert.Pass();
        }

        [Test]
        public void String()
        {
            var tokens = Tokenizer.Parse();
            string expected = "1213212123212123212132323";
            Assert.AreEqual(expected, tokens.ToString());
        }
    }
}