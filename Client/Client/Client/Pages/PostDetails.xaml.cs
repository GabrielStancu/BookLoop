using Acr.UserDialogs;
using Business.Helpers;
using Business.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostDetails : ContentPage
    {
        private readonly PostDetailsViewModel _model;
        private readonly IInternetConnectionChecker _internetConnectionChecker;

        public PostDetails(PostDetailsViewModel model, IInternetConnectionChecker internetConnectionChecker)
        {
            InitializeComponent();
            BindingContext = model;
            _model = model;
            _internetConnectionChecker = internetConnectionChecker;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _model.DisconnectFromHub();
        }

        private async void OnChatClicked(object sender, EventArgs e)
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            using (UserDialogs.Instance.Loading("Loading conversation..."))
            {
                await _model.StartConversationAsync();
                await Navigation.PushAsync(App.Container.Resolve<ConversationPage>());
            }
        }
    }
}