using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleTest
{
    public class Puzzle : IPuzzle
    {
        private List<string> _words;
        private string _puzzleString = "";
        private readonly IChunksExtractor _extractor;
        private readonly ISearch _searcher;

        public Puzzle(IChunksExtractor extractor, ISearch searcher)
        {
            _extractor = extractor;
            _searcher = searcher;
        }

        public char[,] SolvePuzzle(string puzzle, IEnumerable<string> searchingWords)
        {
            _puzzleString = puzzle.ClearPuzzleString();
            _words = new List<string>();

            foreach (var w in searchingWords)
            {
                if (!String.IsNullOrEmpty(w) && w.Length > 1)
                {
                    _words.Add(w.ClearPuzzleString());
                }
            }

            _extractor.ExtractChunks(_puzzleString);

            var reversedWords = new List<string>();
            _words.ForEach(w => { reversedWords.Add(w.ReverseString()); });
            reversedWords.AddRange(_words);

            _searcher.SearchWords(_extractor, reversedWords);

            return _searcher.SolvedPuzzle;
        }
    }
}
