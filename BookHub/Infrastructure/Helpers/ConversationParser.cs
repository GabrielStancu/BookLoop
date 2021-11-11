using Core.Models;
using Infrastructure.DTOs;

namespace Infrastructure.Helpers
{
    public class ConversationParser : IConversationParser
    {
        private readonly IPhotosUrlResolver _photosUrlResolver;
        private readonly string _defaultGroupPhotoName = "GroupPhoto.png";
        public ConversationParser(IPhotosUrlResolver photosUrlResolver)
        {
            _photosUrlResolver = photosUrlResolver;
        }
        public ConversationDTO ParseConversation(PrivateConversation conversation, Message message, int userId)
        {
            var otherUser = (userId == conversation.User1Id) ? conversation.User2 : conversation.User1;
            var conversationDTO = new ConversationDTO()
            {
                Id = conversation.Id,
                ConversationType = conversation.ConversationType,
                ConversationPhoto = otherUser.PhotoFileName,
                ConversationLastMessage = (message.SenderId == userId) ? $"You: {message.MessageContent}" : message.MessageContent,
                ConversationName = otherUser.Username,
                LastMessageSendTime = message.SendTime
            };
            _photosUrlResolver.ResolveUrl(conversationDTO);

            return conversationDTO;
        }

        public ConversationDTO ParseConversation(GroupConversation conversation, Message message, int userId)
        {
            var conversationDTO = new ConversationDTO()
            {
                Id = conversation.Id,
                ConversationType = conversation.ConversationType,
                ConversationPhoto = _defaultGroupPhotoName,
                ConversationLastMessage = (message.SenderId == userId) ? $"You: {message.MessageContent}" : message.MessageContent,
                ConversationName = conversation.Name,
                LastMessageSendTime = message.SendTime
            };
            _photosUrlResolver.ResolveUrl(conversationDTO);

            return conversationDTO;
        }

        public ConversationDTO ParseConversation(GroupConversation conversation)
        {
            var conversationDTO = new ConversationDTO()
            {
                Id = conversation.Id,
                ConversationType = conversation.ConversationType,
                ConversationPhoto = _defaultGroupPhotoName,
                ConversationName = conversation.Name,
            };
            _photosUrlResolver.ResolveUrl(conversationDTO);

            return conversationDTO;
        }

        public ConversationDTO ParseConversation(PrivateConversation conversation, int crtUserId)
        {
            if (conversation is null)
            {
                return null;
            }

            var otherUser = (crtUserId == conversation.User1Id) ? conversation.User2 : conversation.User1;
            var conversationDTO = new ConversationDTO()
            {
                Id = conversation.Id,
                ConversationType = conversation.ConversationType,
                ConversationName = otherUser.Username
            };

            return conversationDTO;
        }

        public Conversation ParseConversation(ConversationDTO conversationDTO)
        {
            Conversation pc;
            if (conversationDTO.ConversationType == ConversationType.Private)
            {
                pc = new PrivateConversation()
                {
                    Id = conversationDTO.Id
                };
            }
            else
            {
                pc = new GroupConversation()
                {
                    Id = conversationDTO.Id
                };
            }

            return pc;
        }
    }
}
