using Infrastructure.Helpers;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(string username, string password);
    }
}