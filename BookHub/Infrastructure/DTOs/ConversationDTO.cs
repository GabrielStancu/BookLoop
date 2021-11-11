using Core.Models;
using System;

namespace Infrastructure.DTOs
{
    public class ConversationDTO
    {
        public int Id { get; set; }
        public ConversationType ConversationType { get; set; }
        public string ConversationPhoto { get; set; }
        public string ConversationName { get; set; }
        public string ConversationLastMessage { get; set; }
        public DateTime LastMessageSendTime { get; set; }
    }
}
