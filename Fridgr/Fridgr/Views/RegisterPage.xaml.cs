using System;
using Fridgr.Models;
using Fridgr.Models.Database;
using Xamarin.Forms;
using MongoDB.Driver;

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
            if (validDetails(entry_fname.Text, entry_lname.Text, entry_email.Text, entry_pw.Text))
            {
                User user = new User(entry_fname.Text, entry_lname.Text, entry_email.Text, entry_pw.Text);
                App.UserCollection.InsertOne(user);
                await Navigation.PushAsync(new LoginPage());
            } else
            {
               await DisplayAlert("Registration Error", errormsg, "Retry");
            }

            

        }

        public string errormsg = "";
        bool validDetails(string fname, string lname, string emailaddr, string pw)
        {
            if (!string.IsNullOrWhiteSpace(emailaddr) && !string.IsNullOrWhiteSpace(pw) && !string.IsNullOrWhiteSpace(fname) && !string.IsNullOrWhiteSpace(lname))
            {
                if (emailaddr.Contains("@"))
                {
                    if (pw.Length >= 6)
                    {
                        var user = App.UserCollection.Find(u => u.email == emailaddr).Limit(1).ToListAsync().Result;
                        if (user.Count == 0)
                        {
                            return true;
                        }
                        else
                        {
                            errormsg = "Email already in use";
                            return false;
                        }
                    }
                    else
                    {
                        errormsg = "Password length must be at least 6 characters long";
                        return false;
                    }
                }
                else
                {
                    errormsg = "Invalid email address";
                    return false;
                }

            }
            else
            {
                errormsg = "One or more of the fields is empty";
                return false;
            }
        }
    }
}
