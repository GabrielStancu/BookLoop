using AutoMapper;
using Core.Models;

namespace Infrastructure.DTOs
{
    public class DtoModule: Profile
    {
        public DtoModule()
        {
            CreateMap<SignupUserDTO, BookUser>();
            CreateMap<MessageDTO, Message>().ReverseMap();
            CreateMap<BookUser, BookUserDTO>().ReverseMap();
            CreateMap<BookPost, BookPostDTO>();
            CreateMap<Conversation, ConversationDTO>();
        }
    }
}
