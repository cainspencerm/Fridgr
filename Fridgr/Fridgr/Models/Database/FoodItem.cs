using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Fridgr.Models.Database
{
    public class FoodItem
    {
        public FoodItem(string brandName, string foodName)
        {
            try
            {
                var item = App.FoodItemCollection
                    .Find(i => i.FoodName.Equals(foodName) && i.BrandName.Equals(brandName)).Limit(1).ToListAsync()
                    .Result.ElementAt(0);
                Id = item.Id;
                FoodName = item.FoodName;
                BrandName = item.BrandName;
                Nutrition = new Nutrition(item.Nutrition);
                FoodTypeId = item.FoodTypeId;
                FoodType = (FoodType) item.FoodTypeId;
            }
            catch (Exception)
            {
                throw new Exception("Food item " + brandName + " " + foodName + " could not be found.");
            }
        }

        public FoodItem(ObjectId id)
        {
            var item = App.FoodItemCollection.Find(i => i.Id.Equals(id)).Limit(1).FirstOrDefaultAsync().Result;
            Id = item.Id;
            BrandName = item.BrandName;
            FoodName = item.FoodName;
            Nutrition = new Nutrition(item.Nutrition);
            FoodTypeId = item.FoodTypeId;
            FoodType = (FoodType) item.FoodTypeId;
        }

        public FoodItem()
        {
            Id = ObjectId.Empty;
            FoodName = null;
            BrandName = null;
            Nutrition = null;
            FoodTypeId = -1;
            FoodType = FoodType.Null;
        }

        public ObjectId Id { get; set; }
        [BsonElement("foodName")] public string FoodName { get; set; }
        [BsonElement("brandName")] public string BrandName { get; set; }
        [BsonElement("nutrition")] public Nutrition Nutrition { get; set; }
        [BsonElement("foodType")] public int FoodTypeId { get; set; }
        public FoodType FoodType { get; set; }

        public static FoodItem FindFoodItem(string brandName, string foodName)
        {
            return App.FoodItemCollection.Find(i => i.FoodName.Equals(foodName) && i.BrandName.Equals(brandName))
                .Limit(1).ToListAsync().Result.ElementAt(0);
        }
    }
}