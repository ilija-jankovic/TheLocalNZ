using System;
using System.Collections.Generic;
using System.ComponentModel;
using TheLocalNZ.Models;
using TheLocalNZ.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheLocalNZ.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}