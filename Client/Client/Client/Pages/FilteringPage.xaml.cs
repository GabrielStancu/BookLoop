using Business.Helpers;
using Business.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilteringPage : ContentPage
    {
        private readonly FilterPostsViewModel _model;
        private readonly IInternetConnectionChecker _internetConnectionChecker;

        public FilteringPage(FilterPostsViewModel model, IInternetConnectionChecker internetConnectionChecker)
        {
            InitializeComponent();
            _model = model;
            _internetConnectionChecker = internetConnectionChecker;
            BindingContext = _model;
        }

        private async void OnFilterClicked(object sender, EventArgs e)
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            _model.CreateSpecification();
            await Navigation.PopAsync();
        }
    }
}