using Xamarin.Essentials;

namespace Business.Helpers
{
    public class UserCredentialsSaver : IUserCredentialsSaver
    {
        public void SaveUserCredentials(string username, string password)
        {
            Preferences.Set("username", username);
            Preferences.Set("password", password);
            Preferences.Set("stored_user", true.ToString());
        }

        public void ClearUserCredentials()
        {
            Preferences.Set("username", string.Empty);
            Preferences.Set("password", string.Empty);
            Preferences.Set("stored_user", false.ToString());
        }
    }
}
