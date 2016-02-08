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
            var filesSorted = (from file in files orderby file descending select file).Take(lastDays);
            var testRsults = filesSorted.Select(RetriveData).ToList();

            return testRsults;
        }

        private static TestResult RetriveData(string file)
        {
            XNamespace xmlns = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
            XDocument xdoc = XDocument.Load(file);
            var ns = xdoc.Root.Name.Namespace;

            var countersElement = (from item in xdoc.Descendants(ns + "Counters")
                                   select item).FirstOrDefault();
            var testResult = new TestResult()
            {
                Passed = Int32.Parse(countersElement.Attribute("passed").Value),
                Failed = Int32.Parse(countersElement.Attribute("failed").Value),
                Aborted = Int32.Parse(countersElement.Attribute("aborted").Value),
                Inconclusive = Int32.Parse(countersElement.Attribute("inconclusive").Value),
                Pending = Int32.Parse(countersElement.Attribute("pending").Value),
                Total = Int32.Parse(countersElement.Attribute("total").Value),
            };
            return testResult;
        }
    }
}
