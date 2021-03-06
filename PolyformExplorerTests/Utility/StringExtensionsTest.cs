using NUnit.Framework;

namespace PolyformExplorer.Utility.Test
{
    internal class StringExtensionsTest
    {
        [Test]
        public void Test_normalize_newlines()
        {
            string windowsString = "123\r\n456\r\n789";
            string linuxString = "123\n456\n789";
            string macString = "123\r456\r789";

            Assert.That(windowsString.NormalizeNewlines(), Is.EqualTo(linuxString.NormalizeNewlines()));
            Assert.That(windowsString.NormalizeNewlines(), Is.EqualTo(macString.NormalizeNewlines()));
            Assert.That(linuxString.NormalizeNewlines(), Is.EqualTo(macString.NormalizeNewlines()));
        }

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
".NormalizeNewlines();

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

ghj".NormalizeNewlines();

            Assert.That(inputString.TrimCommonIndentation(true), Is.EqualTo(expectedResult));
        }
    }
}
