namespace LoyaltyTestResultViewer.Models
{
    public enum TestStatusType
    {
        Success, 
        Error, 
        Timeout,
        InConclusive
    }
    public class TestCase
    {
        public TestStatusType TestStatus { get; set; }
        public string TestName { get; set; }
        public string ComputerName { get; set; }
        public string Detail { get; set; }
    }
}