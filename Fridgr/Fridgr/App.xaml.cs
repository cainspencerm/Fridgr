using Xamarin.Forms;
using Fridgr.Views;
using Fridgr.Models.Database;
using MongoDB.Driver;

namespace Fridgr
{
    public partial class App : Application
    {
        public static IMongoCollection<User> UserCollection;
        public static IMongoCollection<FoodItem> FoodItemCollection;
        
        public App()
        {
            InitializeComponent();
            MainPage = new LoginPage();
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
