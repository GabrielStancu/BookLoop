using Data.Helpers;
using Data.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class MessageService : IMessageService
    {
        private readonly string _apiURL = $"{Properties.Resources.ServerIp}/messages";
        private readonly string _hubUrl = Properties.Resources.HubIp;
        private HubConnection _hubConnection;

        public async Task<IEnumerable<Message>> GetConversationMessagesAsync(ConversationDTO conversationDTO)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(_apiURL,
                    new StringContent(
                        JsonConvert.SerializeObject(conversationDTO),
                        Encoding.UTF8, "application/json"
                        ));
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<Message>>(jsonResponse);
            }
        }

        public async Task ConnectToHub()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_hubUrl)
                .Build();

            _hubConnection.On<MessageDTO>("ReceiveMessage", message =>
            {
                OnReceivedMessage?.Invoke(message, new EventArgs());
            });

            await _hubConnection.StartAsync();
        }

        public async Task JoinChat(ConversationDTO conversationDTO)
        {
            await _hubConnection.InvokeAsync("JoinChat", $"{conversationDTO.ConversationType}/{conversationDTO.Id}");
        }

        public async Task DisconnectFromHub()
        {
            await _hubConnection.StopAsync();
        }

        public bool IsHubInitalized()
        {
            return _hubConnection != null;
        }

        public async Task SendMessageAsync(MessageDTO message, ConversationDTO conversationDTO)
        {
            await _hubConnection.InvokeAsync("SendMessage", message, $"{conversationDTO.ConversationType}/{conversationDTO.Id}");
        }

        public event EventHandler OnReceivedMessage;
    }
}
