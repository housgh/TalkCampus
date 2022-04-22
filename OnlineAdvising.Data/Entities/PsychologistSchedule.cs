using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAdvising.Data.Entities
{
    public class PsychologistSchedule
    {
        public int Id { get; set; }
        public int? PsychologistId { get; set; }
        public double TotalHours { get; set; }
        
        [ForeignKey("PsychologistId")]
        public virtual User Psychologist { get; set; }

        public virtual List<ScheduleDay> ScheduleDays { get; set; }
    }
}