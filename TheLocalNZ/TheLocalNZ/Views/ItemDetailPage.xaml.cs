using System.Collections.ObjectModel;
using System.ComponentModel;
using TheLocalNZ.ViewModels;
using Xamarin.Forms;
using static TheLocalNZ.App;

namespace TheLocalNZ.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public Listing listing { get; set; }

        public ItemDetailPage(Listing listing)
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();

            this.listing = listing;
            name.Text = listing.name;
            description.Text = listing.description;
        }
    }
}