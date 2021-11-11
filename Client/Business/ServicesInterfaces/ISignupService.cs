using Data.Helpers;
using System.IO;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ISignupService
    {
        Task<SignupResult> SignupAsync(SignupUserDTO signupUser);
        Task<bool> UploadPhotoAsync(Stream image, string fileName);
    }
}