using System;
using System.Collections.Generic;

namespace Data.Models
{
    public class BookUser: BaseModel, IEquatable<BookUser>
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName} {LastName}"; }
        public string Email { get; set; }
        public string PhotoFileName { get; set; }
        public List<BookPost> BookPosts { get; set; }

        public bool Equals(BookUser other)
        {
            if (Id == other.Id || Username == other.Username)
            {
                return true;
            }

            return false;
        }
    }
}
