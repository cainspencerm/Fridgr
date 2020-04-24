using Fridgr.Models;
using Fridgr.Models.Database;
using Fridgr.Views;
using MongoDB.Driver;
using Xamarin.Forms;

namespace Fridgr
{
    public partial class App : Application
    {
        public static IMongoCollection<User> UserCollection;
        public static IMongoCollection<FoodItem> FoodItemCollection;
        public static Page NavPage;

        public static User currentUser;

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Constants.secondaryBackground
            };     // need NavigationPage to support PushAsync
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
