using Fridgr.Models;
using Fridgr.Models.Database;
using Fridgr.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fridgr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodItemDetailPage : ContentPage
    {
        FoodItemDetailViewModel viewModel;
        
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

            if (viewModel.FoodItem == null) return;
            
            FoodItemTitle.Text = viewModel.FoodItem.BrandName + " " + viewModel.FoodItem.FoodName;
            NutritionFacts.Text = viewModel.FoodItem.Nutrition.ToString();

        }
    }
}