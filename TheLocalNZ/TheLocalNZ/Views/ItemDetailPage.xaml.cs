using System.Collections.ObjectModel;
using System.ComponentModel;
using TheLocalNZ.ViewModels;
using Xamarin.Forms;
using static TheLocalNZ.App;

namespace TheLocalNZ.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage(Listing listing)
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();

            imgSlider.Images = new ObservableCollection<FileImageSource>() { listing.image1, listing.image2, listing.image3 };
            name.Text = listing.name;
            description.Text = listing.description;
        }
    }
}