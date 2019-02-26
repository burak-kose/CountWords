using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string szText;
                Dictionary<string, int> dictWordList = new Dictionary<string, int>();

                //Download text file
                szText = fnDownloadText("http://www.gutenberg.org/files/2701/2701-0.txt");

                //Delete special characters from text 
                szText = fnRemoveSpecialCharacters(szText);

                //Split and Format words
                dictWordList = fnSplitAndFormatWords(szText, dictWordList);

                //Export XML File
                fnExportXMLFile(dictWordList);

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("\nProgram failed... Error: " + e.ToString());
                Console.ReadKey();
            }
        }

        public static string fnDownloadText(string szAddress)
        {
            try
            {
                Console.WriteLine("Text file downloading...");

                WebClient client = new WebClient();
                string szDownloadedText = client.DownloadString(szAddress);

                Console.WriteLine("*** Text file download succcessfully...");

                return szDownloadedText;
            }
            catch (Exception e)
            {
                throw new Exception("fnDownloadText failed. Exception: " + e.ToString());
            }
        }

        public static string fnRemoveSpecialCharacters(string szText)
        {
            try
            {
                string str = szText.Replace(Environment.NewLine, " ");
                return Regex.Replace(str, "[^a-zA-Z0-9 ]+", " ", RegexOptions.Compiled);
            }
            catch (Exception e)
            {
                throw new Exception("fnRemoveSpecialCharacters failed. Exception: " + e.ToString());
            }
        }

        public static Dictionary<string, int> fnSplitAndFormatWords(string szText, Dictionary<string, int> dictWordList)
        {
            try
            {
                //Split text into words
                string[] listTextWords = szText.Split(' ');

                //Formating words and add to dictionary
                foreach (string word in listTextWords)
                {
                    string szFormattedWord = word.Trim().ToLower(new CultureInfo("en-US", false));
                    if (szFormattedWord != "")
                    {
                        if (dictWordList.ContainsKey(szFormattedWord))
                        {
                            dictWordList[szFormattedWord] += 1;
                        }
                        else
                        {
                            dictWordList.Add(szFormattedWord, 1);
                        }
                    }
                }

                return dictWordList;
            }
            catch (Exception e)
            {
                throw new Exception("fnDownloadText failed. Exception: " + e.ToString());
            }
        }

        public static void fnExportXMLFile(Dictionary<string, int> dictWordList)
        {
            try
            {
                Console.WriteLine("XML file creating...");

                XmlWriter xmlWriter = XmlWriter.Create("../../../xmlFiles/words.xml");

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("words");

                foreach (string key in dictWordList.Keys)
                {
                    xmlWriter.WriteStartElement("user");
                    xmlWriter.WriteAttributeString("text", key);
                    xmlWriter.WriteAttributeString("count", dictWordList[key].ToString());
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();

                Console.WriteLine("*** XML file created...");
            }
            catch (Exception e)
            {
                throw new Exception("fnExportXMLFile failed. Exception: " + e.ToString());
            }
        }
    }
}
