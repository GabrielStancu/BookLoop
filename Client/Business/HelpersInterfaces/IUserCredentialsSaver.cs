namespace Business.Helpers
{
    public interface IUserCredentialsSaver
    {
        void ClearUserCredentials();
        void SaveUserCredentials(string username, string password);
    }
}