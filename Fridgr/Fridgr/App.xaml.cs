using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Fridgr.Services;
using Fridgr.Views;
using Fridgr.Models.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using Fridgr.Models;

namespace Fridgr
{
    public partial class App : Application
    {
        public static IMongoCollection<User> UserCollection;
        public static IMongoCollection<FoodItem> FoodItemCollection;

        public static User currentUser;

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());     // need NavigationPage to support PushAsync
        }

        protected override void OnStart()
        {
            var client = new MongoClient("mongodb+srv://user:BzypQCaeTwbudvWt@fridgr-bs8ph.gcp.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("Fridgr");
            UserCollection = database.GetCollection<User>("User");
            FoodItemCollection = database.GetCollection<FoodItem>("FoodItems");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
