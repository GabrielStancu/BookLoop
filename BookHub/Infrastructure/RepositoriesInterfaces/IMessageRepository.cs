using Core.Models;
using Infrastructure.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IMessageRepository: IGenericRepository<Message>
    {
        Task<IReadOnlyList<Message>> GetLastMessagesForUserAsync(IReadOnlyList<Conversation> conversations);
        Task<IReadOnlyList<Message>> GetMessagesFromConversationAsync(ConversationDTO conversation);
    }
}