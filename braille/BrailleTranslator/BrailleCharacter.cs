using System.Linq;
using System.Text;
using BrailleTranslator.Data;

namespace BrailleTranslator
{
    public class BrailleCharacter
    {
        public enum Row
        {
            Top,
            Middle,
            Bottom
        }

        public enum Column
        {
            Left,
            Right
        }

        public const int Size = 6;
        public const int RowSize = 3;
        public const int ColumnSize = 2;

        public static string EmptyPattern;

        public BrailleCharacter(BrailleDictionary dictionary)
        {
            EmptyPattern = dictionary.BasicToPattern[' '];
        }

        public static char[,] Create(string pattern)
        {
            var line = 0;
            var rightExists = false;
            var leftExists = false;
            var brailleChar = new char[RowSize, ColumnSize];

            for (var i = 0; i < pattern.Length; i++)
            {
                if (i % 2 == 0)
                {
                    brailleChar[line, (int)Column.Left] = pattern[i];
                    leftExists = true;
                }
                else
                {
                    brailleChar[line, (int)Column.Right] = pattern[i];
                    rightExists = true;
                }

                if (rightExists && leftExists)
                {
                    line++;
                    rightExists = false;
                    leftExists = false;
                }
            }

            return brailleChar;
        }

        public string BrailleCharToPattern(char[,] brailleChar)
        {
            var sb = new StringBuilder();

            for (var i = 0; i <= brailleChar.GetUpperBound(0); i++)
            {
                for (var j = 0; j <= brailleChar.GetUpperBound(1); j++)
                {
                    sb.Append(brailleChar[i, j]);
                }
            }
            return sb.ToString();
        }
    }
}
