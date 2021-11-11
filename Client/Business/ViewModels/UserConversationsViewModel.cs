using Business.Services;
using Business.Shared;
using Data.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Business.ViewModels
{
    public class UserConversationsViewModel: BaseViewModel
    {
        public ObservableCollection<ConversationDTO> UserConversations { get; set; } 
            = new ObservableCollection<ConversationDTO>();

        private ConversationDTO _selectedConversation;
        public ConversationDTO SelectedConversation
        {
            get { return _selectedConversation; }
            set
            {
                _selectedConversation = value;
                SetProperty<ConversationDTO>(ref _selectedConversation, value);
                DataContainer.ConversationDTO = value;
            }
        }

        private bool _visibleNewConversation = false;
        public bool VisibleNewConversation
        {
            get { return _visibleNewConversation; }
            set
            {
                _visibleNewConversation = value;
                SetProperty<bool>(ref _visibleNewConversation, value);
            }
        }

        private string _username = string.Empty;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                SetProperty<string>(ref _username, value);
            }
        }

        private readonly IConversationService _conversationService;
        private readonly IMessageService _messageService;

        public UserConversationsViewModel(IConversationService conversationService, IMessageService messageService)
        {
            _conversationService = conversationService;
            _messageService = messageService;
            _messageService.OnReceivedMessage += MessageService_OnReceivedMessage;

            ConnectToHub();
        }

        private async void ConnectToHub()
        {
            await _messageService.ConnectToHub();
        }

        private void MessageService_OnReceivedMessage(object sender, System.EventArgs e)
        {
            var messageDTO = (MessageDTO)sender;
            var conversation = UserConversations.FirstOrDefault(uc => uc.Id == messageDTO.ConversationId && uc.ConversationType == messageDTO.ConversationType);
            conversation.ConversationLastMessage = messageDTO.MessageContent;
        }

        public async Task GetUserConversations()
        {
            var conversations = await _conversationService.GetConversationsForUserAsync(DataContainer.GetUserId());

            UserConversations.Clear();
            foreach (var conversation in conversations)
            {
                UserConversations.Add(conversation);
            }

            foreach (var conversation in conversations)
            {

                await _messageService.JoinChat(conversation);
            }
        }

        public async Task<ConversationDTO> GetConversationWithUser(string username)
        {
            var userPair = new UserPairIdUsernameDTO()
            {
                UserId = DataContainer.GetUserId(),
                Username = username
            };
            var conversation = await _conversationService.GetConversationWithUser(userPair);

            if(conversation == null)
            {
                return conversation;
            }

            DataContainer.ConversationDTO = conversation;
            if (conversation.ConversationLastMessage is null)
            {
                UserConversations.Add(conversation);
                await _messageService.JoinChat(conversation);
            }

            return conversation;
        }

        public async Task RegisterNewGroupConversation()
        {
            UserConversations.Add(DataContainer.ConversationDTO);
            await _messageService.JoinChat(DataContainer.ConversationDTO);
        }

        public void RemoveEmtpyConversations()
        {
            var conversations = new List<ConversationDTO>(UserConversations);
            foreach (var conversation in conversations)
            {
                if (conversation.ConversationLastMessage is null)
                {
                    UserConversations.Remove(conversation);
                }
            }
        }

        public async Task DisconnectFromHub()
        {
            await _messageService.DisconnectFromHub();
        }
    }
}
