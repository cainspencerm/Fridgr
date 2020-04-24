using System;
using Fridgr.Models;
using Fridgr.Models.Database;
using Xamarin.Forms;

namespace Fridgr.Views
{
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();

            this.BackgroundColor = Constants.background;
            
            OldPassword.BackgroundColor = Constants.secondaryBackground;
            NewPassword.BackgroundColor = Constants.secondaryBackground;
            ConfirmPassword.BackgroundColor = Constants.secondaryBackground;

            NameLabel.Text = App.currentUser.FirstName + "\n" + App.currentUser.LastName;
            EmailLabel.Text = App.currentUser.Email;

            OldPassword.WidthRequest = 300;
            NewPassword.WidthRequest = 300;
            ConfirmPassword.WidthRequest = 300;
        }

        async void logoutProcedure(object sender, EventArgs e)
        {
            App.currentUser = null;
            await App.NavPage.Navigation.PopAsync();
            App.NavPage = null;
            //throw new NotImplementedException();
        }

        private void ResetPasswordButton_OnPressed(object sender, EventArgs e)
        {
            OldPassword.IsVisible = !OldPassword.IsVisible;
            NewPassword.IsVisible = !NewPassword.IsVisible;
            ConfirmPassword.IsVisible = !ConfirmPassword.IsVisible;
            ChangePasswordButton.IsVisible = !ChangePasswordButton.IsVisible;
        }

        private async void ChangePasswordButton_OnPressed(object sender, EventArgs e)
        {
            if (App.currentUser.Password != OldPassword.Text)
            {
                await DisplayAlert("Invalid", "Current password does not match.", "Retry");
                return;
            }

            if (NewPassword.Text != ConfirmPassword.Text)
            {
                await DisplayAlert("Invalid", "New password and confirm password do not match.", "Retry");
                return;
            }
            
            // TODO Update user password.
            App.currentUser.Password = ConfirmPassword.Text;
            await User.DataStore.UpdateItemAsync(App.currentUser);

            OldPassword.Text = "";
            NewPassword.Text = "";
            ConfirmPassword.Text = "";

            ResetPasswordButton_OnPressed(null, null);
            
            await DisplayAlert("Password Changed", null, "Retry");
        }
    }
}
