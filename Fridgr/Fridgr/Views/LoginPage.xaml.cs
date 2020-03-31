using System;
using System.Linq;
using System.Collections.Generic;

using MongoDB.Driver;
using MongoDB.Bson;
using Fridgr.Models;
using Fridgr.Models.Database;
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

        async void loginProdecure(object sender, EventArgs e)
        {
            //User user = new User(entry_email.Text, entry_pw.Text);
            if (CheckLogin(entry_email.Text, entry_pw.Text))
            {
                await DisplayAlert("Login Successful", "Login Successful", "Continue");
                //await PushAsync
            } else
            {
                await DisplayAlert("Login Error", "Invalid email or password", "Retry");
            }

        }

        async void registerProcedure(object sender, EventArgs e)
        {
            //await DisplayAlert("Register", "Register New User", "Create New Account");
            await Navigation.PushAsync(new RegisterPage());
        }

        public bool CheckLogin(string email, string pw)
        {
            if (email != null && pw != null)
            {
                var user = App.users.Find(u => u.email == email).Limit(1).ToListAsync().Result;
                return user.ElementAt(0).password == pw;
            }
            else return false;
        }
    }
}