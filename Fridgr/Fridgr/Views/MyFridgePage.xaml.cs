using System;
using System.Collections.ObjectModel;
using Fridgr.Models.Database;
using Fridgr.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fridgr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFridgePage : ContentPage
    {
        private FoodItemsViewModel viewModel;
        
        public MyFridgePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new FoodItemsViewModel(false);

            viewModel.FoodItems = new ObservableCollection<FoodItem>();
            //var users = App.UserCollection.Find(u => u.email == "test@case.com").ToListAsync().Result;
            //var user = users.Count == 1 ? users.ElementAt(0) : null;
            var user = App.currentUser;

            if (user?.FoodIds != null)
            {
                foreach (var food in user.Foods)
                {
                    food.FoodItem = new FoodItem(food.FoodItemId);
                    viewModel.FoodItems.Add(food.FoodItem);
                }
            }

            ItemsListView.ItemsSource = viewModel.FoodItems;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                FoodItem foodItem = new FoodItem(viewModel.FoodItems[e.SelectedItemIndex].Id);
                await Navigation.PushAsync(new FoodItemDetailPage(new FoodItemDetailViewModel(foodItem))); 
                ((ListView) sender).SelectedItem = null;
            }
        }
        
        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FoodItemsPage());
        }
    }
}