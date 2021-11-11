using Core.Models;
using Infrastructure.Contexts;
using Infrastructure.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ConversationRepository : GenericRepository<Conversation>, IConversationRepository
    {
        public ConversationRepository(BookContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Conversation>> GetAllConversationsForUserAsync(int id)
        {
            var conversations = new List<Conversation>();

            conversations.AddRange(await Context.PrivateConversation
                .Where(pc => pc.User1Id == id || pc.User2Id == id)
                .Include(pc => pc.User1)
                .Include(pc => pc.User2)
                .ToListAsync());

            var groupUsers = await Context.GroupUser
                .Where(gu => gu.UserId == id)
                .ToListAsync();

            foreach (var groupUser in groupUsers)
            {
                var groupConversation = await Context.GroupConversation
                                            .FirstOrDefaultAsync(gc => gc.Id == groupUser.GroupId);

                conversations.Add(groupConversation);
            }

            return conversations;
        }

        public async Task<PrivateConversation> GetConversationBetweenUsers(int userId1, int userId2)
        {
            return await Context.PrivateConversation
                .Include(pc => pc.User1)
                .Include(pc => pc.User2)
                .FirstOrDefaultAsync(pc => (pc.User1Id == userId1 && pc.User2Id == userId2) || (pc.User1Id == userId2 && pc.User2Id == userId1));
        }

        public async Task<PrivateConversation> GetConversationWithUser(int userId, string otherUser)
        {
            var user1 = await Context.BookUser
                .FirstOrDefaultAsync(bu => bu.Id == userId);
            var user2 = await Context.BookUser
                .FirstOrDefaultAsync(bu => bu.Username == otherUser);

            if (user1 is null || user2 is null)
            {
                return null;
            }

            var conversation = await Context.PrivateConversation
                                    .Include(pc => pc.User1)
                                    .Include(pc => pc.User2)
                                    .FirstOrDefaultAsync(pc => ((pc.User1Id == userId && pc.User2.Username == otherUser) ||
                                                               (pc.User2Id == userId && pc.User1.Username == otherUser)) &&
                                                               pc.User1Id != pc.User2Id);

            if (conversation != null)
            {
                return conversation;
            }

            conversation = new PrivateConversation()
            {
                ConversationType = ConversationType.Private,
                User1Id = user1.Id,
                User2Id = user2.Id
            };

            await Context.PrivateConversation
                .AddAsync(conversation);
            await Context.SaveChangesAsync();

            return conversation;
        }

        public async Task<GroupConversation> CreateGroupConversation(GroupConversationDTO groupConversationDTO)
        {
            var groupConversation = new GroupConversation()
            {
                Name = groupConversationDTO.GroupName,
                ConversationType = groupConversationDTO.ConversationType
            };

            await Context.GroupConversation
                .AddAsync(groupConversation);
            await Context.SaveChangesAsync();

            groupConversation = await Context.GroupConversation
                .FirstOrDefaultAsync(gc => gc.Name == groupConversationDTO.GroupName);

            foreach (var groupMember in groupConversationDTO.GroupMembers)
            {
                var groupUser = new GroupUser()
                {
                    GroupId = groupConversation.Id,
                    UserId = groupMember.Id
                };
                await Context.GroupUser
                    .AddAsync(groupUser);
            }
            await Context.SaveChangesAsync();

            return groupConversation;
        }

        public async Task<bool> DeleteConversationAsync(PrivateConversation conversation)
        {                
            Context.Remove(conversation);
            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteConversationAsync(GroupConversation groupConversation)
        {
            var groupUsers = await Context.GroupUser
                                    .Where(gu => gu.GroupId == groupConversation.Id)
                                    .ToListAsync();
            Context.RemoveRange(groupUsers);
            Context.Remove(groupConversation);
            await Context.SaveChangesAsync();

            return true;
        }
    }
}
