using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleTest
{
    internal class PuzzleDrawer
    {
        private readonly TextWriter _output;

        public PuzzleDrawer(TextWriter writeStream)
        {
            _output = writeStream;
        }

        public void DrawPuzzle(char[,] puzzle, int width)
        {
            _output.WriteLine(string.Empty);
            _output.WriteLine("Solved Puzzle:");
            _output.WriteLine(string.Empty);

            int num = 0;
            foreach (var character in puzzle)
            {
                _output.Write(character);

                num++;
                if (num % width == 0)
                {
                    _output.WriteLine(string.Empty);
                }
            }
        }

        public void LogChunks(IChunksExtractor extractor)
        {
            Action<List<string>> printList = (list) =>  {
                                                            foreach (var str in list)
                                                            {
                                                                _output.WriteLine(str);
                                                            }
                                                        };

            _output.WriteLine("Horizintal chunks:");
            printList(extractor.HorizontalChunks);
            _output.WriteLine("Vertical chunks:");
            printList(extractor.VerticalChunks);
            _output.WriteLine("Left diagonal chunks:");
            printList(extractor.DiagonalLeftChunks);
            _output.WriteLine("Right diagonal chunks:");
            printList(extractor.DiagonalRightChunks);
        }
    }
}
