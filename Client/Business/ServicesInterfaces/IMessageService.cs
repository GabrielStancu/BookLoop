using Data.Helpers;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IMessageService
    {
        event EventHandler OnReceivedMessage;

        Task ConnectToHub();
        Task DisconnectFromHub();
        Task<IEnumerable<Message>> GetConversationMessagesAsync(ConversationDTO conversationDTO);
        Task JoinChat(ConversationDTO conversationDTO);
        Task SendMessageAsync(MessageDTO message, ConversationDTO conversationDTO);
        bool IsHubInitalized();
    }
}