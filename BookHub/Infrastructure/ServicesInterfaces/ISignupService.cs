using Infrastructure.DTOs;
using Infrastructure.Helpers;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ISignupService
    {
        Task<SignupResult> SignupAsync(SignupUserDTO signupUser);
    }
}