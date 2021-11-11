using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IPhotoService
    {
        Task<bool> SavePhoto(string folderName, IFormFile file);
    }
}