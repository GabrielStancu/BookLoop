using Core.Models;
using Infrastructure.Contexts;
using Infrastructure.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(BookContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Message>> GetLastMessagesForUserAsync(IReadOnlyList<Conversation> conversations)
        {
            var messages = new List<Message>();

            foreach (var conversation in conversations)
            {
                var conversationMessages = await Context.Message
                                .Include(m => m.Sender)
                                .Where(m => m.ConversationId == conversation.Id && m.ConversationType == conversation.ConversationType)
                                .ToListAsync();

                if (conversationMessages.Count > 0)
                {
                    messages.Add(conversationMessages.Last());
                }
                
            }

            return messages;
        }

        public async Task<IReadOnlyList<Message>> GetMessagesFromConversationAsync(ConversationDTO conversation)
        {
            return await Context.Message
                .Include(m => m.Sender)
                .Where(m => m.ConversationId == conversation.Id && m.ConversationType == conversation.ConversationType)
                .ToListAsync();
        }
    }
}
