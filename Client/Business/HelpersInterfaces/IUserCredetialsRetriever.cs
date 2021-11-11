using Data.Helpers;

namespace Business.Helpers
{
    public interface IUserCredetialsRetriever
    {
        LoginUserDTO GetUserCredentials();
    }
}