using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    [Table("PrivateConversation")]
    public class PrivateConversation : Conversation
    {    
        [ForeignKey("User1")]
        public int User1Id { get; set; }
        public BookUser User1 { get; set; }
        [ForeignKey("User2")]
        public int User2Id { get; set; }
        public BookUser User2 { get; set; }
    }
}
