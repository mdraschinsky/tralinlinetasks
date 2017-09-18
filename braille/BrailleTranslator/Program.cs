using System;

using BrailleTranslator.Data;
using BrailleTranslator.Presentation;

namespace BrailleTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var brailleDictionary = new BrailleDictionary();
                var readMeNotes = new ReadmeNotes(brailleDictionary);
                Console.WriteLine(readMeNotes.ToString());

                Console.Write(readMeNotes.ToString());
                Console.WriteLine("");

                Console.WriteLine("Please enter text to translate: ");
                var inputString = Console.ReadLine();

                if (!string.IsNullOrEmpty(inputString))
                {
                    var brailleChar = new BrailleCharacter(brailleDictionary);
                    var brailleVisualizer = new BrailleVisualizer(brailleChar, brailleDictionary, Console.Out);
                    var translator = new Translator(brailleDictionary);

                    try
                    {
                        var brailleCharacters = translator.TranslateToBraille(inputString);
                        Console.WriteLine("Translated text:");
                        Console.WriteLine(string.Empty);
                        brailleVisualizer.Print(brailleCharacters);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                Console.WriteLine("If you would like to exit the program, press 'Y'. Otherwise, press any key.");
                var answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && answer.ToUpper() == "Y")
                {
                    break;
                }
            }
        }
    }
}
