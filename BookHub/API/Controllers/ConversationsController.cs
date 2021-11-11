using Infrastructure.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationsController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<ConversationDTO>>> GetConversationsForUser(int id)
        {
            var conversations = await _conversationService.GetConversationsForUserAsync(id);
            return Ok(conversations);
        }

        [HttpPut]
        public async Task<ActionResult<ConversationDTO>> GetConversationBetweenUsers(UserPairDTO userPairDTO)
        {
            var conversation = await _conversationService.GetConversationBetweenUsersAsync(userPairDTO);
            return Ok(conversation);
        }

        [HttpPost]
        public async Task<ActionResult<ConversationDTO>> GetConversationWithUser(UserPairIdUsernameDTO userPairDTO)
        {
            var conversation = await _conversationService.GetConversationWithUserAsync(userPairDTO);
            return Ok(conversation);
        }

        [HttpPost("Group")]
        public async Task<ActionResult<ConversationDTO>> CreateGroupConversation(GroupConversationDTO conversationDTO)
        {
            var conversation = await _conversationService.CreateGroupConversationDTO(conversationDTO);
            return Ok(conversation);
        }

        [HttpPut("Delete")]
        public async Task<ActionResult<bool>> DeleteConversation(ConversationDTO conversationDTO)
        {
            bool deleted = await _conversationService.DeleteConversationAsync(conversationDTO);
            return Ok(deleted);
        }
    }
}
