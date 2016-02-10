using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using LoyaltyTestResultViewer.Models;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace LoyaltyTestResultViewer.Controllers
{
    public class ReportController : ApiController
    {
        //TestResult[] results = new TestResult[]
        //{
        //    new TestResult { Date = DateTime.Today, TestCases = new TestCase[]
        //    {
        //        new TestCase { TestName = "Test1", ComputerName = "pc1", Detail = "detail1", TestStatus = TestStatusType.Error},
        //        new TestCase { TestName = "Test2", ComputerName = "pc2", Detail = "detail2", TestStatus = TestStatusType.Error},
        //        new TestCase { TestName = "Test3", ComputerName = "pc3", Detail = "detail3", TestStatus = TestStatusType.Error}
        //    }},
        //    new TestResult { Date = DateTime.Today, TestCases = new TestCase[]
        //    {
        //        new TestCase { TestName = "Test4", ComputerName = "pc4", Detail = "detail4", TestStatus = TestStatusType.Error},
        //        new TestCase { TestName = "Test5", ComputerName = "pc5", Detail = "detail5", TestStatus = TestStatusType.Error}
        //    }},
        //};

        public IEnumerable<TestResult> GetTestResults()
        {
            return null;
        }

        public IEnumerable<TestResult> GetTestResults(int lastDays)
        {
            //todo: 1, get last 7 days data 
            string testDataPath = ConfigurationSettings.AppSettings["TestDataPath"];

            testDataPath = testDataPath.IsNullOrWhiteSpace() ? System.Web.Hosting.HostingEnvironment.MapPath(@"\TestData") : testDataPath;
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
                       select pair).Take(lastDays);

            //var filesSorted = (from file in files orderby file descending select file).Take(lastDays);
            var testRsults = filesSorted.Select(RetriveData).ToList();

            return testRsults;
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
