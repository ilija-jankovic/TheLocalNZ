using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLocalNZ.Models;
using TheLocalNZ.ViewModels;
using TheLocalNZ.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static TheLocalNZ.App;

namespace TheLocalNZ.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        static int page = 0;
        const int LISTINGS_PER_PAGE = 5;

        public ItemsPage()
        {
            InitializeComponent();

            //listen for changes in collection
            Listings.CollectionChanged += ListingsUpdated;
            GetListingsData();
            DisplayListings();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        void ListingsUpdated(object sender, NotifyCollectionChangedEventArgs e)
        {
            DisplayListings();
        }

        private async void CellButton_Tapped(object sender, EventArgs e)
        {
            //get item from the clicked cell and send it to item detail page
            var page = new ItemDetailPage((Listing)((ViewCell)((Button)sender).Parent.Parent).BindingContext);
            await Navigation.PushAsync(page, true);
        }

        void PreviousPage(object sender, EventArgs e)
        {
            if (page > 0)
            {
                page--;
                DisplayListings();
            }
        }

        void NextPage(object sender, EventArgs e)
        {
            if (page < Listings.Count / LISTINGS_PER_PAGE)
            {
                page++;
                DisplayListings();
            }
        }

        void DisplayListings()
        {
            List<Listing> pageListings = new List<Listing>();
            for (int i = page * LISTINGS_PER_PAGE; i < Math.Min((page + 1) * LISTINGS_PER_PAGE, Listings.Count); i++)
                pageListings.Add(Listings[i]);
            itemList.ItemsSource = pageListings;
            pageText.Text = (page + 1) + "/" + (int)Math.Ceiling((double) Listings.Count / LISTINGS_PER_PAGE);
        }
    }
}