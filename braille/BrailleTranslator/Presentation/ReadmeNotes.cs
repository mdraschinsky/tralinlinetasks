using System;
using System.Text;

using BrailleTranslator.Data;

namespace BrailleTranslator.Presentation
{
    public class ReadmeNotes
    {
        private readonly BrailleDictionary _dictionary;

        public ReadmeNotes(BrailleDictionary dictionary)
        {
            _dictionary = dictionary;
        }

        public override string ToString()
        {
            var readmeNotes = new StringBuilder();

            readmeNotes.Append("This application translates text to Braille");
            readmeNotes.Append(Environment.NewLine);
            readmeNotes.Append(Environment.NewLine);

            readmeNotes.Append("The supported basic Braille characters are: " + Environment.NewLine);
            foreach (var key in _dictionary.BasicToPattern.Keys)
            {
                readmeNotes.Append($"{key}, ");
            }

            foreach (var key in _dictionary.FormattingCharsToUnicode.Keys)
            {
                readmeNotes.Append($"{key}, ");
            }

            readmeNotes.Append(Environment.NewLine);
            readmeNotes.Append(Environment.NewLine);
            readmeNotes.Append("the supported Contracted Braille entities are: " + Environment.NewLine);

            foreach (var key in _dictionary.ContractionToUnicode.Keys)
            {
                readmeNotes.Append($"{key}, ");
            }

            readmeNotes.Append(Environment.NewLine);
            readmeNotes.Append(Environment.NewLine);
            readmeNotes.Append("if there is a non-supported character in the input string, the application will display a message." + Environment.NewLine);
            readmeNotes.Append(Environment.NewLine);
            readmeNotes.Append(Environment.NewLine);

            readmeNotes.Append("One more thing to mention before you start using the application:");
            readmeNotes.Append(Environment.NewLine);
            readmeNotes.Append("For the long string, we do word wrap.");
            readmeNotes.Append(Environment.NewLine);
            readmeNotes.Append("For better user experience, we find a space (if it exists) and do the word wrap right after the space. It allows us not to split a Braille character.");

            readmeNotes.Append(Environment.NewLine);
            readmeNotes.Append(Environment.NewLine);

            return readmeNotes.ToString();
        }
    }
}
