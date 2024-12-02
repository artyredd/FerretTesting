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
                .When(x => x.Value == ")\\");

            int found = tokens.FindPattern(pattern);

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

            int found = tokens.FindPattern(pattern);

            Assert.AreEqual(5, found);
        }

        [Test]
        public void TokenPatternAdvanced()
        {
            var data = "using System.Text;\n#define test(var)\\\n__debugbreak();\nvoid method();\n";
            var tokens = new Tokenizer(data).Parse();

            var pattern = new TokenPattern(false)
                .When(TokenType.Control)
                .IsPreceededBy(TokenType.Text)
                .IsPreceededBy(TokenType.Whitespace)
                .IsPreceededBy(TokenType.Text)
                .IsPreceededBy(x=>x == "#")
                .IsPreceededBy(TokenType.Whitespace)
                .IsFollowedBy(TokenType.Text)
                .IsFollowedBy(x=>x== ")\\")
                .IsFollowedBy(TokenType.Whitespace);

            int found = tokens.FindPattern(pattern);

            Assert.AreEqual(11, found);
        }
    }
}