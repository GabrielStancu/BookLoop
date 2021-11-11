using Business.Helpers;
using Business.Services;
using Business.Shared;
using Data.Helpers;
using System.Threading.Tasks;

namespace Business.ViewModels
{
    public class LoginViewModel: BaseViewModel
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                SetProperty<string>(ref _username, value);
            }

        }

        private bool _rememberUser;
        public bool RememberUser
        {
            get { return _rememberUser; }
            set
            {
                _rememberUser = value;
                SetProperty<bool>(ref _rememberUser, value);
            }
        }

        private readonly IUserCredentialsSaver _userCredentialsSaver;
        private readonly ILoginService _loginService;

        public LoginViewModel(IUserCredentialsSaver userCredentialsSaver, ILoginService loginService)
        {
            _userCredentialsSaver = userCredentialsSaver;
            _loginService = loginService;
        }

        public async Task<LoginResult> LoginAsync(string password)
        {
            var loginUser = new LoginUserDTO()
            {
                Username = Username,
                Password = password
            };

            var loginResult = await _loginService.LoginAsync(loginUser);
            if(loginResult.Successful)
            {
                DataContainer.User = loginResult.User;
                DataContainer.Specification.UserId = loginResult.User.Id;

                if(RememberUser)
                {
                    _userCredentialsSaver.SaveUserCredentials(Username, password);
                }
                
            }

            return loginResult;
        }
    }
}
