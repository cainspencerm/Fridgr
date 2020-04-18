using System;
using Fridgr.Models.Database;
using MongoDB.Bson;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fridgr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewFoodItemPage : ContentPage
    {
        public FoodItem FoodItem { get; set; }

        public NewFoodItemPage()
        {
            InitializeComponent();

            FoodItem = new FoodItem {Id = new ObjectId()};

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            // Require a food name and brand name.
            if (FoodItem.FoodName == null || FoodItem.BrandName == null)
            {
                await DisplayAlert("Upload Error", "Must include food name and brand name.", "Retry");
                return;
            }
            
            // Insert nutrition information.
            FoodItem.Nutrition = new Nutrition();
            if (Calories.Text != string.Empty) FoodItem.Nutrition.Calories = Convert.ToInt32(Calories.Text);
            if (TotalFat.Text != string.Empty) FoodItem.Nutrition.TotalFat = Convert.ToDouble(TotalFat.Text);
            if (SaturatedFat.Text != string.Empty) FoodItem.Nutrition.SaturatedFat = Convert.ToDouble(SaturatedFat.Text);
            if (TransFat.Text != string.Empty) FoodItem.Nutrition.TransFat = Convert.ToDouble(TransFat.Text);
            if (Cholesterol.Text != string.Empty) FoodItem.Nutrition.Cholesterol = Convert.ToDouble(Cholesterol.Text);
            if (Sodium.Text != string.Empty) FoodItem.Nutrition.Sodium = Convert.ToDouble(Sodium.Text);
            if (TotalCarbohydrate.Text != string.Empty) FoodItem.Nutrition.TotalCarbohydrate = Convert.ToDouble(TotalCarbohydrate.Text);
            if (DietaryFiber.Text != string.Empty) FoodItem.Nutrition.DietaryFiber = Convert.ToDouble(DietaryFiber.Text);
            if (TotalSugars.Text != string.Empty) FoodItem.Nutrition.TotalSugars = Convert.ToDouble(TotalSugars.Text);
            if (AddedSugars.Text != string.Empty) FoodItem.Nutrition.AddedSugars = Convert.ToDouble(AddedSugars.Text);
            if (Protein.Text != string.Empty) FoodItem.Nutrition.Protein = Convert.ToDouble(Protein.Text);
            if (VitaminD.Text != string.Empty) FoodItem.Nutrition.VitaminD = Convert.ToDouble(VitaminD.Text);
            if (Calcium.Text != string.Empty) FoodItem.Nutrition.Calcium = Convert.ToDouble(Calcium.Text);
            if (Iron.Text != string.Empty) FoodItem.Nutrition.Iron = Convert.ToDouble(Iron.Text);
            if (Potassium.Text != string.Empty) FoodItem.Nutrition.Potassium = Convert.ToDouble(Potassium.Text);
            if (ServingSize.Text != string.Empty) FoodItem.Nutrition.ServingSize = Convert.ToInt32(ServingSize.Text);
            
            MessagingCenter.Send(this, "AddItem", FoodItem);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}