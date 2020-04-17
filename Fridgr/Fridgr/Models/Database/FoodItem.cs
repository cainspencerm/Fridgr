using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Fridgr.Models.Database
{
    public class FoodItem
    {
        public ObjectId Id { get; set; }
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
        [BsonElement("calories")] public int Calories { get; set; }
        [BsonElement("totalFat")] public double TotalFat { get; set; }
        [BsonElement("saturatedFat")] public double SaturatedFat { get; set; }
        [BsonElement("transFat")] public double TransFat { get; set; }
        [BsonElement("cholesterol")] public double Cholesterol { get; set; }
        [BsonElement("sodium")] public double Sodium { get; set; }
        [BsonElement("totalCarbohydrate")] public int TotalCarbohydrate { get; set; }
        [BsonElement("dietaryFiber")] public double DietaryFiber { get; set; }
        [BsonElement("totalSugars")] public double TotalSugars { get; set; }
        [BsonElement("addedSugars")] public double AddedSugars { get; set; }
        [BsonElement("protein")] public double Protein { get; set; }
        [BsonElement("vitaminD")] public double VitaminD { get; set; }
        [BsonElement("calcium")] public double Calcium { get; set; }
        [BsonElement("iron")] public double Iron { get; set; }
        [BsonElement("potassium")] public double Potassium { get; set; }
        [BsonElement("servingSize")] public int ServingSize { get; set; }

        public Nutrition()
        {
            //Calories = 0;
            //TotalFat = 0;
            //SaturatedFat = 0;
            //TransFat = 0;
            //Cholesterol = 0;
            //Sodium = 0;
            //TotalCarbohydrate = 0;
            //DietaryFiber = 0;
            //TotalSugars = 0;
            //AddedSugars = 0;
            //Protein = 0;
            //VitaminD = 0;
            //Calcium = 0;
            //Iron = 0;
            //Potassium = 0;
            // ServingSize = 0;
        }

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
                   "Total Fat: " + TotalFat + "g\n" +
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