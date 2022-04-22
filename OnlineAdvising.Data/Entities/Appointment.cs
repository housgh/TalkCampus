using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAdvising.Data.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public int? PsychologistId { get; set; }
        public int? AppointmentStatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? TimeEnded { get; set; }
        
        [ForeignKey("PatientId")]
        public virtual User Patient { get; set; }
        [ForeignKey("PsychologistId")]
        public virtual User Psychologist { get; set; }
        [ForeignKey("AppointmentStatusId")]
        public virtual Status AppointmentStatus { get; set; }
        
    }
}