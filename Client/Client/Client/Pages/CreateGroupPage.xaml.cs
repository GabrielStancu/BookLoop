using Business.Helpers;
using Business.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateGroupPage : ContentPage
    {
        private readonly CreateGroupViewModel _createGroupViewModel;
        private readonly IInternetConnectionChecker _internetConnectionChecker;

        public CreateGroupPage(CreateGroupViewModel createGroupViewModel, IInternetConnectionChecker internetConnectionChecker)
        {
            InitializeComponent();
            _createGroupViewModel = createGroupViewModel;
            _internetConnectionChecker = internetConnectionChecker;
            BindingContext = _createGroupViewModel;
        }

        private async void OnAddUserClicked(object sender, EventArgs e)
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            bool added = await _createGroupViewModel.AddUser();

            if(!added)
            {
                await DisplayAlert("Error", "The provided user does not exist or was already added.", "OK");
            }
        }

        private async void OnCreateGroupClicked(object sender, EventArgs e)
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            await _createGroupViewModel.CreateGroup();
            await Navigation.PushAsync(App.Container.Resolve<ConversationPage>());
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}