using Core.Models;
using Infrastructure.DTOs;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Helpers
{
    public class PhotosUrlResolver : IPhotosUrlResolver
    {
        private readonly string _profilePhotosUrl;
        private readonly string _bookPostPhotosUrl;

        public PhotosUrlResolver(IHttpContextAccessor httpContextAccessor)
        {
            var protocol = httpContextAccessor.HttpContext.Request.Scheme;
            var remoteIp = httpContextAccessor.HttpContext.Request.Host.Value;

            _profilePhotosUrl = $"{protocol}://{remoteIp}/ProfilePhotos";
            _bookPostPhotosUrl = $"{protocol}://{remoteIp}/BookPosts";
        }
        public void ResolveUrl(BookPost bookPost)
        {
            if (!bookPost.PhotoFileName.StartsWith(_bookPostPhotosUrl))
            {
                bookPost.PhotoFileName = $"{_bookPostPhotosUrl}/{bookPost.PhotoFileName}";
            }
        }

        public void ResolveUrl(BookPostDTO bookPostDTO)
        {
            if (!bookPostDTO.PhotoFileName.StartsWith(_bookPostPhotosUrl))
            {
                bookPostDTO.PhotoFileName = $"{_bookPostPhotosUrl}/{bookPostDTO.PhotoFileName}";
            }
        }

        public void ResolveUrl(BookUser bookUser)
        {
            if (!bookUser.PhotoFileName.StartsWith(_profilePhotosUrl))
            {
                bookUser.PhotoFileName = $"{_profilePhotosUrl}/{bookUser.PhotoFileName}";
            }
        }

        public void ResolveUrl(BookUserDTO bookUserDTO)
        {
            if (!bookUserDTO.PhotoFileName.StartsWith(_profilePhotosUrl))
            {
                bookUserDTO.PhotoFileName = $"{_profilePhotosUrl}/{bookUserDTO.PhotoFileName}";
            }
        }

        public void ResolveUrl(ConversationDTO conversationDTO)
        {
            if (!conversationDTO.ConversationPhoto.StartsWith(_profilePhotosUrl))
            {
                conversationDTO.ConversationPhoto = $"{_profilePhotosUrl}/{conversationDTO.ConversationPhoto}";
            }
        }
    }
}
