using Fridgr.Models.Database;

namespace Fridgr.ViewModels
{
    public class FoodItemDetailViewModel : BaseViewModel
    {
        public FoodItem FoodItem { get; set; }

        public FoodItemDetailViewModel(FoodItem foodItem = null)
        {
            Title = foodItem?.BrandName + foodItem?.FoodName;
            FoodItem = foodItem;
        }
    }
}
