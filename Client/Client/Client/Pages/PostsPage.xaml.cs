using Acr.UserDialogs;
using Business.Helpers;
using Business.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostsPage : ContentPage
    {
        private readonly PostsViewModel _postsViewModel;
        private readonly IInternetConnectionChecker _internetConnectionChecker;

        public PostsPage(PostsViewModel postsViewModel, IInternetConnectionChecker internetConnectionChecker)
        {
            InitializeComponent();
            AddTapGestureRecognizers();

            _postsViewModel = postsViewModel;
            _internetConnectionChecker = internetConnectionChecker;
            BindingContext = _postsViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            _postsViewModel.LoadBookPosts();
        }

        private void AddTapGestureRecognizers()
        {
            var tgrFilter = new TapGestureRecognizer();
            tgrFilter.Tapped += (s, e) => OnFilteringLabelTapped();
            FilterLabel.GestureRecognizers.Add(tgrFilter);

            var tgrAddOffer = new TapGestureRecognizer();
            tgrAddOffer.Tapped += (s, e) => OnAddOfferTapped();
            MakeOfferLabel.GestureRecognizers.Add(tgrAddOffer);

            var tgrMessages = new TapGestureRecognizer();
            tgrMessages.Tapped += (s, e) => OnMessagingLabelTapped();
            ChatLabel.GestureRecognizers.Add(tgrMessages);

            var tgrSettings = new TapGestureRecognizer();
            tgrSettings.Tapped += (s, e) => OnSettingsTapped();
            SettingsLabel.GestureRecognizers.Add(tgrSettings);

            var tgrLeftNavigation = new TapGestureRecognizer();
            tgrLeftNavigation.Tapped += (s, e) => OnLeftNavigationTapped();
            LeftPageNavigation.GestureRecognizers.Add(tgrLeftNavigation);

            var tgrRightNavigation = new TapGestureRecognizer();
            tgrRightNavigation.Tapped += (s, e) => OnRightNavigationTapped();
            RightPageNavigation.GestureRecognizers.Add(tgrRightNavigation);
        }

        private async void OnFilteringLabelTapped()
        {
            await Navigation.PushAsync(App.Container.Resolve<FilteringPage>());
        }

        private async void OnAddOfferTapped()
        {
            await Navigation.PushAsync(App.Container.Resolve<AddPostPage>());
        }

        private async void OnMessagingLabelTapped()
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            using (UserDialogs.Instance.Loading("Loading conversations..."))
            {
                await Navigation.PushAsync(App.Container.Resolve<UserConversationsPage>());
            }
        }

        private async void OnSettingsTapped()
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            await Navigation.PushAsync(App.Container.Resolve<SettingsPage>());
        }

        private async void OnLeftNavigationTapped()
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            _postsViewModel.DecrementPage();
        }

        private async void OnRightNavigationTapped()
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            _postsViewModel.IncrementPage();
        }

        private async void OnSelectedPost(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(App.Container.Resolve<PostDetails>());
        }
    }
}