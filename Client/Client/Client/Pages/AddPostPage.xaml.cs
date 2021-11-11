using Business.Helpers;
using Business.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPostPage : ContentPage
    {
        private readonly AddPostViewModel _model;
        private readonly IInternetConnectionChecker _internetConnectionChecker;

        public AddPostPage(AddPostViewModel model, IInternetConnectionChecker internetConnectionChecker)
        {
            InitializeComponent();
            _model = model;
            _internetConnectionChecker = internetConnectionChecker;
            BindingContext = _model;
        }

        private async void OnPostClicked(object sender, EventArgs e)
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            var posted = await _model.MakePostAsync();
            if(posted)
            {
                await DisplayAlert("Success", "Your post has been created!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Failed", "We could not create your post. Please try again.", "OK");
            }
        }

        private async void OnBookPhotoClicked(object sender, EventArgs e)
        {
            await _model.PickBookPhoto();
        }

        private void OnOfferTypeChanged(object sender, EventArgs e)
        {
            _model.EnablePrice();
        }
    }
}