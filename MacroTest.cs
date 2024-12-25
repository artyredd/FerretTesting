using Ferret;

namespace FerretTesting
{
    public class MacroTest
    {
        [Test]
        public void TestMacroRemove()
        {
            var data = "using System.Text;\n#define test(var)\\\r\n__debugbreak();\r\nvoid method();\n";
            var tokens = new Tokenizer(data).Parse();
            var stream = new TokenStream(tokens);

            new TokenLexer(stream)
                .AppendRule(new MacroRule())
                .ProcessRules();

            string expected = "using System.Text;\nvoid method();\n";
            string actual = stream.ToString()!;

            Assert.AreEqual(expected, actual);
        }
    }
}