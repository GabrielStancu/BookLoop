using System.IO;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IUploadPhotoService
    {
        Task<bool> UploadPhotoAsync(Stream image, string fileName, string apiURL);
    }
}