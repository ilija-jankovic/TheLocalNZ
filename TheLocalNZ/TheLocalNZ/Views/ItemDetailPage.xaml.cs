using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TheLocalNZ.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using static TheLocalNZ.App;

namespace TheLocalNZ.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        Listing listing;

        public ItemDetailPage(Listing listing)
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();

            this.listing = listing;
            carousel.ItemsSource = new string[] { listing.image1, listing.image2, listing.image3 };
            name.Text = listing.name;
            description.Text = listing.description;
        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            //add https protocol if protocol stated in link
            string link = listing.link;
            if (!((link.Length >= 8 && link.Substring(0, 8) == "https://") || (link.Length >= 7 && link.Substring(0, 7) == "http://")))
                link = "https://" + link;
            Launcher.OpenAsync(new Uri(link));
        }
    }
}