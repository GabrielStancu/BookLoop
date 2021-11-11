using Data.Helpers;
using Data.Models;
using Data.Specification;

namespace Business.Shared
{
    public class DataContainer
    {
        public static BookUser User { get; set; }
        public static BookPost BookPost { get; set; }
        public static Specification Specification { get; set; } = new Specification();
        public static ConversationDTO ConversationDTO { get; set; } = new ConversationDTO();

        public static int GetUserId() =>
            User.Id;
    }
}
