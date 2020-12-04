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

        void OnButtonClicked(object sender, EventArgs args)
        {
            Launcher.OpenAsync(new Uri("https://www.thelocalnz.com/"));
        }
    }
}