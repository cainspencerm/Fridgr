using System;
using System.Collections.Generic;

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
        }

        public void loginProdecure(object sender, EventArgs e)
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
