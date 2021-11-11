using Infrastructure.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IConversationService
    {
        Task<ConversationDTO> GetConversationBetweenUsersAsync(UserPairDTO userPairDTO);
        Task<IReadOnlyList<ConversationDTO>> GetConversationsForUserAsync(int id);
        Task<ConversationDTO> GetConversationWithUserAsync(UserPairIdUsernameDTO userPair);
        Task<ConversationDTO> CreateGroupConversationDTO(GroupConversationDTO conversationDTO);
        Task<bool> DeleteConversationAsync(ConversationDTO conversationDTO);
    }
}