using System;
using TheLocalNZ.Services;
using TheLocalNZ.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading;
using Xamarin.Essentials;
using System.Collections.ObjectModel;

namespace TheLocalNZ
{
    public partial class App : Application
    {
        static App appRef;

        public App()
        {
            appRef = this;

            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        public class Listing
        {
            public string name { get; set; }
            public string description { get; set; }
            public string link { get; set; }
            public string image1 { get; set; }
            public string image2 { get; set; }
            public string image3 { get; set; }
        }

        static ObservableCollection<Listing> _listings = new ObservableCollection<Listing>();
        public static ObservableCollection<Listing> Listings { get { return _listings; } }

        protected override void OnStart()
        {
            GetListingsData();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            
        }

        public static async void GetListingsData()
        {
            if (Listings.Count == 0 && Connectivity.NetworkAccess == NetworkAccess.Internet || Connectivity.NetworkAccess == NetworkAccess.ConstrainedInternet)
            {
                var client = new HttpClient();
                var response = await client.GetAsync("https://www.thelocalnz.com/_functions/listings");
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;

                    JObject json = JObject.Parse(content);

                    IList<JToken> results = json["items"].Children().ToList();

                    _listings.Clear();

                    //serialize JSON results into .NET objects
                    foreach (JToken result in results)
                    {
                        Listing listing = result.ToObject<Listing>();
                        listing.image1 = appRef.GetURLFromSrc(listing.image1);
                        listing.image2 = appRef.GetURLFromSrc(listing.image2);
                        listing.image3 = appRef.GetURLFromSrc(listing.image3);

                        //remove later
                        ImageService.Instance.LoadUrl(listing.image1).Preload();
                        ImageService.Instance.LoadUrl(listing.image2).Preload();
                        ImageService.Instance.LoadUrl(listing.image3).Preload();
                        //

                        _listings.Add(listing);
                    }
                }
            }
        }

        string GetURLFromSrc(string src)
        {
            return "https://static.wixstatic.com/media/" + src.Split('/')[3];
        }
    }
}
