using System.ComponentModel;
using Fridgr.Models.Database;
using Fridgr.ViewModels;
using Xamarin.Forms;

namespace Fridgr.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class FoodItemsPage : ContentPage
    {
        FoodItemsViewModel viewModel;

        public FoodItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new FoodItemsViewModel(true);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as FoodItem;
            if (item == null)
                return;

            await Navigation.PushAsync(new FoodItemDetailPage(new FoodItemDetailViewModel(item), true));

            // Manually deselect item.
            FoodItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.FoodItems.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
        
        
    }
}