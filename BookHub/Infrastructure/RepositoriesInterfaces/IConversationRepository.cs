using Core.Models;
using Infrastructure.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IConversationRepository: IGenericRepository<Conversation>
    {
        Task<IReadOnlyList<Conversation>> GetAllConversationsForUserAsync(int id);
        Task<PrivateConversation> GetConversationBetweenUsers(int userId1, int userId2);
        Task<PrivateConversation> GetConversationWithUser(int userId, string otherUser);
        Task<GroupConversation> CreateGroupConversation(GroupConversationDTO groupConversationDTO);
        Task<bool> DeleteConversationAsync(PrivateConversation conversationDTO);
        Task<bool> DeleteConversationAsync(GroupConversation groupConversation);
    }
}