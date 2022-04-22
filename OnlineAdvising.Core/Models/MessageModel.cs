using System;

namespace OnlineAdvising.Core.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public int? ChatId { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }
    }
}