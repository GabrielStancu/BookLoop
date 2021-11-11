using Core.Models;
using System.Collections.Generic;

namespace Infrastructure.DTOs
{
    public class GroupConversationDTO
    {
        public string GroupName { get; set; }
        public ConversationType ConversationType { get; set; }
        public List<BookUserDTO> GroupMembers { get; set; }
    }
}
