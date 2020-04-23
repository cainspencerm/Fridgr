using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridgr.Models;
using Fridgr.Models.Database;
using Fridgr.ViewModels;
using MongoDB.Bson;
using MongoDB.Driver;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fridgr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFridgePage : ContentPage
    {
        private ObservableCollection<Food> foods;
        
        public MyFridgePage()
        {
            InitializeComponent();
            
            foods = new ObservableCollection<Food>();
            //var users = App.UserCollection.Find(u => u.email == "test@case.com").ToListAsync().Result;
            //var user = users.Count == 1 ? users.ElementAt(0) : null;
            var user = App.currentUser;

            if (user?.foods != null)
            {
                foreach (var food in user.foods)
                {
                    food.FoodItem = new FoodItem(food.FoodItemId);
                    foods.Add(food);
                }
            }
            
            ItemsListView.ItemsSource = foods;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                FoodItem foodItem = new FoodItem(foods[e.SelectedItemIndex].FoodItemId);
                await Navigation.PushAsync(new FoodItemDetailPage(new FoodItemDetailViewModel(foodItem))); 
                ((ListView) sender).SelectedItem = null;
            } 
            else 
            {
                Console.WriteLine("Why is this getting called again?");
            }
        }
    }
}