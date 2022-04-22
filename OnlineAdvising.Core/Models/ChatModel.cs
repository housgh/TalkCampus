using System;

namespace OnlineAdvising.Core.Models
{
    public class ChatModel
    {
        public int Id { get; set; }
        public int? AppointmentId { get; set; }
        public DateTime FirstMessageSentAt { get; set; }
    }
}