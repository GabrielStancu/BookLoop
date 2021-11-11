using Data.Helpers;
using Xamarin.Essentials;

namespace Business.Helpers
{
    public class UserCredetialsRetriever : IUserCredetialsRetriever
    {
        public LoginUserDTO GetUserCredentials()
        {
            if (Preferences.Get("stored_user", string.Empty) != true.ToString())
            {
                return null;
            }

            var loginUser = new LoginUserDTO()
            {
                Username = Preferences.Get("username", string.Empty),
                Password = Preferences.Get("password", string.Empty)
            };

            return loginUser;
        }
    }
}
