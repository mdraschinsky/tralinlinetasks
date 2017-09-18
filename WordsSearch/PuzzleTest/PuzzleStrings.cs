using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PuzzleTest
{
    public static class PuzzleStrings
    {
        public const string Separator = "\n";

        public static string ReverseString(this string s)
        {
            return new string(s.Reverse().ToArray());
        }

        public static string ClearPuzzleString(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            return s.ToUpperInvariant()
                .Trim()
                .Replace(" ", String.Empty)
                .Replace("\r\n", Separator);
        }

        public static IEnumerable<int> GetWordsPositions(this string input, string word)
        {
            IEnumerable<int> indexesOfAll = Regex.Matches(input, word).Cast<Match>().Select(m => m.Index);
            return indexesOfAll;
        }
    }
}
