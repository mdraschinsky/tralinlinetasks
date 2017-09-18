using System.Collections.Generic;

namespace BrailleTranslator
{
    public interface ITranslator
    {
        IEnumerable<char[,]> TranslateToBraille(string text);
    }
}
