using Data.Models;
using System.IO;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IUserService
    {
        Task<BookUser> SelectUserWithInfoByIdAsync(int id);
        Task<BookUser> SelectUserWithInfoByUsernameAsync(string username);
        Task<bool> UpdateProfileChangesAsync(BookUser bookUser);
        Task<bool> UploadPhotoAsync(Stream image, string fileName);
    }
}