using Fridgr.Models.Database;

namespace Fridgr.ViewModels
{
    public class RecipeDetailViewModel : BaseRecipeViewModel
    {
        public RecipeDetailViewModel(Recipe recipe = null)
        {
            Title = recipe?.Title;
            Recipe = recipe;
        }

        public Recipe Recipe { get; set; }
    }
}