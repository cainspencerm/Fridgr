using System;
using System.Collections.Generic;
using Fridgr.Models;
using Fridgr.Models.Database;
using Xamarin.Forms;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Fridgr.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            BackgroundColor = Constants.background;


            entry_fname.Completed += (s, e) => entry_lname.Focus();
            entry_lname.Completed += (s, e) => entry_email.Focus();
            entry_email.Completed += (s, e) => entry_pw.Focus();
            entry_pw.Completed += (s, e) => CreateNewAcct(s, e);
        }

        async void CreateNewAcct(object sender, EventArgs e)
        {
            if(!validDetails(entry_fname.Text, entry_lname.Text, entry_email.Text, entry_pw.Text))
            {
                await DisplayAlert("Registration Error", "One or more of the fields is invalid", "Retry");
            }

            User user = new User(entry_fname.Text, entry_lname.Text, entry_email.Text, entry_pw.Text);
            App.UserCollection.InsertOne(user);

        }

        bool validDetails(string fname, string lname, string email, string pw)
        {
            if (!string.IsNullOrWhiteSpace(email) &&
                !string.IsNullOrWhiteSpace(pw) &&
                !string.IsNullOrWhiteSpace(fname) &&
                !string.IsNullOrWhiteSpace(lname) &&
                email.Contains("@") &&
                pw.Length > 6 &&
                !App.UserCollection.Equals(email))
            {
                return true;
            }
            else return false;
        }
    }
}
