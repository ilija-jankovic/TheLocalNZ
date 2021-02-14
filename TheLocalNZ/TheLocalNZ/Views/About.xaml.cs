using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace TheLocalNZ.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        void WebsiteLinkClicked(object sender, EventArgs args)
        {
            Launcher.OpenAsync(new Uri("https://www.thelocalnz.com/"));
        }

        void BrowseListingsClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ItemsPage(), true);
        }
    }
}