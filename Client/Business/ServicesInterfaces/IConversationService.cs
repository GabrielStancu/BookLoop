using Data.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IConversationService
    {
        Task<ConversationDTO> CreateGroupConversation(GroupConversationDTO conversationDTO);
        Task<bool> DeleteConversationAsync(ConversationDTO conversationDTO);
        Task<ConversationDTO> GetConversationBetweenUsers(UserPairDTO userPairDTO);
        Task<IEnumerable<ConversationDTO>> GetConversationsForUserAsync(int id);
        Task<ConversationDTO> GetConversationWithUser(UserPairIdUsernameDTO userPairDTO);
    }
}