using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridgr.Models.Database;
using MongoDB.Driver;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fridgr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFridgePage : ContentPage
    {
        private ObservableCollection<string> items;
        
        public MyFridgePage()
        {
            InitializeComponent();
            
            items = new ObservableCollection<string>();
            var users = App.UserCollection.Find(u => u.email == "test@case.com").ToListAsync().Result;
            var user = users.Count == 1 ? users.ElementAt(0) : null;

            if (user?.foods != null)
            {
                foreach (var food in user.foods)
                {
                    var id = food.FoodItem;
                    FoodItem foodItem = new FoodItem("fake", "fake");
                    items.Add(foodItem?.FoodName);
                }
            }
            
            ItemsListView.ItemsSource = items;
        }


        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}