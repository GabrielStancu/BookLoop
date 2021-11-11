using Infrastructure.DTOs;
using Infrastructure.Helpers;
using Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace API.Hubs
{
    public class BookHub: Hub
    {
        private readonly IMessageService _messageService;
        private readonly IPhotosUrlResolver _photosUrlResolver;

        public BookHub(IMessageService messageService, IPhotosUrlResolver photosUrlResolver)
        {
            _messageService = messageService;
            _photosUrlResolver = photosUrlResolver;
        }
        public async Task SendMessage(MessageDTO message, string chatName)
        {
            _photosUrlResolver.ResolveUrl(message.Sender);
            await Clients.Group(chatName).SendAsync("ReceiveMessage", message);
            await _messageService.StoreMessageAsync(message);
        }

        public async Task JoinChat(string chatName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatName);
        }

        public async Task LeaveChat(string chatName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatName);
        }
    }
}
