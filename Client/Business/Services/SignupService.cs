using Data.Helpers;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class SignupService : ISignupService
    {
        public SignupService(IUploadPhotoService uploadPhotoService)
        {
            _uploadPhotoService = uploadPhotoService;
        }

        private readonly string _apiURL = $"{Properties.Resources.ServerIp}/signup";
        private readonly IUploadPhotoService _uploadPhotoService;

        public async Task<SignupResult> SignupAsync(SignupUserDTO signupUser)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(_apiURL,
                    new StringContent(
                        JsonConvert.SerializeObject(signupUser),
                        Encoding.UTF8, "application/json"
                        ));
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<SignupResult>(jsonResponse);
            }
        }

        public async Task<bool> UploadPhotoAsync(Stream image, string fileName)
        {
            return await _uploadPhotoService.UploadPhotoAsync(image, fileName, _apiURL);
        }
    }
}
