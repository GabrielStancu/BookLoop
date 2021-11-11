using Business.Helpers;
using Business.ViewModels;
using Data.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : TabbedPage
    {
        private readonly int _pagesFromLogin = 2;
        private readonly SettingsViewModel _settingsViewModel;
        private readonly IInternetConnectionChecker _internetConnectionChecker;
        private readonly IUserCredentialsSaver _userCredentialsSaver;

        public SettingsPage(SettingsViewModel settingsViewModel, IInternetConnectionChecker internetConnectionChecker, IUserCredentialsSaver userCredentialsSaver)
        {
            InitializeComponent();
            _settingsViewModel = settingsViewModel;
            _internetConnectionChecker = internetConnectionChecker;
            _userCredentialsSaver = userCredentialsSaver;
            BindingContext = _settingsViewModel;
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            for (var counter = 1; counter < _pagesFromLogin; counter++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            }

            _userCredentialsSaver.ClearUserCredentials();
            await Navigation.PopAsync();
        }

        private async void OnProfileChangeClicked(object sender, EventArgs e)
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            await _settingsViewModel.PickProfilePhoto();
        }

        private async void OnSaveChangesClicked(object sender, EventArgs e)
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            bool updated = await _settingsViewModel.SaveProfileChangesAsync();

            if(updated)
            {
                await DisplayAlert("Success", "Changes saved successfully!", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Not valid email, or already in use. Please try again.", "OK");
            }
        }

        private void OnPostTapped(object sender, SelectedItemChangedEventArgs e)
        {
            var bookPost = e.SelectedItem as BookPost;
            OnDeletePostClicked(bookPost);
        }

        private async void OnDeletePostClicked(BookPost bookPost)
        {
            var delete = await DisplayAlert("Delete post", "Are you sure you want to delete this post?", "Yes", "No");

            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            if (delete)
            {
                await _settingsViewModel.DeletePostAsync(bookPost);
                await DisplayAlert("Success", "Post deleted succesfully!", "OK");
            }
        }
    }
}