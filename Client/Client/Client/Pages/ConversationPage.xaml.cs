using Business.Helpers;
using Business.ViewModels;
using System;
using System.Collections.Specialized;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConversationPage : ContentPage
    {
        private readonly ConversationViewModel _model;
        private readonly IInternetConnectionChecker _internetConnectionChecker;

        public ConversationPage(ConversationViewModel model, IInternetConnectionChecker internetConnectionChecker)
        {
            InitializeComponent();
            _model = model;
            _internetConnectionChecker = internetConnectionChecker;
            BindingContext = _model;
            ((INotifyCollectionChanged)_model.Messages).CollectionChanged += ConversationPage_CollectionChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(() => {
                ScrollAtEnd();
            });
        }

        private void ConversationPage_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ScrollAtEnd();
        }

        protected async override void OnDisappearing()
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            base.OnDisappearing();
            await _model.DeleteConversationIfEmpty(); 
        }

        private async void OnSendMessageClicked(object sender, EventArgs e)
        {
            if (!_internetConnectionChecker.CheckConnection())
            {
                await DisplayAlert(Properties.Resources.NoNetworkMsgTitle, Properties.Resources.NoNetworkMsgContent, Properties.Resources.NoNetworkMsgConfirm);
                return;
            }

            await _model.SendMessage();   
        }

        private void ScrollAtEnd()
        {
            if(_model.Messages.Count > 0)
            {
                MessagesListView.ScrollTo(_model.Messages.Last(), ScrollToPosition.MakeVisible, false);
            }
        }
    }
}