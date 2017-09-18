using System.Collections.Generic;

namespace PuzzleTest
{
    public interface ISearch
    {
        char[,] SolvedPuzzle { get; }
        void SearchWords(IChunksExtractor extractor, IEnumerable<string> words);
    }
}
