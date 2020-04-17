using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fridgr.Models.Database
{
    public class Food
    {
        [BsonElement("foodItem")] public ObjectId FoodItem { get; set; }
        [BsonElement("purchaseDate")] public DateTime PurchaseDate { get; set; }
        [BsonElement("expirationDate")] public DateTime ExpirationDate { get; set; }
        [BsonElement("servings")] public int Servings { get; set; }
    }
}