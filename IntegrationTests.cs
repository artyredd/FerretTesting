using Ferret;

namespace FerretTesting
{
    public class IntegrationTests
    {
        private class DoNothingRule : ILexerRule
        {
            bool applied = false;

            public void ApplyRule(TokenStream stream)
            {
                while (stream.GetToken(out var token))
                {
                    stream.PutToken(token);
                }
            }

            public bool RuleApplies(TokenStream stream)
            {
                if (applied)
                {
                    return false;
                }

                applied = true;
                return applied;
            }
        }
        private class RemoveWhitespace : ILexerRule
        {
            bool appliedRule = false;
            public void ApplyRule(TokenStream stream)
            {
                while (stream.GetToken(out var token))
                {
                    if (token != TokenType.Whitespace)
                    {
                        stream.PutToken(token);
                    }
                }

                appliedRule = true;
            }

            public bool RuleApplies(TokenStream stream)
            {
                if (appliedRule) return false;

                var tokens = new TokenCollection(TokenType.Whitespace);
                return stream.Tokens.IndexOf(tokens) != -1;
            }
        }

        [Test]
        public void LexerDefault()
        {
            var data = "abcdefghijklmnopqrstuvwxyz";
            var tokens = new Tokenizer(data).Parse();
            var stream = new TokenStream(tokens);

            new TokenLexer(stream)
                .AppendRule(new DoNothingRule())
                .ProcessRules();

            string expected = data;
            string actual = stream.ToString()!;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LexerSimple()
        {
            var data = "abcd efghi jklmno pqrs tuvwxyz";
            var tokens = new Tokenizer(data).Parse();
            var stream = new TokenStream(tokens);

            new TokenLexer(stream)
                .AppendRule(new RemoveWhitespace())
                .ProcessRules();

            string expected = "abcdefghijklmnopqrstuvwxyz";
            string actual = stream.ToString()!;

            Assert.AreEqual(expected, actual);
        }
    }
}