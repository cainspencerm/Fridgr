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

            FoodItem = new FoodItem {Id = new ObjectId(), FoodName = "A value", BrandName = "another value", Nutrition = new Nutrition()};

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", FoodItem);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}