using Microsoft.VisualStudio.TestTools.UnitTesting;
using PuzzleTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleTest.Tests
{
    [TestClass()]
    public class PuzzleTests
    {
        [TestMethod()]
        public void TestAllDiagonals()
        {
            var words = new string[] { "xyz" };

            var puzzleString = @"X O O O O O Z
                                 O Y O O O Y O
                                 O O Z O X O O
                                 O O O O O O O
                                 O O X O X O O
                                 O Y O O O Y O
                                 Z O O O O O Z";

            var expectedResult = new char[7, 7]  {
                                                    { 'X', '+', '+', '+', '+', '+', 'Z'},
                                                    { '+', 'Y', '+', '+', '+', 'Y', '+'},
                                                    { '+', '+', 'Z', '+', 'X', '+', '+'},
                                                    { '+', '+', '+', '+', '+', '+', '+'},
                                                    { '+', '+', 'X', '+', 'X', '+', '+'},
                                                    { '+', 'Y', '+', '+', '+', 'Y', '+'},
                                                    { 'Z', '+', '+', '+', '+', '+', 'Z'}
                                                };

            var result = GetPuzzleResult(puzzleString, words);
            bool isEqual = CompareArrays(expectedResult, result, 7);

            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void TestAllHorizontalsAndVerticals()
        {
            var words = new string[] { "xyz" };

            var puzzleString = @"X Y Z O Z Y X
                                 Y O O O O O Y
                                 Z O O O O O Z
                                 O O O O O O O
                                 X O O O O O X
                                 Y O O O O O Y
                                 Z Y X O X Y Z";

            var expectedResult = new char[7, 7]  {
                                                    { 'X', 'Y', 'Z', '+', 'Z', 'Y', 'X'},
                                                    { 'Y', '+', '+', '+', '+', '+', 'Y'},
                                                    { 'Z', '+', '+', '+', '+', '+', 'Z'},
                                                    { '+', '+', '+', '+', '+', '+', '+'},
                                                    { 'X', '+', '+', '+', '+', '+', 'X'},
                                                    { 'Y', '+', '+', '+', '+', '+', 'Y'},
                                                    { 'Z', 'Y', 'X', '+', 'X', 'Y', 'Z'}
                                                };

            var result = GetPuzzleResult(puzzleString, words);
            bool isEqual = CompareArrays(expectedResult, result, 7);

            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void TestHorizontalsWithSeveralWords()
        {
            var words = new string[] { "xyz" };

            var puzzleString = @"O O O O O O O
                                 O O O O O O O
                                 O O O X O O O
                                 X Y Z Y X Y Z
                                 O O O Z O O O
                                 O O O O O O O
                                 O O O O O O O";

            var expectedResult = new char[7, 7]  {
                                                    { '+', '+', '+', '+', '+', '+', '+'},
                                                    { '+', '+', '+', '+', '+', '+', '+'},
                                                    { '+', '+', '+', 'X', '+', '+', '+'},
                                                    { 'X', 'Y', 'Z', 'Y', 'X', 'Y', 'Z'},
                                                    { '+', '+', '+', 'Z', '+', '+', '+'},
                                                    { '+', '+', '+', '+', '+', '+', '+'},
                                                    { '+', '+', '+', '+', '+', '+', '+'}
                                                };

            var result = GetPuzzleResult(puzzleString, words);
            bool isEqual = CompareArrays(expectedResult, result, 7);

            Assert.IsTrue(isEqual);
        }


        [TestMethod()]
        public void TestDiagonalsNW()
        {
            var words = new string[] { "xyz" };

            var puzzleString = @"X A A A
                                 A Y A A
                                 A A Z A
                                 A A A A";

            var expectedResult = new char[4, 4]  {
                                                    { 'X', '+', '+', '+'},
                                                    { '+', 'Y', '+', '+'},
                                                    { '+', '+', 'Z', '+'},
                                                    { '+', '+', '+', '+'}
                                                };

            var result = GetPuzzleResult(puzzleString, words);
            bool isEqual = CompareArrays(expectedResult, result, 4);

            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void TestDiagonalsSW()
        {
            var words = new string[] { "xyz" };

            var puzzleString = @"A A A A
                                 A A Z A
                                 A Y A A
                                 X A A A";

            var expectedResult = new char[4, 4]  {
                                                    { '+', '+', '+', '+'},
                                                    { '+', '+', 'Z', '+'},
                                                    { '+', 'Y', '+', '+'},
                                                    { 'X', '+', '+', '+'}
                                                };

            var result = GetPuzzleResult(puzzleString, words);
            bool isEqual = CompareArrays(expectedResult, result, 4);

            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void TestDiagonalsSE()
        {
            //TODO
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void TestDiagonalsNE()
        {
            //TODO
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void TestHorizontalsNE()
        {
            var words = new string[] { "xyz" };

            var puzzleString = @"X Y Z A
                                 A A A A
                                 A A A A
                                 A A A A";

            var expectedResult = new char[4, 4]  {
                                                    { 'X', 'Y', 'Z', '+'},
                                                    { '+', '+', '+', '+'},
                                                    { '+', '+', '+', '+'},
                                                    { '+', '+', '+', '+'}
                                                };

            var result = GetPuzzleResult(puzzleString, words);
            bool isEqual = CompareArrays(expectedResult, result, 4);

            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void TestHorizontalsSW()
        {
            var words = new string[] { "xyz" };

            var puzzleString = @"A A A A
                                 A A A A
                                 A A A A
                                 A X Y Z";

            var expectedResult = new char[4, 4]  {
                                                    { '+', '+', '+', '+'},
                                                    { '+', '+', '+', '+'},
                                                    { '+', '+', '+', '+'},
                                                    { '+', 'X', 'Y', 'Z'}
                                                };

            var result = GetPuzzleResult(puzzleString, words);
            bool isEqual = CompareArrays(expectedResult, result, 4);

            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        public void TestHorizontalsSE()
        {
            //TODO
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void TestHorizontalsNW()
        {
            //TODO
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void TestVerticalsNE()
        {
            var words = new string[] { "xyz" };

            var puzzleString = @"X A A A
                                 Y A A A
                                 Z A A A
                                 A A A A";

            var expectedResult = new char[4, 4]  {
                                                    { 'X', '+', '+', '+'},
                                                    { 'Y', '+', '+', '+'},
                                                    { 'Z', '+', '+', '+'},
                                                    { '+', '+', '+', '+'}
                                                };

            var result = GetPuzzleResult(puzzleString, words);
            bool isEqual = CompareArrays(expectedResult, result, 4);

            Assert.IsTrue(isEqual);
        }

        [TestMethod()]
        [Timeout(500)]
        public void TestTooLargeWordsWithTimeout()
        {
            var allWords = InputData.tooManyWordsString.Replace("\r\n", "\n").Split('\n');
            var wordsList = new List<string>(allWords);
            var words = wordsList.Select(x => x.Trim()).ToList();

            var result = GetPuzzleResult(InputData.puzzleString, words);
            Assert.IsTrue(true);
        }

        private char[,] GetPuzzleResult(string puzzleString, IEnumerable<string> words)
        {
            IChunksExtractor exractor = new Extractor();
            ISearch searcher = new Searcher();
            var puzzle = new Puzzle(exractor, searcher);
            return puzzle.SolvePuzzle(puzzleString, words);
        }

        private bool CompareArrays(char[,] arr1, char[,] arr2, int w)
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (arr1[i, j] != arr2[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}