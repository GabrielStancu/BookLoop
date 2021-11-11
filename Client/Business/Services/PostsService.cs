using Data.Helpers;
using Data.Models;
using Data.Specification;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class PostsService : IPostsService
    {
        public PostsService(IUploadPhotoService uploadPhotoService)
        {
            _uploadPhotoService = uploadPhotoService;
        }
        private readonly string _apiURL = $"{Properties.Resources.ServerIp}/posts";
        private readonly IUploadPhotoService _uploadPhotoService;
        public async Task<bool> MakePostAsync(BookPost bookPost)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(_apiURL,
                    new StringContent(
                        JsonConvert.SerializeObject(bookPost),
                        Encoding.UTF8, "application/json"
                        ));
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<bool>(jsonResponse);
            }
        }

        public async Task<BooksSearchResultDTO> GetBooksWithSpecificationAsync(Specification specification)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsync(_apiURL,
                    new StringContent(
                        JsonConvert.SerializeObject(specification),
                        Encoding.UTF8, "application/json"
                        ));
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<BooksSearchResultDTO>(jsonResponse);
            }
        }

        public async Task<bool> UploadPhotoAsync(Stream image, string fileName)
        {
            return await _uploadPhotoService.UploadPhotoAsync(image, fileName, _apiURL);
        }

        public async Task<bool> DeletePostAsync(BookPost bookPost)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"{_apiURL}/{bookPost.Id}");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<bool>(jsonResponse);
            }
        }
    }
}
