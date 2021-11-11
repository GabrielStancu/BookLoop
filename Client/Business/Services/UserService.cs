using Data.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserService : IUserService
    {
        public UserService(IUploadPhotoService uploadPhotoService)
        {
            _uploadPhotoService = uploadPhotoService;
        }
        private readonly string _apiURL = $"{Properties.Resources.ServerIp}/user";
        private readonly IUploadPhotoService _uploadPhotoService;

        public async Task<BookUser> SelectUserWithInfoByIdAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                string result = await httpClient.GetStringAsync($"{_apiURL}/Id/{id}");
                return JsonConvert.DeserializeObject<BookUser>(result);
            }
        }

        public async Task<BookUser> SelectUserWithInfoByUsernameAsync(string username)
        {
            using (var httpClient = new HttpClient())
            {
                string result = await httpClient.GetStringAsync($"{_apiURL}/Username/{username}");
                return JsonConvert.DeserializeObject<BookUser>(result);
            }
        }

        public async Task<bool> UpdateProfileChangesAsync(BookUser bookUser)
        {
            var parsedBookUser = ParseUser(bookUser);

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsync(_apiURL,
                    new StringContent(
                        JsonConvert.SerializeObject(parsedBookUser),
                        Encoding.UTF8, "application/json"
                        ));
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<bool>(jsonResponse);
            }
        }

        public async Task<bool> UploadPhotoAsync(Stream image, string fileName)
        {
            return await _uploadPhotoService.UploadPhotoAsync(image, fileName, _apiURL);
        }

        private BookUser ParseUser(BookUser bookUser)
        {
            var newBookUser = new BookUser()
            {
                Id = bookUser.Id,
                Username = bookUser.Username,
                Email = bookUser.Email,
                FirstName = bookUser.FirstName,
                LastName = bookUser.LastName,
                PhotoFileName = bookUser.PhotoFileName.Substring(bookUser.PhotoFileName.LastIndexOf('/') + 1),
                BookPosts = bookUser.BookPosts
            };

            newBookUser.BookPosts.ForEach(bp => bp.PostOwner = null);

            return newBookUser;
        }
    }
}
