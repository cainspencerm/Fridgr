using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fridgr.Models.Database
{
    public class FoodItem
    {
        public ObjectId Id { get; }
        [BsonElement("foodName")]
        public string foodName { get; }
        [BsonElement("brandName")]
        public string brandName { get; }
        [BsonElement("nutrition")]
        public Nutrition nutrition { get; }
    }

    public class Nutrition
    {
        [BsonElement("calories")]
        public int calories { get; }
        [BsonElement("totalFat")]
        public double totalFat { get; }
        [BsonElement("saturatedFat")]
        public double saturatedFat { get; }
        [BsonElement("transFat")]
        public double transFat { get; }
        [BsonElement("cholesterol")]
        public double cholesterol { get; }
        [BsonElement("sodium")]
        public double sodium { get; }
        [BsonElement("totalCarbohydrate")]
        public int totalCarbohydrate { get; }
        [BsonElement("dietaryFiber")]
        public double dietaryFiber { get; }
        [BsonElement("totalSugars")]
        public double totalSugars { get; }
        [BsonElement("addedSugars")]
        public double addedSugars { get; }
        [BsonElement("protein")]
        public double protein { get; }
        [BsonElement("vitaminD")]
        public double vitaminD { get; }
        [BsonElement("calcium")]
        public double calcium { get; }
        [BsonElement("iron")]
        public double iron { get; }
        [BsonElement("potassium")]
        public double potassium { get; }
        [BsonElement("servingSize")]
        public int servingSize { get; }
    }
}