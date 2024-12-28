using Ferret;

namespace FerretTesting
{
    public class StringTests
    {
        [Test]
        public void StringRemove()
        {
            var data = "string myString = \"Hello World!\";";
            var tokens = new Tokenizer(data).Parse();
            var stream = new TokenStream(tokens);

            new TokenLexer(stream)
                .AppendRule(new StringRule())
                .ProcessRules();

            string expected = "string myString = ;";
            string actual = stream.ToString()!;

            Assert.AreEqual(expected, actual);
        }
    }
}