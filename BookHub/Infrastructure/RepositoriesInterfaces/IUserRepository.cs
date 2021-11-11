using Core.Models;
using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IUserRepository : IGenericRepository<BookUser>
    {
        Task<BookUser> SelectUserWithLoginInfoAsync(string username, string password);
        Task<bool> CheckAlreadyRegisteredUserAsync(SignupUserDTO signupUserDTO);
        Task<BookUser> SelectUserInformationByIdAsync(int id);
        Task<bool> CheckEmailAlreadyInUse(BookUser bookUser);
        public void DetachLocal(BookUser bookUser);
        Task<BookUser> SelectUserInformationByUsernameAsync(string username);
    }
}