using System;
using System.Linq;
using Fridgr.Models;
using Fridgr.Models.Database;
using MongoDB.Driver;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fridgr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            BackgroundColor = Constants.background;

            entry_email.BackgroundColor = Constants.secondaryBackground;
            entry_pw.BackgroundColor = Constants.secondaryBackground;

            entry_email.Completed += (s, e) => entry_pw.Focus();
            entry_pw.Completed += (s, e) => loginProdecure(s, e);
        }

        async void DeveloperLogin(object sender, EventArgs e)
        {
            var user = App.UserCollection.Find(u => u.Email == "test@case.com").FirstOrDefaultAsync().Result;
            user.Foods = new Food[user.FoodIds.Count];
            for (int i = 0; i < user.FoodIds.Count; i++)
            {
                var doc = user.FoodIds[i].AsBsonDocument;
                user.Foods[i] = new Food(doc);
            }
            
            App.currentUser = user;
            App.NavPage = new MainPage();
            await Navigation.PushAsync(App.NavPage);
        }

        async void loginProdecure(object sender, EventArgs e)
        {
            if (CheckLogin(entry_email.Text, entry_pw.Text))
            {
                var user = App.UserCollection.Find(u => u.Email == entry_email.Text).FirstOrDefaultAsync().Result;
                user.Foods = new Food[user.FoodIds.Count];
                for (int i = 0; i < user.FoodIds.Count; i++)
                {
                    var doc = user.FoodIds[i].AsBsonDocument;
                    user.Foods[i] = new Food(doc);
                }
            
                App.currentUser = user;
                App.NavPage = new MainPage();
                await Navigation.PushAsync(App.NavPage);
            } else
            {
                await DisplayAlert("Login Error", "Invalid email or password", "Retry");
            }

        }

        async void registerProcedure(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        public bool CheckLogin(string email, string pw)
        {
            if (email != null && pw != null)
            {
                var user = App.UserCollection.Find(u => u.Email == email).Limit(1).ToListAsync().Result;
                return user.Count > 0 && user.ElementAt(0).Password == pw;
            }
            else return false;
        }
    }
}