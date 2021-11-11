using Data.Helpers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class LoginService : ILoginService
    {
        private readonly string _apiURL = $"{Properties.Resources.ServerIp}/login";

        public async Task<LoginResult> LoginAsync(LoginUserDTO loginUser)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(_apiURL,
                    new StringContent(
                        JsonConvert.SerializeObject(loginUser),
                        Encoding.UTF8, "application/json"
                        ));
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<LoginResult>(jsonResponse);
            }
        }
    }
}
