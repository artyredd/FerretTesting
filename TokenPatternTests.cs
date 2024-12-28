using Ferret;

namespace FerretTesting
{
    public class TokenPatternTests 
    {
        [Test]
        public void TestTokenPatternSimple()
        {
            var data = "using System.Text;\n#define test(var)\\\n__debugbreak();\nvoid method();\n";
            var tokens = new Tokenizer(data).Parse();

            var pattern = new TokenPattern()
                .When(")")
                .IsFollowedBy("\\");

            int found = tokens.IndexOf(pattern);

            Assert.AreEqual(13, found);
        }

        [Test]
        public void TestTokenPattern()
        {
            var data = "using System.Text;\n#define test(var)\\\n__debugbreak();\nvoid method();\n";
            var tokens = new Tokenizer(data).Parse();
            var stream = new TokenStream(tokens);

            new TokenLexer(stream)
                .AppendRule(new MacroRule())
                .ProcessRules();

            var pattern = new TokenPattern()
                .When(TokenType.Control)
                .When(x => x.Value.Contains(';'))
                .IsFollowedBy(TokenType.Whitespace)
                .IsPreceededBy(TokenType.Text);

            int found = tokens.IndexOf(pattern);

            Assert.AreEqual(5, found);
        }

        [Test]
        public void TokenPatternAdvanced()
        {
            var data = "1. 2. 3. 4. 5. 6. 7. 8. 9";
            var tokens = new Tokenizer(data).Parse();

            var pattern = new TokenPattern(false)
                .When(TokenType.Control)
                .IsPreceededBy(x=> x == "4")
                .IsPreceededBy(TokenType.Whitespace)
                .IsPreceededBy(TokenType.Control)
                .IsFollowedBy(TokenType.Whitespace)
                .IsFollowedBy(x=> x == "5");

            int found = tokens.IndexOf(pattern);

            Assert.AreEqual(10, found);
        }
    }
}