using Infrastructure.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IMessageService
    {
        Task StoreMessageAsync(MessageDTO message);
        Task<IReadOnlyList<MessageDTO>> GetMessagesFromConversationAsync(ConversationDTO conversation);
    }
}