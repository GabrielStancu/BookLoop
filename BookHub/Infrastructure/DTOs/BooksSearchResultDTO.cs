using System.Collections.Generic;

namespace Infrastructure.DTOs
{
    public class BooksSearchResultDTO
    {
        public IReadOnlyList<BookPostDTO> BookPosts { get; set; }
        public int PostsCount { get; set; }
    }
}
