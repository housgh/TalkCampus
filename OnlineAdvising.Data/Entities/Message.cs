using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAdvising.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public int? ChatId { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }
        
        [ForeignKey("ChatId")]
        public virtual Chat Chat { get; set; }
        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }
        [ForeignKey("ReceiverId")]
        public virtual User Receiver { get; set; }
    }
}