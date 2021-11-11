using Acr.UserDialogs;
using Client.Pages;
using Client.Utils;
using Nancy.TinyIoc;
using Xamarin.Forms;

namespace Client
{
    public partial class App : Application
    {
        public static TinyIoCContainer Container;
        public App()
        {
            InitializeComponent();

            Container = new TinyIoCContainer();
            Container.RegisterClient();
            using (UserDialogs.Instance.Loading("Loading data..."))
            {
                MainPage = new NavigationPage(Container.Resolve<LoginPage>());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
