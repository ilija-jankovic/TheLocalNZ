using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLocalNZ.Models;
using TheLocalNZ.ViewModels;
using TheLocalNZ.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheLocalNZ.Views
{
    public class ListingDisplay
    {
        public string Image1 { get; set; }
        public string Text { get; set; }

        public ListingDisplay() { }

        public ListingDisplay(string image1, string text)
        {
            this.Image1 = image1;
            this.Text = text;
        }
    }

    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();

            if (App.Listings == null)
                return;

            ListingDisplay[] displays = new ListingDisplay[App.Listings.Count];
            foreach (App.Listing listing in App.Listings)
            {
                string text = listing.name + '\n'
                            + listing.description + '\n'
                            + listing.link;
                displays[App.Listings.IndexOf(listing)] = new ListingDisplay(listing.image1, text);
            }

            itemList.ItemsSource = displays;
        }
    }
}