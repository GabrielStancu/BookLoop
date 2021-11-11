using Business.Services;
using Business.Shared;
using Data.Helpers;
using Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Business.ViewModels
{
    public class CreateGroupViewModel: BaseViewModel
    {
        public ObservableCollection<BookUser> GroupMembers { get; set; }
            = new ObservableCollection<BookUser>();

        private string _groupName;
        public string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                SetProperty<string>(ref _groupName, value);
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                SetProperty<string>(ref _username, value);
            }
        }

        private bool _enabledCreateGroup;
        public bool EnabledCreateGroup
        {
            get { return _enabledCreateGroup; }
            set
            {
                _enabledCreateGroup = value;
                SetProperty<bool>(ref _enabledCreateGroup, value);
            }
        }

        private readonly IUserService _userService;
        private readonly IConversationService _conversationService;

        public CreateGroupViewModel(IUserService userService, IConversationService conversationService)
        {
            _userService = userService;
            _conversationService = conversationService;
            GroupMembers.Add(DataContainer.User);
        }

        public async Task<bool> AddUser()
        {
            var bookUser = await _userService.SelectUserWithInfoByUsernameAsync(Username);

            if(bookUser is null)
            {
                return false;
            }

            if(!GroupMembers.Contains(bookUser))
            {
                GroupMembers.Add(bookUser);
                if (GroupMembers.Count > 2 && !string.IsNullOrEmpty(GroupName))
                {
                    EnabledCreateGroup = true;
                }
                Username = string.Empty;

                return true;
            }

            return false;
        }

        public async Task CreateGroup()
        {
            var conversationDTO = new GroupConversationDTO()
            {
                GroupName = GroupName,
                ConversationType = ConversationType.Group,
                GroupMembers = new List<BookUser>(GroupMembers)
            };

            var createdConversationDTO = await _conversationService.CreateGroupConversation(conversationDTO);
            DataContainer.ConversationDTO = createdConversationDTO;
        }
    }
}
