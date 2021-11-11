using Core.Models;
using System;

namespace Infrastructure.DTOs
{
    public class MessageDTO
    {
        public string MessageContent { get; set; }
        public BookUserDTO Sender { get; set; }
        public int CurrentUserId { get; set; }
        public int ConversationId { get; set; }
        public ConversationType ConversationType { get; set; }
        public DateTime SendTime { get; set; }
    }
}
