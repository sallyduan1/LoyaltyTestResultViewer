using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyaltyTestResultViewer.Models
{
    /// <summary>
    /// one test result match to one trx file
    /// </summary>
    public class TestResult
    {
        public DateTime Date { get; set; }
        public IEnumerable<TestCase> TestCases { get; set; }

        public int Passed { get; set; }
        public int Failed { get; set; }
        public int Aborted { get; set; }
        public int Inconclusive { get; set; }
        public int Pending { get; set; }
        public int Total { get; set; }

    }
}