using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAdvising.Data.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public int? ReportedId { get; set; }
        public string Reason { get; set; }
        
        [ForeignKey("ReportedId")]
        public virtual User Reported { get; set; }
    }
}