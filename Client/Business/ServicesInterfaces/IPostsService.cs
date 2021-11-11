using Data.Helpers;
using Data.Models;
using Data.Specification;
using System.IO;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IPostsService
    {
        Task<bool> DeletePostAsync(BookPost bookPost);
        Task<BooksSearchResultDTO> GetBooksWithSpecificationAsync(Specification specification);
        Task<bool> MakePostAsync(BookPost bookPost);
        Task<bool> UploadPhotoAsync(Stream image, string fileName);
    }
}