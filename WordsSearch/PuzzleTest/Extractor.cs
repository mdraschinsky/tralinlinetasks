using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleTest
{
    public class Extractor : IChunksExtractor
    {
        private int _width;
        private List<string> _horizontalChunks;
        private List<string> _verticalChunks;
        private List<string> _diagonalLeftChunks;
        private List<string> _diagonalRightChunks;

        public int Width => _width;
        public List<string> HorizontalChunks => _horizontalChunks;
        public List<string> VerticalChunks => _verticalChunks;
        public List<string> DiagonalLeftChunks => _diagonalLeftChunks;
        public List<string> DiagonalRightChunks => _diagonalRightChunks;

        public void ExtractChunks(string puzzleString)
        {
            var lines = puzzleString.Split(PuzzleStrings.Separator.ToCharArray());
            _horizontalChunks = new List<string>(lines);
            _width = puzzleString.IndexOf(PuzzleStrings.Separator, StringComparison.Ordinal);

            Func<int, int, string> extractDownLeftSide = null;
            extractDownLeftSide = (i, num) => {
                                                var s = "" + lines[i - num][num];
                                                if (i - num > 0)
                                                {
                                                    s += extractDownLeftSide(i, num + 1);
                                                }
                                                return s;
                                              };

            Func<int, int, string> extractUpRightSide = null;
            extractUpRightSide = (i, num) => {  var s = "" + lines[i + num][Width - num - 1];
                                                if (i + num < Width - 1)
                                                {
                                                    s += extractUpRightSide(i, num + 1);
                                                }
                                                return s;
                                              };

            Func<int, int, string> extractDownRightSide = null;
            extractDownRightSide = (i, num) => {    var x = Width - i + num - 1;
                                                    var s = "" + lines[x][num];
                                                    if (x < Width - 1)
                                                    {
                                                        s += extractDownRightSide(i, num + 1);
                                                    }
                                                    return s;
                                                };

            Func<int, int, string> extractUpLeftSide = null;
            extractUpLeftSide = (i, num) => {   var y = i + num;
                                                var s = "" + lines[num][y];
                                                if (y < Width - 1)
                                                {
                                                    s += extractUpLeftSide(i, num + 1);
                                                }
                                                return s;
                                            };

            Func<int, int, string> extractVertical = null;
            extractVertical = (i, num) => { var s = "" + lines[num][i];
                                            if (num < Width - 1)
                                            {
                                                s += extractVertical(i, num + 1);
                                            }
                                            return s;
                                        };

            //vertical
            _verticalChunks = new List<string>();

            //get diagonal left strings
            _diagonalLeftChunks = new List<string>();
            var diagonalLeftChunks2 = new List<string>();

            //get diagonal right strings
            _diagonalRightChunks = new List<string>();
            var diagonalRightChunks2 = new List<string>();

            for (int i = 0; i < Width; i++)
            {
                _verticalChunks.Add(extractVertical(i, 0));

                _diagonalLeftChunks.Add(extractDownLeftSide(i, 0));
                diagonalLeftChunks2.Add(extractUpRightSide(i, 0).ReverseString());

                _diagonalRightChunks.Add(extractDownRightSide(i, 0));
                diagonalRightChunks2.Add(extractUpLeftSide(i, 0));
            }
            _diagonalLeftChunks.AddRange(diagonalLeftChunks2.Skip(1));
            _diagonalRightChunks.AddRange(diagonalRightChunks2.Skip(1));
        }
    }
}
