using Core.Models;
using Infrastructure.DTOs;

namespace Infrastructure.Helpers
{
    public interface IConversationParser
    {
        ConversationDTO ParseConversation(GroupConversation conversation);
        ConversationDTO ParseConversation(GroupConversation conversation, Message message, int userId);
        ConversationDTO ParseConversation(PrivateConversation conversation, int crtUserId);
        ConversationDTO ParseConversation(PrivateConversation conversation, Message message, int userId);
        Conversation ParseConversation(ConversationDTO conversationDTO);
    }
}