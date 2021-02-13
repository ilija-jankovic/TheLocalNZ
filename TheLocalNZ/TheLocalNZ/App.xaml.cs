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

namespace TheLocalNZ
{
    public partial class App : Application
    {

        public App()
        {
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

        static List<Listing> _listings = new List<Listing>();
        public static List<Listing> Listings { get { return _listings; } }

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

        async void GetListingsData()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://www.thelocalnz.com/_functions/listings");
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;

                JObject json = JObject.Parse(content);

                IList<JToken> results = json["items"].Children().ToList();

                //serialize JSON results into .NET objects
                foreach (JToken result in results)
                {
                    Listing listing = result.ToObject<Listing>();
                    listing.image1 = GetURLFromSrc(listing.image1);
                    listing.image2 = GetURLFromSrc(listing.image2);
                    listing.image3 = GetURLFromSrc(listing.image3);

                    //remove later
                    ImageService.Instance.LoadUrl(listing.image1).Preload();
                    ImageService.Instance.LoadUrl(listing.image2).Preload();
                    ImageService.Instance.LoadUrl(listing.image3).Preload();
                    //

                    _listings.Add(listing);
                }

                //randomise listings for fairness
                var rand = new Random();
                _listings = _listings.OrderBy(x => rand.Next()).ToList();
            }
        }

        string GetURLFromSrc(string src)
        {
            return "https://static.wixstatic.com/media/" + src.Split('/')[3];
        }
    }
}
