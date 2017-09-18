using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using BrailleTranslator.Data;

namespace BrailleTranslator.Presentation
{
    public class BrailleVisualizer : IBrailleVisualizer
    {
        private readonly BrailleCharacter _brailleCharacter;
        private readonly BrailleDictionary _dictionary;
        private readonly TextWriter _writer;

        public BrailleVisualizer(BrailleCharacter brailleCharacter, BrailleDictionary dictionary, TextWriter writer)
        {
            _brailleCharacter = brailleCharacter;
            _dictionary = dictionary;
            _writer = writer;
        }

        public void Print(IEnumerable<char[,]> brailleCharacters)
        {
            var brailleCharList = brailleCharacters.ToList();

            var itemLimit = (int)Math.Floor((decimal)((Console.WindowWidth / 3) - 1));

            if (brailleCharList.Count > itemLimit)
            {
                PrintByChunks(brailleCharList, itemLimit);
            }
            else
            {
                PrintBrailleCharacterLine(brailleCharList, BrailleCharacter.Row.Top);
                _writer.WriteLine(string.Empty);

                PrintBrailleCharacterLine(brailleCharList, BrailleCharacter.Row.Middle);
                _writer.WriteLine(string.Empty);

                PrintBrailleCharacterLine(brailleCharList, BrailleCharacter.Row.Bottom);
                _writer.WriteLine(string.Empty);
            }
        }

        private void PrintByChunks(List<char[,]> brailleCharacters, int itemLimit)
        {
            var startIndex = 0;
            var remainingCharCount = brailleCharacters.Count;
            while (remainingCharCount > 0)
            {
                var isLastChunk = remainingCharCount < itemLimit;

                var count = isLastChunk ? remainingCharCount : GetItemCountToProcess(brailleCharacters, startIndex, itemLimit);

                var list = brailleCharacters.GetRange(startIndex, count);

                PrintBrailleCharacterLine(list);

                startIndex = count;
                remainingCharCount = remainingCharCount - startIndex;
            }

        }

        private int GetItemCountToProcess(List<char[,]> brailleCharacters, int startIndex, int itemLimit)
        {
            var count = itemLimit;

            var list = brailleCharacters.GetRange(startIndex, itemLimit);
            var ind = list.FindLastIndex(bc => IsSpace(bc));

            if (ind != -1)
            {
                count = ind + 1;
            }

            return count;
        }

        private bool IsSpace(char[,] first)
        {
            var emptyBrailleChar = BrailleCharacter.Create(BrailleCharacter.EmptyPattern);
            var isSpaceChar = true;

            for (var i = 0; i <= emptyBrailleChar.GetUpperBound(0); i++)
            {
                for (var j = 0; j <= emptyBrailleChar.GetUpperBound(1); j++)
                {
                    if (first[i, j] != emptyBrailleChar[i, j])
                    {
                        isSpaceChar = false;
                        break;
                    }
                }
            }

            return isSpaceChar;
        }

        private void PrintBrailleCharacterLine(List<char[,]> brailleCharacters)
        {
            PrintBrailleCharacterLine(brailleCharacters, BrailleCharacter.Row.Top);
            _writer.WriteLine(string.Empty);

            PrintBrailleCharacterLine(brailleCharacters, BrailleCharacter.Row.Middle);
            _writer.WriteLine(string.Empty);

            PrintBrailleCharacterLine(brailleCharacters, BrailleCharacter.Row.Bottom);
            _writer.WriteLine(string.Empty);
            _writer.WriteLine(string.Empty);
        }

        private void PrintBrailleCharacterLine(List<char[,]> brailleCharacters, BrailleCharacter.Row row)
        {
            var theRow = (int)row;

            brailleCharacters.ForEach(item =>
            {
                _writer.Write(item[theRow, (int)BrailleCharacter.Column.Left]);
                _writer.Write(item[theRow, (int)BrailleCharacter.Column.Right]);
                _writer.Write(" ");
            });
        }
    }
}
