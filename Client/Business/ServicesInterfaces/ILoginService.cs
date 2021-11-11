using Data.Helpers;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(LoginUserDTO loginUser);
    }
}