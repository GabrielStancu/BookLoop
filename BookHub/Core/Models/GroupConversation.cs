using Core.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Table("GroupConversation")]
    public class GroupConversation : Conversation
    {
        public string Name { get; set; }
        [NotMapped]
        public IList<GroupUser> GroupUsers { get; set; }
    }
}
