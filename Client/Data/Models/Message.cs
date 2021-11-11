using Data.Helpers;
using System;

namespace Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageContent { get; set; }
        public int SenderId { get; set; }
        public BookUser Sender { get; set; }
        public int ConversationId { get; set; }
        public ConversationType ConversationType { get; set; }
        public DateTime SendTime { get; set; }
    }
}
