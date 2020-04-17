using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Fridgr.Models.Database
{
    public class FoodItem
    {
        private ObjectId Id { get; set; }
        [BsonElement("foodName")] public string FoodName { get; set; }
        [BsonElement("brandName")] public string BrandName { get; set; }
        [BsonElement("nutrition")] public Nutrition Nutrition { get; set; }

        public FoodItem(string brandName, string foodName)
        {
            try
            {
                var item = App.FoodItemCollection.Find(i => i.FoodName.Equals(foodName) && i.BrandName.Equals(brandName)).Limit(1).ToListAsync().Result.ElementAt(0);
                this.Id = item.Id;
                this.FoodName = item.FoodName;
                this.BrandName = item.BrandName;
                this.Nutrition = new Nutrition(item.Nutrition);
            }
            catch (Exception)
            {
                throw new Exception("Food item " + brandName + " " + foodName + " could not be found.");
            }

        }

        public FoodItem()
        {
            this.Id = ObjectId.Empty;
            this.FoodName = null;
            this.BrandName = null;
            this.Nutrition = null;
        }

        public static FoodItem FindFoodItem(string brandName, string foodName)
        {
            return App.FoodItemCollection.Find(i => i.FoodName.Equals(foodName) && i.BrandName.Equals(brandName)).Limit(1).ToListAsync().Result.ElementAt(0);
        }
    }

    public class Nutrition
    {
        [BsonElement("calories")] private int Calories { get; set; }
        [BsonElement("totalFat")] private double TotalFat { get; set; }
        [BsonElement("saturatedFat")] private double SaturatedFat { get; set; }
        [BsonElement("transFat")] private double TransFat { get; set; }
        [BsonElement("cholesterol")] private double Cholesterol { get; set; }
        [BsonElement("sodium")] private double Sodium { get; set; }
        [BsonElement("totalCarbohydrate")] private int TotalCarbohydrate { get; set; }
        [BsonElement("dietaryFiber")] private double DietaryFiber { get; set; }
        [BsonElement("totalSugars")] private double TotalSugars { get; set; }
        [BsonElement("addedSugars")] private double AddedSugars { get; set; }
        [BsonElement("protein")] private double Protein { get; set; }
        [BsonElement("vitaminD")] private double VitaminD { get; set; }
        [BsonElement("calcium")] private double Calcium { get; set; }
        [BsonElement("iron")] private double Iron { get; set; }
        [BsonElement("potassium")] private double Potassium { get; set; }
        [BsonElement("servingSize")] private int ServingSize { get; set; }

        public Nutrition(Nutrition nutrition)
        {
            Calories = nutrition.Calories;
            TotalFat = nutrition.TotalFat;
            SaturatedFat = nutrition.SaturatedFat;
            TransFat = nutrition.TransFat;
            Cholesterol = nutrition.Cholesterol;
            Sodium = nutrition.Sodium;
            TotalCarbohydrate = nutrition.TotalCarbohydrate;
            DietaryFiber = nutrition.DietaryFiber;
            TotalSugars = nutrition.TotalSugars;
            AddedSugars = nutrition.AddedSugars;
            Protein = nutrition.Protein;
            VitaminD = nutrition.VitaminD;
            Calcium = nutrition.Calcium;
            Iron = nutrition.Iron;
            Potassium = nutrition.Potassium;
            ServingSize = nutrition.ServingSize;
        }

        public override string ToString()
        {
            return "Calories: " + Calories + "\n" + 
                   "TotalFat: " + TotalFat + "g\n" +
                   "Saturated Fat: " + SaturatedFat + "g\n" +
                   "Trans Fat: " + TransFat + "g\n" +
                   "Cholesterol: " + Cholesterol + "mg\n" +
                   "Sodium: " + Sodium + "mg\n" +
                   "Total Carbohydrate: " + TotalCarbohydrate + "g\n" +
                   "Dietary Fiber: " + DietaryFiber + "g\n" +
                   "Total Sugars: " + TotalSugars + "g\n" +
                   "Added Sugars: " + AddedSugars + "g\n" +
                   "Protein: " + Protein + "g\n" +
                   "Vitamin D: " + VitaminD + "mcg\n" +
                   "Calcium: " + Calcium + "mg\n" +
                   "Iron: " + Iron + "mg\n" +
                   "Potassium: " + Potassium + "mg\n" +
                   "Serving Size: " + ServingSize + "g\n";
        }
    }
}