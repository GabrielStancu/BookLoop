using Infrastructure.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        
        [HttpPost]
        public async Task<ActionResult<IReadOnlyList<MessageDTO>>> GetConversationMessages(ConversationDTO conversation)
        {
            var messages = await _messageService.GetMessagesFromConversationAsync(conversation);
            return Ok(messages);
        }
    }
}
