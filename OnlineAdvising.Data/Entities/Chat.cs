using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAdvising.Data.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public int? AppointmentId { get; set; }
        public DateTime? FirstMessageSentAt { get; set; }
        
        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
    }
}