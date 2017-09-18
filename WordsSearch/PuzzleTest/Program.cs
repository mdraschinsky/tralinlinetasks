using System;

namespace PuzzleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var words = new string[] {"THIS", "PUZZLE", "ROCKS", "SOCKS", "ROW"};
            //var puzzle = @"S E W R T R B S C D
            //               C I G Z U W I Y E R
            //               R O H K S H A U C U
            //               S F K T T T Y S G E
            //               Y S O O U N M Z I M
            //               T C G R R R I D A N
            //               H Z G H O G W T U T
            //               H Q M W O R Z B H T
            //               N T C L A T N I C E
            //               Y B U R P Z S X M S";

            var words = new string[] { "dan", "RUBY", "DAN", "ROCKS", "MATZ" };

            var puzzleString = @"U E W R T R B H C D
                           C X G Z U W R Y A D
                           R O C K S B A N A D
                           S F N F M T Y N G E
                           Y A O O U N M Z I M
                           D A N P R T I D A N
                           A Z G H Q G W T U V
                           N Q M N D X Z N S T
                           N T C L A T N B A E
                           Y B U R P Z U X M D";

            IChunksExtractor exractor = new Extractor();
            ISearch searcher = new Searcher();

            var puzzle = new Puzzle(exractor, searcher);
            var solvedPuzzle = puzzle.SolvePuzzle(puzzleString, words);

            var drawer = new PuzzleDrawer(Console.Out);
            drawer.LogChunks(exractor);
            drawer.DrawPuzzle(solvedPuzzle, exractor.Width);

            Console.ReadKey();
        }
    }
}
