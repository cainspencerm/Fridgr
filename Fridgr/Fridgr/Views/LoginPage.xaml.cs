using System;
using System.Collections.Generic;

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
            Spinner.IsVisible = false;

            entry_user.Completed += (s, e) => entry_pw.Focus();
            entry_pw.Completed += (s, e) => loginProdecure(s, e);
        }

        void loginProdecure(object sender, EventArgs e)
        {
            User user = new User(entry_user.Text, entry_pw.Text);
            if(user.checkLogin())
            {
                DisplayAlert("Login Successful", "Login Successful", "Continue");
            } else
            {
                DisplayAlert("Login Error", "Empty username or password", "Retry");
            }

        }
    }
}
