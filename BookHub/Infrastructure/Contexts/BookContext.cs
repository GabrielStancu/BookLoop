using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Contexts
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }
        public DbSet<BookPost> BookPost {get; set;}
        public DbSet<BookUser> BookUser { get; set; }
        public DbSet<GroupConversation> GroupConversation { get; set; }
        public DbSet<GroupUser> GroupUser { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<PrivateConversation> PrivateConversation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
