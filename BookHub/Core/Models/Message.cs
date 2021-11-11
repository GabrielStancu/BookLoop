using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Message : BaseModel
    {
        public string MessageContent { get; set; }
        [ForeignKey("Sender")]
        public int SenderId { get; set; }
        public BookUser Sender { get; set; }
        public int ConversationId { get; set; }
        public ConversationType ConversationType { get; set; }
        public DateTime SendTime { get; set; }
    }
}
