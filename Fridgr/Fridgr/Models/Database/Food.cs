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

        public Food(BsonDocument document)
        {
            FoodItemId = document.GetElement("foodItem").Value.AsObjectId;
            //FoodItem = FoodItem.DataStore.GetItemAsync(FoodItemId.ToString()).Result;
            PurchaseDate = document.GetElement("purchaseDate").Value.ToUniversalTime();
            ExpirationDate = document.GetElement("expirationDate").Value.ToUniversalTime();
            Servings = document.GetElement("servings").Value.AsInt32;
        }

        public Food(FoodItem foodItem)
        {
            FoodItemId = foodItem.Id;
            FoodItem = foodItem;
            PurchaseDate = ExpirationDate = new DateTime();
            Servings = 0;
        }
    }
}