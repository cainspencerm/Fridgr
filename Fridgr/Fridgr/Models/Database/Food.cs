using System;

namespace Fridgr.Models.Food
{
    public class Food
    {
        public FoodItem foodItem { get; set; }
        public DateTime purchaseDate { get; set; }
        public DateTime expirationDate { get; set; }
        public int servings { get; set; }
    }
}