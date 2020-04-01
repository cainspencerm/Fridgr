using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fridgr.Models.Database
{
    public class Food
    {
        [BsonElement("foodItem")]
        public ObjectId foodItem { get; set; }
        [BsonElement("purchaseDate")]
        public DateTime purchaseDate { get; set; }
        [BsonElement("expirationDate")]
        public DateTime expirationDate { get; set; }
        [BsonElement("servings")]
        public int servings { get; set; }
    }
}