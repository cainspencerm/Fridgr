using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fridgr.Models.Database
{
    public class Food
    {
        [BsonElement("foodItem")] public ObjectId FoodItemId { get; set; }
        [BsonElement("purchaseDate")] public DateTime PurchaseDate { get; set; }
        [BsonElement("expirationDate")] public DateTime ExpirationDate { get; set; }
        [BsonElement("servings")] public int Servings { get; set; }
        
        public FoodItem FoodItem { get; set; }

        public override string ToString()
        {
            return FoodItem.FoodName;
        }
    }
}