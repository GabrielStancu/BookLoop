using Business.Services;
using Business.Shared;
using Data.Helpers;
using Data.Models;
using System.Threading.Tasks;

namespace Business.ViewModels
{
    public class PostDetailsViewModel: BaseViewModel
    {
        private BookPost _bookPost;
        public BookPost BookPost
        {
            get { return _bookPost; }
            set 
            { 
                _bookPost = value;
                SetProperty<BookPost>(ref _bookPost, value);
            }
        }

        private readonly IConversationService _conversationService;
        private readonly IMessageService _messageService;

        public PostDetailsViewModel(IConversationService conversationService, IMessageService messageService)
        {
            _conversationService = conversationService;
            _messageService = messageService;
            BookPost = DataContainer.BookPost;
        }

        public async Task StartConversationAsync()
        {
            var userPairDTO = new UserPairIdUsernameDTO()
            {
                UserId = DataContainer.GetUserId(),
                Username = BookPost.PostOwner.Username
            };
            var conversationDTO = await _conversationService.GetConversationWithUser(userPairDTO);

            DataContainer.ConversationDTO = conversationDTO;
            await _messageService.ConnectToHub();
            await _messageService.JoinChat(conversationDTO);
        }

        public async Task DisconnectFromHub()
        {
            if (_messageService.IsHubInitalized())
            {
                await _messageService.DisconnectFromHub();
            }
        }
    }
}
