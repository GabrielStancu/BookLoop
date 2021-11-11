using Core.Models;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IBookPostRepository: IGenericRepository<BookPost>
    {
        Task<BooksSearchResultDTO> GetPostsWithSpecificationAsync(Specification specification);
        Task<bool> DeletePostByIdAsync(int id);
    }
}