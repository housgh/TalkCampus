using System.Collections.Generic;
using OnlineAdvising.Data.Entities;

namespace OnlineAdvising.Core.Models
{
    public class ScheduleModel
    {
        public int Id { get; set; }
        public int? PsychologistId { get; set; }
        public double TotalHours { get; set; }

        public List<ScheduleDay> ScheduleDays { get; set; }
    }
}