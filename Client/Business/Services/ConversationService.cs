using Data.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ConversationService : IConversationService
    {
        public static readonly string _apiURL = $"{Properties.Resources.ServerIp}/conversations";

        public async Task<IEnumerable<ConversationDTO>> GetConversationsForUserAsync(int id)
        {
            var client = new HttpClient();
            string result = await client.GetStringAsync($"{_apiURL}/{id}");

            return JsonConvert.DeserializeObject<IEnumerable<ConversationDTO>>(result);
        }

        public async Task<ConversationDTO> GetConversationBetweenUsers(UserPairDTO userPairDTO)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsync(_apiURL,
                    new StringContent(
                        JsonConvert.SerializeObject(userPairDTO),
                        Encoding.UTF8, "application/json"
                        ));
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ConversationDTO>(jsonResponse);
            }
        }

        public async Task<ConversationDTO> GetConversationWithUser(UserPairIdUsernameDTO userPairDTO)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(_apiURL,
                    new StringContent(
                        JsonConvert.SerializeObject(userPairDTO),
                        Encoding.UTF8, "application/json"
                        ));
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ConversationDTO>(jsonResponse);
            }
        }

        public async Task<ConversationDTO> CreateGroupConversation(GroupConversationDTO conversationDTO)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync($"{_apiURL}/Group",
                    new StringContent(
                        JsonConvert.SerializeObject(conversationDTO),
                        Encoding.UTF8, "application/json"
                        ));
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ConversationDTO>(jsonResponse);
            }
        }

        public async Task<bool> DeleteConversationAsync(ConversationDTO conversationDTO)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsync($"{_apiURL}/Delete",
                    new StringContent(
                        JsonConvert.SerializeObject(conversationDTO),
                        Encoding.UTF8, "application/json"
                        ));
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<bool>(jsonResponse);
            }
        }
    }
}
