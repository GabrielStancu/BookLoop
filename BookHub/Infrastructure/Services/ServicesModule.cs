using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services
{
    public static class ServicesModule
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ISignupService, SignupService>();
            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IConversationService, ConversationService>();
        }
    }
}
