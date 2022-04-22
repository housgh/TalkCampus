using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAdvising.Data.Entities
{
    public class Rate
    {
        public int Id { get; set; }
        public int? RatedId { get; set; }
        public double RateValue { get; set; }
        
        [ForeignKey("RatedId")]
        public virtual User Rated { get; set; }
    }
}