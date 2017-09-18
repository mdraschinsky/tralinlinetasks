using System.Collections.Generic;

namespace BrailleTranslator.Data
{
    public class BrailleDictionary
    {
        #region "Contraction to Unicode"

        internal readonly Dictionary<string, string> ContractionToUnicode = new Dictionary<string, string>()
        {
            { "but", "\u2803"},
            {"can", "\u2809"},
            {"do", "\u2819"},
            {"every", "\u2811"},
            {"from", "\u2811"},
            {"go", "\u281B"},
            {"have", "\u2813"},
            {"I", "\u280a"},
            {"just", "\u281a"},
            {"knowledge", "\u2805"},
            {"like", "\u2807"},
            {"more", "\u280d"},
            {"not", "\u281d"},
            {"people", "\u280f"},
            {"quite", "\u281f"},
            {"rather", "\u2817"},
            {"so", "\u280e"},
            {"that", "\u281e"},
            {"still", "\u280c"},
            {"us", "\u2825"},
            {"very", "\u2827"},
            {"it", "\u282d"},
            {"you", "\u283d"},
            {"as", "\u2835"},
            {"child", "\u2821"},
            {"shall", "\u2829"},
            {"this", "\u2839"},
            {"which", "\u2831"},
            {"out", "\u2833"},
            {"will", "\u283a"},
            {"be", "\u2806"},
            {"enough", "\u2822"},
            {"were", "\u2836"},
            {"his", "\u2826"},
            {"in", "\u2814"},
            {"was", "\u2834"}
        };

        #endregion

        #region "Contraction to Pattern"

        internal readonly Dictionary<string, string> ContractionToPattern = new Dictionary<string, string>()
        {
            {"\u2803", "O.O..."},
            {"\u2809", "OO...."},
            {"\u2819", "OO.O.."},
            {"\u2811", "O..O.."},
            {"\u280B", "OOO..."},
            {"\u281B", "OOOO.."},
            {"\u2813", "O.OO.."},
            {"\u280a", ".OO..."},
            {"\u281a", ".OOO.."},
            {"\u2805", "O...O."},
            {"\u2807", "O.O.O."},
            {"\u280d", "OO..O."},
            {"\u281d", "OO.OO."},
            {"\u280f", "OOO.O."},
            {"\u281f", "OOOOO."},
            {"\u2817", "O.OOO."},
            {"\u280e", ".OO.O."},
            {"\u281e", ".OOOO."},
            {"\u280c", ".O..O."},
            {"\u2825", "O...OO"},
            {"\u2827", "O.O.OO"},
            {"\u282d", "OO..OO"},
            {"\u283d", "OO.OOO"},
            {"\u2835", "O..OOO"},
            {"\u2821", "O....O"},
            {"\u2829", "OO...O"},
            {"\u2839", "OO.O.O"},
            {"\u2831", "O..O.O"},
            {"\u2833", "O.OO.O"},
            {"\u283a", ".OOO.O"},
            {"\u2806", "..O.O."},
            {"\u2822", "..O..O"},
            {"\u2836", "..OOOO"},
            {"\u2826", "..O.OO"},
            {"\u2814", "...OO."},
            {"\u2834", "...OOO"}
        };

        #endregion

        #region "Basic to Pattern"

        internal readonly Dictionary<char, string> BasicToPattern = new Dictionary<char, string>()
        {
            {'a', "O....."},
            {'b', "O.O..."},
            {'c', "OO...."},
            {'d', "OO.O.."},
            {'e', "O..O.."},
            {'f', "OOO..."},
            {'g', "OOOO.."},
            {'h', "O.OO.."},
            {'i', ".OO..."},
            {'j', ".OOO.."},
            {'k', "O...O."},
            {'l', "O.O.O."},
            {'m', "OO..O."},
            {'n', "OO.OO."},
            {'o', "O..OO."},
            {'p', "OOO.O."},
            {'q', "OOOOO."},
            {'r', "O.OOO."},
            {'s', ".OO.O."},
            {'t', ".OOOO."},
            {'u', "O...OO"},
            {'v', "O.O.OO"},
            {'w', ".OOO.O"},
            {'x', "OO..OO"},
            {'y', "OO.OOO"},
            {'z', "O..OOO"},

            {'A', ".....OO....."},
            {'B', ".....OO.O..."},
            {'C', ".....OOO...."},
            {'D', ".....OOO.O.."},
            {'E', ".....OO..O.."},
            {'F', ".....OOOO..."},
            {'G', ".....OOOOO.."},
            {'H', ".....OO.OO.."},
            {'I', ".....O.OO..."},
            {'J', ".....O.OOO.."},
            {'K', ".....OO...O."},
            {'L', ".....OO.O.O."},
            {'M', ".....OOO..O."},
            {'N', ".....OOO.OO."},
            {'P', ".....OOOO.O."},
            {'Q', ".....OOOOOO."},
            {'R', ".....OO.OOO."},
            {'S', ".....O.OO.O."},
            {'T', ".....O.OOOO."},
            {'U', ".....OO...OO"},
            {'V', ".....OO.O.OO"},
            {'W', ".....O.OOO.O"},
            {'X', ".....OOO..OO"},
            {'Y', ".....OOO.OOO"},
            {'Z', ".....OO..OOO"},

            {' ', "......"},

            {':', "..OO.."},
            {'-', "....OO"},

            {',', "..O..."},
            {';', "..O.O."},
            {'!', "..OOO."},
            {'(', "..OOOO"},
            {')', "..OOOO"},
            {'?', "..O.OO"},
            {'/', ".O..O."},
            {'*', "...OO."},
            {'`', "....O."},
            {'_', "....OO"},

            {'1', "O....."},
            {'2', "O.O..."},
            {'3', "OO...."},
            {'4', "OO.O.."},
            {'5', "O..O.."},
            {'6', "OOO..."},
            {'7', "OOOO.."},
            {'8', "O.OO.."},
            {'9', ".OO..."},
            {'0', ".OOO.."}
        };

        #endregion

        #region "Formatting character conversion"

        internal readonly Dictionary<char, string> FormattingCharsToUnicode = new Dictionary<char, string>()
        {
            {'O', "\u2815"},
            {'.', "\u2832"}
        };

        internal readonly Dictionary<string, string> FormattingUnicodeToPattern = new Dictionary<string, string>()
        {
            {"\u2815", ".....OO..OO."},
            {"\u2832", "..OO.O"}
        };

        #endregion
    }
}
