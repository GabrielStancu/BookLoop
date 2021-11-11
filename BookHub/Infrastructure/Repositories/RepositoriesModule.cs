using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repositories
{
    public static class RepositoriesModule
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IBookPostRepository, BookPostRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
        }
    }
}
