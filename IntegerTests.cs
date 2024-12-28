using Ferret;

namespace FerretTesting
{
    public class IntegerTests
    {
        [Test]
        public void IntegerRemove()
        {
            var data = "char c = 'c'; int x = 1234; float y = 1234.6; x = 0xFFeF; x=0x; x=0b; x=0b0101; x=FFeFG; x=0xFFeFG; x=0x1234;";
            var tokens = new Tokenizer(data).Parse();
            var stream = new TokenStream(tokens);

            new TokenLexer(stream)
                .AppendRule(new IntegerRule())
                .ProcessRules();

            string expected = "char c = 'c'; int x = ; float y = 1234.6; x = ; x=0x; x=0b; x=; x=FFeFG; x=0xFFeFG; x=;";
            string actual = stream.ToString()!;

            Assert.AreEqual(expected, actual);
        }
    }
}