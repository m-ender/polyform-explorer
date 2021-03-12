using NUnit.Framework;

namespace PolyformExplorer.Utility.Test
{
    internal class StringExtensionsTest
    {
        [Test]
        public void Test_trimming_common_indentation()
        {
            string inputString = @"
                    123
                abc
                    def

                ghj
            ";

            string expectedResult = @"
    123
abc
    def

ghj
";

            Assert.That(inputString.TrimCommonIndentation(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void Test_trimming_leading_and_trailing_empty_lines()
        {
            string inputString = @"
                    123
                abc
                    def

                ghj
            ";

            string expectedResult = @"    123
abc
    def

ghj";

            Assert.That(inputString.TrimCommonIndentation(true), Is.EqualTo(expectedResult));
        }
    }
}
