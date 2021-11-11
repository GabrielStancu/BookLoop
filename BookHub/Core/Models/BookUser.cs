using System.Collections.Generic;

namespace Core.Models
{
    public class BookUser : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoFileName { get; set; }
        public List<BookPost> BookPosts { get; set; }
    }
}
