using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineAdvising.Data.Entities
{
    public class ScheduleDay
    {
        public int Id { get; set; }
        public int? ScheduleId { get; set; }
        public int? DayOfWeekId { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        
        [ForeignKey("DayOfWeekId")]
        public virtual DayOfWeek DayOfWeek { get; set; }
        [ForeignKey("ScheduleId")]
        [JsonIgnore]
        public PsychologistSchedule Schedule { get; set; }
        
    }
}