using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using BrailleTranslator.Data;

namespace BrailleTranslator
{
    public class Translator : ITranslator
    {
        private readonly BrailleDictionary _brailleDictionary;

        public Translator(BrailleDictionary brailleDictionary)
        {
            _brailleDictionary = brailleDictionary;
        }

        public IEnumerable<char[,]> TranslateToBraille(string textToTranslate)
        {
            var brailleCharacters = new List<char[,]>();

            //first, we replace all the contracted entities with Unicode
            var contractedString = ConvertContractedCharsToUnicode(textToTranslate);

            //we have to escape the formatting characters from being replaced
            contractedString = ConvertFormattingCharsToUnicode(contractedString);

            //replace braille contraction unicode with patterns
            contractedString = ConvertContractedUnicodeToPatterns(contractedString);

            //translate single characters to their respective braille equivalents
            var brailleString = TranslateSingleCharacters(contractedString);

            //finally, we replace the escaped formatting characters "O" and "." with their respective braille formatting characters
            brailleString = ConvertFormattingUnicodeToPattern(brailleString);

            string nonTranslatableChars;
            var hasNonTranslatableCharacters = HasNonTranslatableCharacters(brailleString, out nonTranslatableChars);

            if (hasNonTranslatableCharacters)
            {
                throw new InvalidOperationException($"The input string has some non-supported characters: {GetUniqueCharacters(nonTranslatableChars)}");
            }

            var braillePatterns = GetBraillePatterns(brailleString).ToList();

            braillePatterns.ForEach(pattern =>
            {
                var brailleChar = BrailleCharacter.Create(pattern);
                brailleCharacters.Add(brailleChar);
            });

            return brailleCharacters;
        }

        private string ConvertContractedCharsToUnicode(string str)
        {
            foreach (var key in _brailleDictionary.ContractionToUnicode.Keys)
            {
                str = ReplaceWithContractedBraille(str, key, _brailleDictionary.ContractionToUnicode[key]);
            }

            return str;
        }

        private string ConvertContractedUnicodeToPatterns(string str)
        {
            foreach (var key in _brailleDictionary.ContractionToUnicode.Keys)
            {
                str = str.Replace(_brailleDictionary.ContractionToUnicode[key], _brailleDictionary.ContractionToPattern[_brailleDictionary.ContractionToUnicode[key]]);
            }

            return str;
        }

        private string ConvertFormattingCharsToUnicode(string str)
        {
            foreach (var key in _brailleDictionary.FormattingCharsToUnicode.Keys)
            {
                str = str.Replace(key.ToString(), _brailleDictionary.FormattingCharsToUnicode[key]);
            }

            return str;
        }

        private string ConvertFormattingUnicodeToPattern(string str)
        {
            foreach (var key in _brailleDictionary.FormattingUnicodeToPattern.Keys)
            {
                str = str.Replace(key, _brailleDictionary.FormattingUnicodeToPattern[key]);
            }

            return str;
        }

        private string TranslateSingleCharacters(string str)
        {
            var braille = new StringBuilder();

            braille.Append(str);
            foreach (var key in _brailleDictionary.BasicToPattern.Keys)
            {
                var keyString = key.ToString();
                braille.Replace(keyString, _brailleDictionary.BasicToPattern[key]);
            }

            return braille.ToString();
        }

        private string ReplaceWithContractedBraille(string searchIn, string searchWord, string replaceWith)
        {
            string pattern = string.Format(@"\b{0}\b", searchWord);
            string ret = Regex.Replace(searchIn, pattern, replaceWith, RegexOptions.IgnoreCase);
            return ret;
        }

        private IEnumerable<string> GetBraillePatterns(string str)
        {
            var brailleCharSize = BrailleCharacter.Size;

            for (int i = 0; i < str.Length; i += brailleCharSize)
            {
                if (str.Length < i + brailleCharSize)
                {
                    throw new Exception("The input text is not in the Braille format.");
                }

                yield return str.Substring(i, brailleCharSize);
            }
        }

        private bool HasNonTranslatableCharacters(string value, out string nonTranslatableChars)
        {
            nonTranslatableChars = value.Replace("O", "").Replace(".", "");
            return nonTranslatableChars.Length > 0;
        }

        private static string GetUniqueCharacters(string str)
        {
            string uiqueChars = string.Empty;
            foreach (char a in str)
            {
                if (uiqueChars.Contains(a.ToString().ToLower()) || uiqueChars.Contains(a.ToString().ToUpper()))
                {
                    continue;
                }

                uiqueChars += a.ToString();
            }
            return uiqueChars;
        }
    }
}
