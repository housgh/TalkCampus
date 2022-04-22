namespace OnlineAdvising.Core.Models
{
    public class ReportModel
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int? ReportedId { get; set; }
        public string Reason { get; set; }
    }
}