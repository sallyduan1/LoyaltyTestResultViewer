using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Xml.Linq;
using LoyaltyTestResultViewer.Models;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace LoyaltyTestResultViewer.Controllers
{

    public static class MiscExtensions
    {
        // Ex: collection.TakeLast(5);
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }
    }

    public class ReportController : ApiController
    {
        public XNamespace ns { get; set; }

        public IEnumerable<TestResult> GetTestResults()
        {
            return null;
        }

        public IEnumerable<TestResult> GetTestResults(int lastDays)
        {
            //todo: 1, get last 7 days data 
            var testDataPath = GetTestDataPath();
            var files = Directory.GetFiles(testDataPath);

            Dictionary<string, string> dateBasedFiles = new Dictionary<string, string>();
            files.ForEach(file =>
            {
                var parts = file.Split();
                var date = parts[parts.Length - 2];
                if (!dateBasedFiles.ContainsKey(date))
                {
                    dateBasedFiles[date] = file;
                }
                else if (String.CompareOrdinal(dateBasedFiles[date], file) < 0)
                {
                    dateBasedFiles[date] = file;
                }
            });

            var filesSorted = (from pair in dateBasedFiles
                               orderby pair.Value ascending
                               select pair).TakeLast(lastDays);

            //var filesSorted = (from file in files orderby file descending select file).Take(lastDays);
            var testRsults = filesSorted.Select(RetriveData).ToList();

            return testRsults;
        }

        private static string GetTestDataPath()
        {
            string testDataPath = ConfigurationSettings.AppSettings["TestDataPath"];

            testDataPath = testDataPath.IsNullOrWhiteSpace()
                ? System.Web.Hosting.HostingEnvironment.MapPath(@"\TestData")
                : testDataPath;
            return testDataPath;
        }

        public IEnumerable<TestCase> GetTestDetail(string fileName, string column)
        {
            var category = "";

            switch (column)
            {
                case "1":
                    category = "Passed";
                    break;
                case "2":
                    category = "Failed";
                    break;
                case "3":
                    category = "Inconclusive";
                    break;
                case "4":
                    category = "NotExecuted";
                    break;
                case "5":
                    category = "Pending";
                    break;
                case "6":
                    category = "Aborted";
                    break;
                default:
                    category = "Passed";
                    break;
            }

            XNamespace xmlns = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
            var testDataPath = GetTestDataPath();
            XDocument xdoc = XDocument.Load(testDataPath + "\\" + fileName);
            ns = xdoc.Root.Name.Namespace;

            var results = (from item in xdoc.Descendants(ns + "Results")
                           select item);
            //results.Elements().Count;
            var unitTestResultsByColumn = from UnitTestResult in results.Elements()
                                          where UnitTestResult.Attribute("outcome").Value == category
                                          select UnitTestResult;
            var result = unitTestResultsByColumn.Select(RetriveUnitTestData).ToList();
            return result;
        }

        private TestCase RetriveUnitTestData(XElement element)
        {
            string message;
            var messageElement = element.Descendants(ns + "Message").FirstOrDefault();
          
            if (messageElement == null) {  message = string.Empty; }
            else
            {
                message = messageElement.Value;
            }
            var testCase = new TestCase()
            {
                TestName = element.Attribute("testName").Value,
                ComputerName = element.Attribute("computerName").Value,
                Message = message
            };

            return testCase;
        }

        private static TestResult RetriveData(KeyValuePair<string, string> pair)
        {
            XNamespace xmlns = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
            XDocument xdoc = XDocument.Load(pair.Value);
            var ns = xdoc.Root.Name.Namespace;

            var countersElement = (from item in xdoc.Descendants(ns + "Counters")
                                   select item).FirstOrDefault();
            var testResult = new TestResult()
            {
                Date = pair.Key,
                FilePath = Path.GetFileName(pair.Value),
                Passed = Int32.Parse(countersElement.Attribute("passed").Value),
                Failed = Int32.Parse(countersElement.Attribute("failed").Value),
                Aborted = Int32.Parse(countersElement.Attribute("aborted").Value),
                Inconclusive = Int32.Parse(countersElement.Attribute("inconclusive").Value),
                Pending = Int32.Parse(countersElement.Attribute("pending").Value),
                NotExecuted = Int32.Parse(countersElement.Attribute("notExecuted").Value),
                Total = Int32.Parse(countersElement.Attribute("total").Value),
            };
            return testResult;
        }
    }
}
