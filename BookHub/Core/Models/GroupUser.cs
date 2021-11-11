using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class GroupUser : BaseModel
    {
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public GroupConversation Group { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public BookUser User { get; set; }
    }
}
