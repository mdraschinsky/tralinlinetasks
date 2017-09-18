using System.Collections.Generic;

namespace PuzzleTest
{
    public interface IPuzzle
    {
        char[,] SolvePuzzle(string puzzle, IEnumerable<string> searchingWords);
    }
}
