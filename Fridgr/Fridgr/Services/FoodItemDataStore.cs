using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fridgr.Models.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Fridgr.Services
{
    public class FoodItemDataStore : IDataStore<FoodItem>
    {
        private static readonly MongoHelper<FoodItem> MongoHelper = new MongoHelper<FoodItem>("FoodItems");

        public async Task<bool> AddItemAsync(FoodItem item)
        {
            if (MongoHelper.Collection.Find(i => i.Id == item.Id).ToListAsync().Result.Count > 0)
                return false;

            try
            {
                await MongoHelper.Collection.InsertOneAsync(item);
                return true;
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Could not insert food item.");
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(FoodItem item)
        {
            try
            {
                var updateDefinition = Builders<FoodItem>.Update;
                var update = updateDefinition.Set(foodItem => foodItem.Nutrition, item.Nutrition)
                    .Set(foodItem => foodItem.FoodName, item.FoodName)
                    .Set(foodItem => foodItem.BrandName, item.BrandName);

                var filterDefinition = Builders<FoodItem>.Filter;
                var filter = filterDefinition.Eq(foodItem => foodItem.Id, item.Id);

                await MongoHelper.Collection.UpdateOneAsync(filter, update);

                return true;
            }
            catch (Exception)
            {
                throw new Exception("Could not update food item.");
            }
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            try
            {
                await MongoHelper.Collection.DeleteOneAsync(foodItem => foodItem.Id == new ObjectId(id));

                return true;
            }
            catch (Exception)
            {
                throw new Exception("Could not delete food item.");
            }
        }

        public async Task<FoodItem> GetItemAsync(string id)
        {
            try
            {
                var item = await MongoHelper.Collection.Find(foodItem => foodItem.Id.ToString() == id)
                    .FirstOrDefaultAsync();
                return item;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception("Could not get food item.");
            }
        }

        public async Task<IEnumerable<FoodItem>> GetMyItemsAsync(bool forceRefresh = false)
        {
            try
            {
                var foodIds = new List<ObjectId>();
                foreach (var food in App.currentUser.Foods)
                {
                    foodIds.Insert(foodIds.Count, food.FoodItem.Id);
                }
                return await MongoHelper.Collection.Find(foodItem => foodIds.Contains(foodItem.Id)).ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Could not get food items.");
            }
        }

        public async Task<IEnumerable<FoodItem>> GetItemsAsync(int increment = 20, int reloads = 0, bool forceRefresh = false)
        {
            try
            {
                return await MongoHelper.Collection.Find(foodItem => true).Limit(increment).Skip(increment * reloads).ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Could not get food items.");
            }
        }
    }
}