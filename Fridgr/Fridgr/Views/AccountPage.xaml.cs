using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Fridgr.Views
{
    public partial class AccountPage : ContentPage
    {
      

        public AccountPage()
        {
            InitializeComponent();
        }

        async void logoutProcedure(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
