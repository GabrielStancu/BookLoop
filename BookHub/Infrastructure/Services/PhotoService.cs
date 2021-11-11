using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PhotoService : IPhotoService
    {
        public async Task<bool> SavePhoto(string folderName, IFormFile file)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (file.Length > 0)
            {
                string filePath = Path.Combine(path, file.FileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);
                fileStream.Close();
            }

            return true;
        }
    }
}
