using System;
using Fridgr.Models;
using Xamarin.Forms;

namespace Fridgr.Views
{
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();

            this.BackgroundColor = Constants.background;

            NavigationPage.SetHasNavigationBar(this, false);

            NameLabel.Text = App.currentUser.firstName + "\n" + App.currentUser.lastName;
            EmailLabel.Text = App.currentUser.email;

            OldPassword.WidthRequest = 300;
            NewPassword.WidthRequest = 300;
            ConfirmPassword.WidthRequest = 300;
        }

        async void logoutProcedure(object sender, EventArgs e)
        {
            App.currentUser = null;
            await Navigation.PushAsync(new LoginPage());
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
            if (App.currentUser.password != OldPassword.Text)
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
        }
    }
}
