using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UploadPhotoService : IUploadPhotoService
    {
        public async Task<bool> UploadPhotoAsync(Stream image, string fileName, string apiURL)
        {
            HttpContent fileStreamContent = new StreamContent(image);
            fileStreamContent.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "file", FileName = fileName };
            fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            using (var client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(fileStreamContent);
                    var response = await client.PostAsync($"{apiURL}/UploadPhoto", formData);
                    return response.IsSuccessStatusCode;
                }
            }
        }
    }
}
