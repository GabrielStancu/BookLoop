using Data.Models;
using System.Collections.Generic;

namespace Data.Helpers
{
    public class BooksSearchResultDTO
    {
        public IReadOnlyList<BookPost> BookPosts { get; set; }
        public int PostsCount { get; set; }
    }
}
