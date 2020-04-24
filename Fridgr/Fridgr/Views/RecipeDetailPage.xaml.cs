using System;
using System.Globalization;
using Fridgr.Models;
using Fridgr.ViewModels;
using MongoDB.Bson;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fridgr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailPage : ContentPage
    {
        private readonly RecipeDetailViewModel viewModel;

        public RecipeDetailPage()
        {
            InitializeComponent();
        }

        public RecipeDetailPage(RecipeDetailViewModel model)
        {
            InitializeComponent();

            BindingContext = viewModel = model;

            Init();
        }

        private void Init()
        {
            BackgroundColor = Constants.background;

            if (viewModel.Recipe.Id == ObjectId.Empty) return;

            Title.Text = viewModel.Recipe.Title;
            Author.Text = viewModel.Recipe.Author?.FirstName + " " + viewModel.Recipe.Author?.LastName;

            Double rating = (double) viewModel.Recipe.RatingsTotal / (double) viewModel.Recipe.RatingsCount;
            Rating.Text = "Rating: " + rating.ToString("F1") + " out of 5";
            
            Date.Text = viewModel.Recipe.CreationDate.ToString(CultureInfo.CurrentCulture);
            Description.Text = "Description: " + viewModel.Recipe.Description;
            Ingredients.Text = "Ingredients: " + viewModel.Recipe.IngredientsAsString();
            Directions.Text = "Directions: " + viewModel.Recipe.Directions;
        }
    }
}