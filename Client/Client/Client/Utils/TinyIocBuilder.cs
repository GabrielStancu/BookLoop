using Business.Helpers;
using Business.Services;
using Business.ViewModels;
using Client.Pages;
using Nancy.TinyIoc;
using System;

namespace Client.Utils
{
    public static class TinyIocBuilder
    {
        public static void RegisterClient(this TinyIoCContainer container)
        {
            RegisterPages(container);
            RegisterViewModels(container);
            RegisterServices(container);
            RegisterHelpers(container);
        }

        private static void RegisterPages(TinyIoCContainer container)
        {
            container.Register<AddPostPage>();
            container.Register<ConversationPage>();
            container.Register<CreateGroupPage>();
            container.Register<FilteringPage>();
            container.Register<LoginPage>();
            container.Register<PostDetails>();
            container.Register<PostsPage>();
            container.Register<SettingsPage>();
            container.Register<SignupPage>();
            container.Register<UserConversationsPage>();
        }

        private static void RegisterViewModels(TinyIoCContainer container)
        {
            container.Register<AddPostViewModel>();
            container.Register<ConversationViewModel>();
            container.Register<CreateGroupViewModel>();
            container.Register<FilterPostsViewModel>();
            container.Register<LoginViewModel>();
            container.Register<PostDetailsViewModel>();
            container.Register<PostsViewModel>();
            container.Register<SettingsViewModel>();
            container.Register<SignupViewModel>();
            container.Register<UserConversationsViewModel>();
        }

        private static void RegisterServices(TinyIoCContainer container)
        {
            container.Register<IConversationService, ConversationService>();
            container.Register<ILoginService, LoginService>();
            container.Register<IMessageService, MessageService>();
            container.Register<IPickPhotoService, PickPhotoService>();
            container.Register<IPostsService, PostsService>();
            container.Register<ISignupService, SignupService>();
            container.Register<IUploadPhotoService, UploadPhotoService>();
            container.Register<IUserService, UserService>();
        }

        private static void RegisterHelpers(TinyIoCContainer container)
        {
            container.Register<IInternetConnectionChecker, InternetConnectionChecker>();
            container.Register(typeof(IOptionsLoader<>), typeof(OptionsLoader<>)).AsMultiInstance();
            container.Register<IUserCredentialsSaver, UserCredentialsSaver>();
            container.Register<IUserCredetialsRetriever, UserCredetialsRetriever>();
        }
    }
}
