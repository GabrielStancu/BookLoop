using Core.Models;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IPostsService
    {
        Task<bool> MakePostAsync(BookPost bookPost);
        Task<BooksSearchResultDTO> GetPostsWithSpecAsync(Specification specification);
        Task<bool> DeletePostAsync(int id);
    }
}