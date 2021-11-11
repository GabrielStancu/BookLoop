using Business.Services;
using Business.Shared;
using Data.Helpers;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Business.ViewModels
{
    public class ConversationViewModel: BaseViewModel
    {
        public ObservableCollection<MessageDTO> Messages { get; set; }
            = new ObservableCollection<MessageDTO>();

        private string _conversationName;
        public string ConversationName
        {
            get { return _conversationName; }
            set
            {
                _conversationName = value;
                SetProperty<string>(ref _conversationName, value);
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                SetProperty<string>(ref _message, value);
            }
        }

        public BookUser CurrentUser { get; set; }
        public ConversationDTO ConversationDTO { get; set; }

        private readonly IMessageService _messageService;
        private readonly IConversationService _conversationService;

        public ConversationViewModel(IMessageService messageService, IConversationService conversationService)
        {
            _messageService = messageService;
            _conversationService = conversationService;
            _messageService.OnReceivedMessage += MessageService_OnReceivedMessage;
            ConversationDTO = DataContainer.ConversationDTO;
            CurrentUser = DataContainer.User;

            MapMessages();
        }

        private void MessageService_OnReceivedMessage(object sender, EventArgs e)
        {
            var receivedMessage = (MessageDTO)sender;
            if(receivedMessage.ConversationId == ConversationDTO.Id && receivedMessage.ConversationType == ConversationDTO.ConversationType)
            {
                receivedMessage.CurrentUserId = CurrentUser.Id;
                Messages.Add(receivedMessage);
            }
        }

        private async void MapMessages()
        {
            var messages = await GetConversationMessages(ConversationDTO);
            foreach (var message in messages)
            {
                var messageDTO = new MessageDTO()
                {
                    ConversationId = message.ConversationId,
                    ConversationType = message.ConversationType,
                    MessageContent = message.MessageContent,
                    SendTime = message.SendTime,
                    Sender = message.Sender,
                    CurrentUserId = CurrentUser.Id
                };

                Messages.Add(messageDTO);
            }

            ConversationName = ConversationDTO.ConversationName;
        }

        public async Task SendMessage()
        {
            if (!string.IsNullOrEmpty(Message))
            {
                var message = new MessageDTO()
                {
                    ConversationId = ConversationDTO.Id,
                    ConversationType = ConversationDTO.ConversationType,
                    CurrentUserId = CurrentUser.Id,
                    MessageContent = Message,
                    Sender = CurrentUser,
                    SendTime = DateTime.Now
                };

                await _messageService.SendMessageAsync(message, ConversationDTO);
                Message = string.Empty;
            }
        }

        public async Task<bool> DeleteConversationIfEmpty()
        {
            if(Messages.Count == 0)
            {
                return await _conversationService.DeleteConversationAsync(ConversationDTO);
            }

            return false;
        }

        private async Task<IEnumerable<Message>> GetConversationMessages(ConversationDTO conversation)
        {
            var messages = await _messageService.GetConversationMessagesAsync(conversation);
            return messages;
        }
    }
}
