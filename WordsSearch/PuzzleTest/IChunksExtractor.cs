using System.Collections.Generic;

namespace PuzzleTest
{
    public interface IChunksExtractor
    {
        int Width { get; }
        List<string> HorizontalChunks { get; }
        List<string> VerticalChunks { get; }
        List<string> DiagonalLeftChunks { get; }
        List<string> DiagonalRightChunks { get; }
        void ExtractChunks(string puzzleString);
    }
}
