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
using static TheLocalNZ.App;

namespace TheLocalNZ.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            GetListingsData();
            itemList.ItemsSource = Listings;
            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();

        }

        private async void CellButton_Tapped(object sender, EventArgs e)
        {
            //get item from the clicked cell and send it to item detail page
            var page = new ItemDetailPage((Listing)((ViewCell)((Button)sender).Parent.Parent).BindingContext);
            await Navigation.PushAsync(page, true);
        }
    }
}