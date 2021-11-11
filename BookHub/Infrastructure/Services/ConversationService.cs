using Core.Models;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IConversationParser _conversationParser;
        private readonly IConversationComparer _conversationComparer;

        public ConversationService(IConversationRepository conversationRepository, IMessageRepository messageRepository, IConversationParser conversationParser,
            IConversationComparer conversationComparer)
        {
            _conversationRepository = conversationRepository;
            _messageRepository = messageRepository;
            _conversationParser = conversationParser;
            _conversationComparer = conversationComparer;
        }
        public async Task<ConversationDTO> GetConversationBetweenUsersAsync(UserPairDTO userPairDTO)
        {
            var privateConversation = await _conversationRepository.GetConversationBetweenUsers(userPairDTO.CurrentUserId, userPairDTO.OtherUserId);
            var conversationDTO = _conversationParser.ParseConversation(privateConversation, userPairDTO.CurrentUserId);

            return conversationDTO;
        }

        public async Task<ConversationDTO> GetConversationWithUserAsync(UserPairIdUsernameDTO userPair)
        {
            var privateConversation = await _conversationRepository.GetConversationWithUser(userPair.UserId, userPair.Username);
            var conversationDTO = _conversationParser.ParseConversation(privateConversation, userPair.UserId);

            return conversationDTO;
        }

        public async Task<IReadOnlyList<ConversationDTO>> GetConversationsForUserAsync(int id)
        {
            var conversations = await _conversationRepository.GetAllConversationsForUserAsync(id);
            var messages = await _messageRepository.GetLastMessagesForUserAsync(conversations);
            var conversationDTOs = new List<ConversationDTO>();

            for (int i = 0; i < messages.Count; i++)
            {
                ConversationDTO conversation;

                if (conversations[i] is PrivateConversation)
                {
                    conversation = _conversationParser.ParseConversation((PrivateConversation)conversations[i], messages[i], id);
                }
                else
                {
                    conversation = _conversationParser.ParseConversation((GroupConversation)conversations[i], messages[i], id);
                }

                conversationDTOs.Add(conversation);
            }

            conversationDTOs.Sort(_conversationComparer);

            return conversationDTOs;
        }

        public async Task<ConversationDTO> CreateGroupConversationDTO(GroupConversationDTO conversationDTO)
        {
            var groupConversation = await _conversationRepository.CreateGroupConversation(conversationDTO);
            var groupConversationDTO = _conversationParser.ParseConversation(groupConversation);

            return groupConversationDTO;
        }

        public async Task<bool> DeleteConversationAsync(ConversationDTO conversationDTO)
        {
            var conversation = _conversationParser.ParseConversation(conversationDTO);

            if(conversation is PrivateConversation)
            {
                return await _conversationRepository.DeleteConversationAsync((PrivateConversation)conversation);
            }
            else
            {
                return await _conversationRepository.DeleteConversationAsync((GroupConversation)conversation);
            }
        }
    }
}
