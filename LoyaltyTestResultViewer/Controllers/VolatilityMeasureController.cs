using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Mvc;


namespace LoyaltyTestResultViewer.Controllers
{     
    public class VolatilityMeasureController : Controller
    {
        // GET: WebTestTeamHome
        public ActionResult Index()
        {
            Dictionary<string, int> data = GetOverallTestResultDetails();
            return View ( data);
        }

        public object OverallJson(Dictionary<string, int> dic)
        {
            dic = GetOverallTestResultDetails();
            string json = JsonConvert.SerializeObject(dic, Formatting.Indented);
           
            return JsonConvert.DeserializeObject<object>(json);  
        }


        public Dictionary<string, int> GetOverallTestResultDetails()
        {
            Dictionary<string, int> Result = new Dictionary<string, int>();
            string testResultPath = @"C:\unstructedTimeDemo\LoyaltyTestResultViewer\LoyaltyTestResultViewer\TestData\sally2.trx";
            XNamespace xmlns = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
            XDocument xdoc = XDocument.Load(testResultPath);
            var ns = xdoc.Root.Name.Namespace;

            var totalNumber = (from item in xdoc.Descendants(ns + "Counters")
                               select item.Attribute("total").Value).FirstOrDefault();
            int _totalNumber = int.Parse(totalNumber);
            Result.Add("total", _totalNumber);

            var passNumber = (from item in xdoc.Descendants(ns + "Counters")
                              select item.Attribute("passed").Value).FirstOrDefault();
            int _passNumber = int.Parse(passNumber);
            Result.Add("passed", _passNumber);

            var failedNumber = (from item in xdoc.Descendants(ns + "Counters")
                                select item.Attribute("failed").Value).FirstOrDefault();
            int _failedNumber = int.Parse(failedNumber);
            Result.Add("failed", _failedNumber);

            var abortedNumber = (from item in xdoc.Descendants(ns + "Counters")
                                 select item.Attribute("aborted").Value).FirstOrDefault();
            int _abortedNumber = int.Parse(abortedNumber);
            Result.Add("aborted", _abortedNumber);

            var inconclusiveNumber = (from item in xdoc.Descendants(ns + "Counters")
                                 select item.Attribute("inconclusive").Value).FirstOrDefault();
            int _inconclusiveNumber = int.Parse(inconclusiveNumber);
            Result.Add("incconluive", _inconclusiveNumber);

            var pendingNumber = (from item in xdoc.Descendants(ns + "Counters")
                                 select item.Attribute("pending").Value).FirstOrDefault();
            int _pendingNumber = int.Parse(pendingNumber);
            Result.Add("pending", _pendingNumber);
            return Result;
        }

    }
}

