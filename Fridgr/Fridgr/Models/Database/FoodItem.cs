using System;
using MongoDB.Bson;

namespace Fridgr.Models.Food
{
    public class FoodItem
    {
        public ObjectId Id { get; set; }
        public string foodName { get; set; }
        public string brandName { get; set; }
        public Nutrition nutrition { get; set; }
    }

    public class Nutrition
    {
        public int calories { get; set; }
        public double totalFat { get; set; }
        public double saturatedFat { get; set; }
        public double transFat { get; set; }
        public double cholesterol { get; set; }
        public double sodium { get; set; }
        public int totalCarbohydrate { get; set; }
        public double dietaryFiber { get; set; }
        public double totalSugars { get; set; }
        public double addedSugars { get; set; }
        public double protein { get; set; }
        public double vitaminD { get; set; }
        public double calcium { get; set; }
        public double iron { get; set; }
        public double potassium { get; set; }
        public int servingSize { get; set; }
    }
}