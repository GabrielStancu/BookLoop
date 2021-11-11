using Business.Helpers;
using Business.ViewModels;
using Data.Helpers;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        private readonly SignupViewModel _signupViewModel;
        private readonly IInternetConnectionChecker _internetConnectionChecker;

        public SignupPage(SignupViewModel signupViewModel, IInternetConnectionChecker internetConnectionChecker)
        {
            InitializeComponent();
            _signupViewModel = signupViewModel;
            _internetConnectionChecker = internetConnectionChecker;
            BindingContext = signupViewModel;
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            var signupResult = await _signupViewModel.SignupAsync(PasswordEntry.Text, RePasswordEntry.Text);

            if(signupResult == SignupResult.BadCredentials)
            {
                await DisplayAlert("Error", "Bad credentials!", "OK");
            }
            else if (signupResult == SignupResult.UserAlreadyRegistered)
            {
                await DisplayAlert("Error", "User already registered", "OK");
            }
            else
            {
                await DisplayAlert("Success", "Your account has been created", "OK");
                await Navigation.PopAsync();
            }
        }

        private async void OnProfilePhotoClicked(object sender, EventArgs e)
        {
            await _signupViewModel.PickProfilePhoto();
        }
    }
}