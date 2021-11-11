using Acr.UserDialogs;
using Business.Helpers;
using Business.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserConversationsPage : ContentPage
    {
        private readonly UserConversationsViewModel _model;
        private readonly IInternetConnectionChecker _internetConnectionChecker;
        private bool _popped = true;

        public UserConversationsPage(UserConversationsViewModel model, IInternetConnectionChecker internetConnectionChecker)
        {
            InitializeComponent();
            _model = model;
            _internetConnectionChecker = internetConnectionChecker;
            BindingContext = _model;
            AddTapGestureRecognizers();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            _popped = true;
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            await _model.GetUserConversations();
            _model.RemoveEmtpyConversations();
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            if(_popped)
            {
                await _model.DisconnectFromHub();
            }
            
        }

        private void AddTapGestureRecognizers()
        {
            var tgrPrivateChat = new TapGestureRecognizer();
            tgrPrivateChat.Tapped += (s, e) => OnNewConversationTapped();
            NewPrivateChat.GestureRecognizers.Add(tgrPrivateChat);

            var tgrGroupChat = new TapGestureRecognizer();
            tgrGroupChat.Tapped += (s, e) => OnNewGroupTapped();
            NewGroupChat.GestureRecognizers.Add(tgrGroupChat);
        }

        private void OnNewConversationTapped()
        {
            _model.VisibleNewConversation = true;
        }

        private async void OnNewGroupTapped()
        {
            _popped = false;
            var createGroupPage = App.Container.Resolve<CreateGroupPage>();
            createGroupPage.Disappearing += CreateGroupPage_Disappearing;
            await Navigation.PushAsync(createGroupPage);
        }

        private async void CreateGroupPage_Disappearing(object sender, System.EventArgs e)
        {
            await _model.RegisterNewGroupConversation(); 
        }

        private async void OnConversationTapped(object sender, SelectedItemChangedEventArgs e)
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;

            }

            _popped = false;
            using (UserDialogs.Instance.Loading("Loading conversation messages..."))
            {
                await Navigation.PushAsync(App.Container.Resolve<ConversationPage>());
            }
        }

        private void OnOkClicked(object sender, EventArgs e)
        {
            _model.VisibleNewConversation = false;
            GetConversationWithUser();
        }

        private async void GetConversationWithUser()
        {
            string result = _model.Username;

            if (!string.IsNullOrEmpty(result))
            {
                if (!_internetConnectionChecker.CheckConnection())
                {
                    await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                    return;
                }

                var conversation = await _model.GetConversationWithUser(result);
                if (conversation is null)
                {
                    await DisplayAlert("Failed", "User not found...", "OK");
                    _model.Username = string.Empty;
                }
                else
                {
                    _popped = false;
                    _model.Username = string.Empty;
                    await Navigation.PushAsync(App.Container.Resolve<ConversationPage>());
                }
            }
        }
    }
}