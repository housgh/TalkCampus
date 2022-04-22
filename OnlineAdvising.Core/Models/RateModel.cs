namespace OnlineAdvising.Core.Models
{
    public class RateModel
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int? RatedId { get; set; }
        public double RateValue { get; set; }
    }
}