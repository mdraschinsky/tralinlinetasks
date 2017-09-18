using System;
using System.Text;
using BrailleTranslator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BrailleTranslator.Data;
using BrailleTranslator.Presentation;

namespace BrailleTranslatorTests
{
    [TestClass]
    public class TranslatorTests
    {
        [TestMethod]
        public void TestBasicBraille()
        {
            TestBrailleTranslation("test", ".OOOO.O..O...OO.O..OOOO.");
        }

        [TestMethod]
        public void TestContractedBraille()
        {
            TestBrailleTranslation("this", "OO.O.O");
        }

        [TestMethod]
        public void TestBasicAndContractedBraille()
        {
            TestBrailleTranslation("test this", ".OOOO.O..O...OO.O..OOOO.......OO.O.O");
        }

        [TestMethod]
        public void TestUnsupportedInput()
        {
            try
            {
                TestBrailleTranslation("[]", "");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }


        public void TestBrailleTranslation(string inputString, string expectedResult)
        {
            var brailleDictionary = new BrailleDictionary();

            var brailleChar = new BrailleCharacter(brailleDictionary);
            var translator = new Translator(brailleDictionary);

            var brailleCharacters = translator.TranslateToBraille(inputString);
            var sb = new StringBuilder();
            foreach (var bc in brailleCharacters)
            {
                sb.Append(brailleChar.BrailleCharToPattern(bc));
            }

            var receivedResult = sb.ToString();

            Assert.IsTrue(receivedResult == expectedResult);
        }
    }
}
