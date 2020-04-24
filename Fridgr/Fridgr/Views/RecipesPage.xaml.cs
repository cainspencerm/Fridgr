using System;
using System.Threading.Tasks;
using Fridgr.Models.Database;
using Fridgr.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fridgr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipesPage : ContentPage
    {

        private RecipeViewModel viewModel;
        public RecipesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new RecipeViewModel();

            Init();
            
            ItemsListView.ItemsSource = viewModel.Recipes;

            
        }

        public async Task<bool> Init()
        {
            try
            {
                var recipesList = await Recipe.DataStore.GetItemsAsync();

                foreach (var recipe in recipesList) viewModel.Recipes.Add(recipe);

                return true;
            }
            catch (Exception)
            {
                throw new Exception("Could not load recipes");
            }
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                // TODO Determine why this breaks the page.
                var recipe = viewModel.Recipes[e.SelectedItemIndex];
                await Navigation.PushAsync(new RecipeDetailPage(new RecipeDetailViewModel(recipe)));
                ((ListView) sender).SelectedItem = null;
            }
            else
            {
                Console.WriteLine("Why is this getting called again?");
            }
        }


        private void AddItem_Clicked(object sender, EventArgs e)
        {
            // TODO Add the following to xaml file
            // <ContentPage.ToolbarItems>
            // <ToolbarItem Text="Add" Clicked="AddItem_Clicked" />
            // </ContentPage.ToolbarItems>
            
            throw new NotImplementedException();
        }
    }
}