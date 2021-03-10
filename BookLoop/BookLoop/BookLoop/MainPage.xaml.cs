using System;
using Xamarin.Forms;

namespace BookLoop
{
    public partial class MainPage : ContentPage
    {
        readonly ChatViewModel model;
        public MainPage()
        {
            InitializeComponent();
            model = new ChatViewModel();
            this.BindingContext = model;

            ConnectToChat();
        }

        private async void ConnectToChat()
        {
            await model.Connect();
        }
    }
}
