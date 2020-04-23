using System;
using System.Linq;
using MongoDB.Driver;
using Fridgr.Models;
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

            entry_email.Completed += (s, e) => entry_pw.Focus();
            entry_pw.Completed += (s, e) => loginProdecure(s, e);
        }

        async void DeveloperLogin(object sender, EventArgs e)
        {
            App.currentUser = App.UserCollection.Find(u => u.email == "test@case.com").FirstOrDefaultAsync().Result;
            await Navigation.PushAsync(new MainPage());
        }

        async void loginProdecure(object sender, EventArgs e)
        {
            if (CheckLogin(entry_email.Text, entry_pw.Text))
            {
                App.currentUser = App.UserCollection.Find(u => u.email == entry_email.Text).FirstOrDefaultAsync().Result;
                await Navigation.PushAsync(new MainPage());
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
                var user = App.UserCollection.Find(u => u.email == email).Limit(1).ToListAsync().Result;
                return user.Count > 0 && user.ElementAt(0).password == pw;
            }
            else return false;
        }
    }
}