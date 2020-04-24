using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Fridgr.Models.Database;
using Fridgr.Services;
using Fridgr.Views;
using Xamarin.Forms;

namespace Fridgr.ViewModels
{
    public class RecipeViewModel : BaseRecipeViewModel
    {
        private int reloads;

        public RecipeViewModel()
        {
            DataStore = new RecipeDataStore();
            Title = "Browse Recipes";
            Recipes = new ObservableCollection<Recipe>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            reloads = 0;

            MessagingCenter.Subscribe<NewFoodItemPage, Recipe>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item;
                Recipes.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        public ObservableCollection<Recipe> Recipes { get; set; }
        public Command LoadItemsCommand { get; set; }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Recipes.Clear();
                var items = await DataStore.GetItemsAsync();
                foreach (var item in items)
                {
                    Recipes.Add(item);
                    Console.WriteLine(item.Title);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected async Task ReloadItemsCommand()
        {
            await DataStore.GetItemsAsync(20, reloads++);
        }
    }
}