using AutoMapper;
using Core.Models;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IPhotosUrlResolver _photosUrlResolver;
        public MessageService(IMessageRepository messageRepository, IMapper mapper, IPhotosUrlResolver photosUrlResolver)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _photosUrlResolver = photosUrlResolver;
        }
        public async Task StoreMessageAsync(MessageDTO message)
        {
            var mappedMessage = _mapper.Map<MessageDTO, Message>(message);
            mappedMessage.Sender = null;
            await _messageRepository.InsertAsync(mappedMessage);
        }

        public async Task<IReadOnlyList<MessageDTO>> GetMessagesFromConversationAsync(ConversationDTO conversation)
        {
            var messages = await _messageRepository.GetMessagesFromConversationAsync(conversation);
            var messagesDTO = new List<MessageDTO>();
            foreach (var message in messages)
            {
                _photosUrlResolver.ResolveUrl(message.Sender);
                messagesDTO.Add(_mapper.Map<Message, MessageDTO>(message));
            }

            return messagesDTO;
        }
    }
}
