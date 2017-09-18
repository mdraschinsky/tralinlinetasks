using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleTest
{
    public class Searcher : ISearch
    {
        private char[,] _result;
        public char[,] SolvedPuzzle => _result;

        public void SearchWords(IChunksExtractor extractor, IEnumerable<string> words)
        {
            var width = extractor.Width;
            InitPuzzle(width);

            foreach (var word in words)
            {
                for (int num = 0; num < extractor.HorizontalChunks.Count; num++)
                {
                    var line = extractor.HorizontalChunks[num];
                    var positions = line.GetWordsPositions(word);
                    foreach (var index in positions)
                    {
                        WriteWord(num, index, word, Direction.Horizontal);
                    }
                }

                for (int num = 0; num < extractor.VerticalChunks.Count; num++)
                {
                    var line = extractor.VerticalChunks[num];
                    var positions = line.GetWordsPositions(word);
                    foreach (var index in positions)
                    {
                        WriteWord(num, index, word, Direction.Vertical);
                    }
                }

                for (int num = 0; num < extractor.DiagonalLeftChunks.Count; num++)
                {
                    var line = extractor.DiagonalLeftChunks[num];
                    var positions = line.GetWordsPositions(word);
                    foreach (var index in positions)
                    {
                        var w = width - 1;
                        var sign = (num >= width ? +1 : -1);
                        var row = ((num >= w) ? w : num) - index;
                        var col = (num <= w) ? index : num % (w) + sign * index;

                        WriteWord(row, col, word, Direction.DiagonalLeft);
                    }
                }

                for (int num = 0; num < extractor.DiagonalRightChunks.Count; num++)
                {
                    var line = extractor.DiagonalRightChunks[num];
                    var positions = line.GetWordsPositions(word);
                    foreach (var index in positions)
                    {
                        var w = width - 1;
                        var row = ((num >= w) ? 0 : num % w) + index;
                        var col = num % w + index;

                        WriteWord(row, col, word, Direction.DiagonalRight);
                    }
                }
            }
        }

        private void WriteWord(int row, int col, string word, Direction direction)
        {
            int n = 0;
            if (direction == Direction.Horizontal)
            {
                foreach (var w in word)
                {
                    SolvedPuzzle[row, col + n] = w;
                    n++;
                }
            } else if (direction == Direction.Vertical)
            {
                foreach (var w in word)
                {
                    SolvedPuzzle[col + n, row] = w;
                    n++;
                }
            }
            else if (direction == Direction.DiagonalLeft)
            {
                foreach (var w in word)
                {
                    SolvedPuzzle[row - n, col + n] = w;
                    n++;
                }
            }
            else if (direction == Direction.DiagonalRight)
            {
                foreach (var w in word)
                {
                    SolvedPuzzle[row + n, col + n] = w;
                    n++;
                }
            }
        }

        private void InitPuzzle(int width)
        {
            _result = new char[width, width];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    _result[i, j] = '+';
                }
            }
        }
    }
}
