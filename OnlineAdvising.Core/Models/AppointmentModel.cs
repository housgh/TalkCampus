using System;

namespace OnlineAdvising.Core.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public int? PsychologistId { get; set; }
        public int? AppointmentStatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime FirstMessageSentAt { get; set; }
        public DateTime TimeEnded { get; set; }
    }
}