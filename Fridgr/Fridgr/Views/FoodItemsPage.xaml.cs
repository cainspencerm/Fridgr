using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridgr.Models;
using Fridgr.Models.Database;
using Fridgr.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fridgr.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class FoodItemsPage : ContentPage
    {
        FoodItemsViewModel viewModel;

        public FoodItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new FoodItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as FoodItem;
            if (item == null)
                return;

            await Navigation.PushAsync(new FoodItemDetailPage(new FoodItemDetailViewModel(item)));

            // Manually deselect item.
            FoodItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewFoodItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.FoodItems.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
        
        
    }
}