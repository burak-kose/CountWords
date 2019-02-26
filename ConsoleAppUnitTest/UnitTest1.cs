using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp;
using System.Collections.Generic;

namespace ConsoleAppUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_fnRemoveSpecialCharacters()
        {
            try
            {
                string szText = "example.text";
                string szExpected = "example text";
                string szActual = ConsoleApp.Program.fnRemoveSpecialCharacters(szText);

                Assert.AreEqual(szActual, szExpected);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Test_fnDownloadText()
        {
            try
            {
                string szAddress = "http://www.gutenberg.org/files/2701/2701-0.txt";
                string szText = ConsoleApp.Program.fnDownloadText(szAddress);

                Assert.AreNotEqual(szText, "");
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Test_fnSplitAndFormatWords()
        {
            try
            {
                string szText = "Apple Banana Orange Apple Banana";
                Dictionary<string, int> dictWordList = new Dictionary<string, int>();

                dictWordList = ConsoleApp.Program.fnSplitAndFormatWords(szText, dictWordList);

                Assert.AreEqual(dictWordList.Count, 3);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }
    }
}
