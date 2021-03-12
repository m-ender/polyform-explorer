using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PolyformExplorer.Utility
{
    internal static class StringExtensions
    {
        public static string NormalizeNewlines(this string multilineString)
            => Regex.Replace(multilineString, "\r\n|\n|\r", Environment.NewLine);

        public static string TrimCommonIndentation(this string multilineString, bool trimEmptyLines = false)
        {
            string[] lines = multilineString.Split(
                new[] { "\r\n", "\r", "\n" }, 
                StringSplitOptions.None);
            int trimWidth = lines.Min(line => {
                Match match = Regex.Match(line, @"\S");
                return match.Success ? match.Index : int.MaxValue;
            });

            IEnumerable<string> trimmedLines = lines.Select(line => line.Substring(Math.Min(line.Length, trimWidth)));

            string result = string.Join(Environment.NewLine, trimmedLines);

            return trimEmptyLines ? result.Trim('\n', '\r') : result;
        }
    }
}
