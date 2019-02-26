using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class WordController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            try
            {
                List<Word> listWords = (List<Word>)HttpRuntime.Cache.Get("listWords");

                if (listWords == null)//not in cache
                {
                    string szAppPath = "", szSolutionPath = "", szAddress = "";

                    if (HostingEnvironment.ApplicationPhysicalPath != null)
                    {
                        szAppPath = HostingEnvironment.ApplicationPhysicalPath;
                        szSolutionPath = Path.GetFullPath(Path.Combine(szAppPath, @"..\"));
                        szAddress = szSolutionPath + "/xmlFiles/words.xml";
                    }
                    else
                    {
                        szAppPath = System.Environment.CurrentDirectory;
                        szSolutionPath = Path.GetFullPath(Path.Combine(szAppPath, @"..\..\..\"));
                        szAddress = szSolutionPath + "/xmlFiles/words.xml";
                    }

                    //Read and Parse XML, then assign into variable
                    listWords = fnReadAndParseXML(szAddress);
                    //Sort word list, according to counts
                    listWords.Sort(fnSortWords);
                    //Set viewbag for html rendering
                    listWords = listWords.GetRange(0, 10);

                    HttpRuntime.Cache.Insert("listWords", listWords);
                }

                //Set viewbag for html rendering
                ViewBag.listWords = listWords;

                return View("List");
            }
            catch (Exception e)
            {
                //Set error
                ViewBag.szError = e.Message;

                return View("ListError");
            }
        }

        public int fnSortWords(Word x, Word y)
        {
            try
            {
                if (x.iCount > y.iCount)
                {
                    return -1;
                }
                else if (x.iCount < y.iCount)
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<Word> fnReadAndParseXML(string szAddress)
        {
            try
            {
                List<Word> listWords = new List<Word>();
                // Create an XML reader for this file.
                using (XmlReader reader = XmlReader.Create(szAddress))
                {
                    while (reader.Read())
                    {
                        switch (reader.Name)
                        {
                            case "word":
                                listWords.Add(new Word
                                {
                                    szText = reader["text"],
                                    iCount = Convert.ToInt16(reader["count"])
                                });
                                break;
                            default:
                                break;
                        }
                    }
                }

                return listWords;
            }
            catch (Exception e)
            {
                throw new Exception("fnReadAndParseXML failed. Exception: " + e.ToString());
            }
        }
    }
}