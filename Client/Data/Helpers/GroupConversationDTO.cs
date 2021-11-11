using Data.Models;
using System.Collections.Generic;

namespace Data.Helpers
{
    public class GroupConversationDTO
    {
        public string GroupName { get; set; }
        public ConversationType ConversationType { get; set; }
        public List<BookUser> GroupMembers { get; set; }
    }
}
