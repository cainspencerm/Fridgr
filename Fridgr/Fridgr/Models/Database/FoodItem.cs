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

        public FoodItem(ObjectId id)
        {
            var item = App.FoodItemCollection.Find(i => i.Id.Equals(id)).Limit(1).FirstOrDefaultAsync().Result;
            this.Id = item.Id;
            this.BrandName = item.BrandName;
            this.FoodName = item.FoodName;
            this.Nutrition = new Nutrition(item.Nutrition);
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
        [BsonElement("totalCarbohydrate")] public double TotalCarbohydrate { get; set; }
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
            Calories = -1;
            TotalFat = -1;
            SaturatedFat = -1;
            TransFat = -1;
            Cholesterol = -1;
            Sodium = -1;
            TotalCarbohydrate = -1;
            DietaryFiber = -1;
            TotalSugars = -1;
            AddedSugars = -1;
            Protein = -1;
            VitaminD = -1;
            Calcium = -1;
            Iron = -1;
            Potassium = -1;
            ServingSize = -1;
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
            string result = "";
            if (Calories != -1) result += "Calories: " + Calories + "\n";
            if (TotalFat != -1) result += "Total Fat: " + TotalFat + "g\n";
            if (SaturatedFat != -1) result += "Saturated Fat: " + SaturatedFat + "g\n";
            if (TransFat != -1) result += "Trans Fat: " + TransFat + "g\n";
            if (Cholesterol != -1) result += "Cholesterol: " + Cholesterol + "mg\n";
            if (Sodium != -1) result += "Sodium: " + Sodium + "mg\n";
            if (TotalCarbohydrate != -1) result += "Total Carbohydrate: " + TotalCarbohydrate + "g\n";
            if (DietaryFiber != -1) result += "Dietary Fiber: " + DietaryFiber + "g\n";
            if (TotalSugars != -1) result += "Total Sugars: " + TotalSugars + "g\n";
            if (AddedSugars != -1) result += "Added Sugars: " + AddedSugars + "g\n";
            if (Protein != -1) result += "Protein: " + Protein + "g\n";
            if (VitaminD != -1) result += "Vitamin D: " + VitaminD + "mcg\n";
            if (Calcium != -1) result += "Calcium: " + Calcium + "mg\n";
            if (Iron != -1) result += "Iron: " + Iron + "mg\n";
            if (Potassium != -1) result += "Potassium: " + Potassium + "mg\n";
            if (ServingSize != -1) result += "Serving Size: " + ServingSize + "g\n";
            return result;
        }
    }
}