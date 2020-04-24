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
    public class FoodItemsViewModel : BaseFoodViewModel
    {
        public ObservableCollection<FoodItem> FoodItems { get; set; }
        public Command LoadItemsCommand { get; set; }

        public FoodItemsViewModel(bool allItems)
        {
            DataStore = new FoodItemDataStore();
            FoodItems = new ObservableCollection<FoodItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(allItems));

            MessagingCenter.Subscribe<NewFoodItemPage, FoodItem>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as FoodItem;
                FoodItems.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        private async Task ExecuteLoadItemsCommand(bool allItems)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                FoodItems.Clear();
                var items = allItems ? await DataStore.GetItemsAsync() : await DataStore.GetMyItemsAsync();
                foreach (var item in items)
                {
                    FoodItems.Add(item);
                    Console.WriteLine(item.FoodName + " " + item.BrandName);
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

        protected async Task ReloadItemsCommand(int reloads)
        {
            await DataStore.GetItemsAsync(20, reloads);
        }
    }
}