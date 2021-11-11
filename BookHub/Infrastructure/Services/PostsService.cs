using Core.Models;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PostsService : IPostsService
    {
        private readonly IBookPostRepository _bookPostRepository;
        private readonly IPhotosUrlResolver _photosUrlResolver;

        public PostsService(IBookPostRepository bookPostRepository, IPhotosUrlResolver photosUrlResolver)
        {
            _bookPostRepository = bookPostRepository;
            _photosUrlResolver = photosUrlResolver;
        }

        public async Task<bool> MakePostAsync(BookPost bookPost)
        {
            await _bookPostRepository.InsertAsync(bookPost);
            return true;
        }

        public async Task<BooksSearchResultDTO> GetPostsWithSpecAsync(Specification specification)
        {
            var searchResult = await _bookPostRepository.GetPostsWithSpecificationAsync(specification);
            foreach (var bookPost in searchResult.BookPosts)
            {
                _photosUrlResolver.ResolveUrl(bookPost);
            }

            return searchResult;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            return await _bookPostRepository.DeletePostByIdAsync(id);
        }
    }
}
