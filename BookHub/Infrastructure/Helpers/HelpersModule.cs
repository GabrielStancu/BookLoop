using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Helpers
{
    public static class ServicesModule
    {
        public static void AddHelpers(this IServiceCollection services)
        {
            services.AddScoped<IConversationParser, ConversationParser>();
            services.AddScoped<IConversationComparer, ConversationComparer>();
            services.AddScoped<IBookPostComparer, BookPostComparer>();
            services.AddScoped<IPhotosUrlResolver, PhotosUrlResolver>();
        }
    }
}
