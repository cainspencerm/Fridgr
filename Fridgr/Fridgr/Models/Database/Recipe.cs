using System;
using Fridgr.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fridgr.Models.Database
{
    public class Recipe
    {
        public static readonly RecipeDataStore DataStore = new RecipeDataStore();

        public Recipe(ObjectId id)
        {
            var item = DataStore.GetItemAsync(id.ToString()).Result;
            Id = item.Id;
            AuthorId = item.AuthorId;
            Author = item.Author;
            Title = item.Title;
            Description = item.Description;
            Ingredients = item.Ingredients;
            Directions = item.Directions;
            RatingsTotal = item.RatingsTotal;
            RatingsCount = item.RatingsCount;
            CreationDate = item.CreationDate;
        }

        public ObjectId Id { get; set; }
        [BsonElement("author")] public ObjectId AuthorId { get; set; }
        public User Author { get; set; }
        [BsonElement("title")] public string Title { get; set; }
        [BsonElement("description")] public string Description { get; set; }
        [BsonElement("ingredients")] public BsonArray Ingredients { get; set; }
        [BsonElement("directions")] public string Directions { get; set; }
        [BsonElement("ratingsTotal")] public int RatingsTotal { get; set; }
        [BsonElement("ratingsCount")] public int RatingsCount { get; set; }
        [BsonElement("creationDate")] public DateTime CreationDate { get; set; }

        public override string ToString()
        {
            return Title;
        }

        public string IngredientsAsString()
        {
            var result = "";
            foreach (var item in Ingredients)
            {
                var foodItem = new FoodItem((ObjectId) item);
                result += foodItem.BrandName + " " + foodItem.FoodName + "\n";
            }

            return result;
        }
    }
}
