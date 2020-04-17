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
        readonly List<FoodItem> FoodItems;
        public async Task<bool> AddItemAsync(FoodItem item)
        {
            if (App.FoodItemCollection.Find(i => i.Id == item.Id).ToListAsync().Result.Count > 0)
                return false;
            
            try
            {
                await App.FoodItemCollection.InsertOneAsync(item);
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

                await App.FoodItemCollection.UpdateOneAsync(filter, update);
                
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
                await App.FoodItemCollection.DeleteOneAsync(foodItem => foodItem.Id == new ObjectId(id));

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
                return await App.FoodItemCollection.Find(foodItem => foodItem.Id == new ObjectId(id))
                    .Limit(1)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw new Exception("Could not get food item.");
            }
            
        }

        public async Task<IEnumerable<FoodItem>> GetItemsAsync(int increment = 20, int reloads = 0, bool forceRefresh = false)
        {
            try
            {
                return await App.FoodItemCollection.Find(foodItem => true).Limit(increment).Skip(increment * reloads)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Could not get food items.");
            }
        }
    }
}