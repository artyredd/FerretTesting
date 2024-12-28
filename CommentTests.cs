using Ferret;

namespace FerretTesting
{
    public class CommentTests
    {
        [Test]
        public void SingleCommentRemove()
        {
            var data = "void method(); // does thing\nint x = 1;";
            var tokens = new Tokenizer(data).Parse();
            var stream = new TokenStream(tokens);

            new TokenLexer(stream)
                .AppendRule(new SingleCommentRule())
                .ProcessRules();

            string expected = "void method(); \nint x = 1;";
            string actual = stream.ToString()!;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MultiLineRemove()
        {
            var data = "\tvoid method(/*pass nothing*/);\n\tint x = 12;";
            var tokens = new Tokenizer(data).Parse();
            var stream = new TokenStream(tokens);

            new TokenLexer(stream)
                .AppendRule(new MultiLineCommentRule())
                .ProcessRules();

            string expected = "\tvoid method();\n\tint x = 12;";
            string actual = stream.ToString()!;

            Assert.AreEqual(expected, actual);
        }
    }
}