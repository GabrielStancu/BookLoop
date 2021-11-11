using Core.Models;
using System.Collections.Generic;

namespace Infrastructure.DTOs
{
    public class BookUserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoFileName { get; set; }
        public List<BookPost> BookPosts { get; set; }
    }
}
