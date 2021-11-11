using Core.Models;
using Infrastructure.DTOs;

namespace Infrastructure.Helpers
{
    public interface IPhotosUrlResolver
    {
        void ResolveUrl(BookPost bookPost);
        void ResolveUrl(BookUser bookUser);
        void ResolveUrl(ConversationDTO conversationDTO);
        void ResolveUrl(BookUserDTO bookUserDTO);
        public void ResolveUrl(BookPostDTO bookPostDTO);
    }
}