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
        public static IMongoCollection<User> users;

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());     // need NavigationPage to support PushAsync
        }

        protected override void OnStart()
        {
            var client = new MongoClient(Constants.connectionString);
            var database = client.GetDatabase("fridgr");
            users = database.GetCollection<User>("User");

            //if (users != null) Console.WriteLine("Connected to users.");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
