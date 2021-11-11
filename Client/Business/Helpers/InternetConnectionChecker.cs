using Xamarin.Essentials;

namespace Business.Helpers
{
    public class InternetConnectionChecker : IInternetConnectionChecker
    {
        public bool CheckConnection()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }
    }
}
