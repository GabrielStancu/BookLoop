using Acr.UserDialogs;
using Business.Helpers;
using Business.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IInternetConnectionChecker _internetConnectionChecker;
        private readonly IUserCredetialsRetriever _userCredetialsRetriever;
        private readonly IUserCredentialsSaver _userCredentialsSaver;
        private bool _appStart = true;

        public LoginPage(LoginViewModel loginViewModel,
                         IInternetConnectionChecker internetConnectionChecker,
                         IUserCredetialsRetriever userCredetialsRetriever,
                         IUserCredentialsSaver userCredentialsSaver)
        {
            InitializeComponent();
            _loginViewModel = loginViewModel;
            _internetConnectionChecker = internetConnectionChecker;
            _userCredetialsRetriever = userCredetialsRetriever;
            _userCredentialsSaver = userCredentialsSaver;
            BindingContext = loginViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var savedUser = _userCredetialsRetriever.GetUserCredentials();
            if (savedUser != null)
            {
                _loginViewModel.Username = savedUser.Username;
                PasswordEntry.Text = savedUser.Password;
                _loginViewModel.RememberUser = true;

                if (_appStart)
                {
                    Login();
                    _appStart = false;
                }
            }
            else
            {
                _appStart = false;
                _loginViewModel.RememberUser = false;
            }
        }

        private void OnLoginButtonClick(object sender, EventArgs e)
        {
            Login();
        }

        private async void Login()
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            using(UserDialogs.Instance.Loading("Logging in..."))
            {
                var loginResult = await _loginViewModel.LoginAsync(PasswordEntry.Text);
                if (loginResult.Successful)
                {
                    await Navigation.PushAsync(App.Container.Resolve<PostsPage>());
                    _loginViewModel.Username = string.Empty;
                    PasswordEntry.Text = string.Empty;
                }
                else
                {
                    await DisplayAlert("Error", "Bad credentials!", "OK");
                }
            }
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(App.Container.Resolve<SignupPage>());
        }

        private void OnRememberChecked(object sender, CheckedChangedEventArgs e)
        {
            if(e.Value == false)
            {
                _userCredentialsSaver.ClearUserCredentials();
            }
        }
    }
}