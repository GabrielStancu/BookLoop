using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IUserService
    {
        Task<BookUserDTO> GetUserInfoByIdAsync(int id);
        Task<bool> SaveProfileChangesAsync(BookUserDTO bookUserDTO);
        Task<BookUserDTO> GetUserInfoByUsernameAsync(string username);
    }
}