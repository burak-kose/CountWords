using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Controllers;
using WebApp.Models;
using System.Web.Hosting;
using System.IO;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebAppUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_ReadAndParseXML()
        {
            try
            {
                WordController controller = new WordController();

                string szAppPath = System.Environment.CurrentDirectory;
                string szSolutionPath = Path.GetFullPath(Path.Combine(szAppPath, @"..\..\..\"));
                string szAddress = szSolutionPath + "/xmlFiles/words.xml";

                List<Word> listWords = controller.fnReadAndParseXML(szAddress);

                Assert.IsNotNull(listWords);
            }
            catch (Exception e)
            {
                Assert.Fail();

            }
        }

        [TestMethod]
        public void Test_SortWords()
        {
            WordController controller = new WordController();

            List<Word> listWords = new List<Word>();
            listWords.Add(new Word
            {
                szText = "Word1",
                iCount = 10
            });
            listWords.Add(new Word
            {
                szText = "Word2",
                iCount = 30
            });
            listWords.Sort(controller.fnSortWords);

            Assert.IsTrue(listWords[0].iCount > listWords[1].iCount);
        }

        [TestMethod]
        public void Test_ActionResultList()
        {
            try
            {
                WordController controller = new WordController();
                ViewResult viewResult = controller.List() as ViewResult;

                Assert.IsNotNull(viewResult.ViewBag);
                Assert.IsNotNull(viewResult.ViewBag.listWords);
            }
            catch (Exception e) {
                Assert.Fail();
            }
        }
    }
}
