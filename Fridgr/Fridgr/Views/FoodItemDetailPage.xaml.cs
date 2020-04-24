using System;
using Fridgr.Models;
using Fridgr.Models.Database;
using Fridgr.ViewModels;
using MongoDB.Bson;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fridgr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodItemDetailPage : ContentPage
    {
        private readonly FoodItemDetailViewModel viewModel;

        public FoodItemDetailPage()
        {
            InitializeComponent();

            //var item = FoodItem.FindFoodItem("Bisquick", "Bisquick Pancakes");
            BindingContext = viewModel = new FoodItemDetailViewModel();

            Init();
        }

        public FoodItemDetailPage(FoodItem foodItem)
        {
            InitializeComponent();

            BindingContext = viewModel = new FoodItemDetailViewModel(foodItem);

            Init();
        }

        public FoodItemDetailPage(FoodItemDetailViewModel model)
        {
            InitializeComponent();

            BindingContext = viewModel = model;

            Init();
        }

        public FoodItemDetailPage(FoodItemDetailViewModel model, bool addButtonIsVisible)
        {
            InitializeComponent();

            BindingContext = viewModel = model;

            Init();
            AddButton.IsVisible = addButtonIsVisible;
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync();
            Navigation.PopAsync();
            Navigation.PushAsync(new MyFridgePage());
            return true;
        }

        private void Init()
        {
            BackgroundColor = Constants.background;
            
            AddFoodPurchaseDate.BackgroundColor = Constants.secondaryBackground;
            AddFoodExpirationDate.BackgroundColor = Constants.secondaryBackground;
            AddFoodServings.BackgroundColor = Constants.secondaryBackground;

            if (viewModel.FoodItem == null) return;

            FoodItemTitle.Text = viewModel.FoodItem.BrandName + " " + viewModel.FoodItem.FoodName;
            NutritionFacts.Text = viewModel.FoodItem.Nutrition.ToString();
            FoodType.Text = "Food type: " + Enum.GetName(typeof(FoodType), viewModel.FoodItem.FoodType);
        }

        public void OnAddButtonPressed(object sender, EventArgs e)
        {
            AddFoodPurchaseDate.IsVisible = !AddFoodPurchaseDate.IsVisible;
            AddFoodExpirationDate.IsVisible = !AddFoodExpirationDate.IsVisible;
            AddFoodServings.IsVisible = !AddFoodServings.IsVisible;
            AddFoodConfirmButton.IsVisible = !AddFoodConfirmButton.IsVisible;
        }
        
        public async void OnConfirmButtonPressed(object sender, EventArgs e)
        {
            var food = new Food(viewModel.FoodItem);
            try
            {
                food.PurchaseDate = DateTime.Parse(AddFoodPurchaseDate.Text);
            }
            catch (Exception)
            {
                await DisplayAlert("Invalid Date", "Could not read expiration date.", "Retry");
                return;
            }
            
            try
            {
                food.ExpirationDate = DateTime.Parse(AddFoodExpirationDate.Text);
            }
            catch (Exception)
            {
                await DisplayAlert("Invalid Date", "Could not read expiration date.", "Retry");
                return;
            }
            
            var foods = App.currentUser.Foods ?? new Food[0];
            App.currentUser.Foods = new Food[foods.Length + 1];
            for (int i = 0; i < foods.Length; i++)
            {
                App.currentUser.Foods[i] = foods[i];
            }

            App.currentUser.Foods[foods.Length] = food;
            var bson = food.ToBsonDocument();
            bson.Remove("FoodItem");

            if (App.currentUser.FoodIds == null)
            {
                App.currentUser.FoodIds = new BsonArray();
            }
            
            App.currentUser.FoodIds.Insert(App.currentUser.FoodIds.Count, bson);
            
            await User.DataStore.UpdateItemAsync(App.currentUser);
            await Navigation.PopToRootAsync();
        }
    }
}