using System.Collections.Generic;

namespace BrailleTranslator
{
    public interface IBrailleVisualizer
    {
        void Print(IEnumerable<char[,]> brailleCharacters);
    }
}
