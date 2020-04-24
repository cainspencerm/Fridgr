using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fridgr.Models.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Fridgr.Services
{
    public class RecipeDataStore : IDataStore<Recipe>
    {
        private static readonly MongoHelper<Recipe> MongoHelper = new MongoHelper<Recipe>("Recipes");

        public async Task<bool> AddItemAsync(Recipe item)
        {
            if (MongoHelper.Collection.Find(recipe => recipe.Id == item.Id).ToListAsync().Result.Count > 0)
                return false;

            try
            {
                await MongoHelper.Collection.InsertOneAsync(item);
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Could not add recipe.");
            }
        }

        public async Task<bool> UpdateItemAsync(Recipe item)
        {
            try
            {
                var updateDefinition = Builders<Recipe>.Update;
                var update = updateDefinition.Set(recipe => recipe.AuthorId, item.AuthorId)
                    .Set(recipe => recipe.Description, item.Description)
                    .Set(recipe => recipe.Directions, item.Directions)
                    .Set(recipe => recipe.Ingredients, item.Ingredients)
                    .Set(recipe => recipe.RatingsCount, item.RatingsCount)
                    .Set(recipe => recipe.RatingsTotal, item.RatingsTotal)
                    .Set(recipe => recipe.Title, item.Title);

                var filterDefinition = Builders<Recipe>.Filter;
                var filter = filterDefinition.Eq(recipe => recipe.Id, item.Id);

                await MongoHelper.Collection.UpdateOneAsync(filter, update);

                return true;
            }
            catch (Exception)
            {
                throw new Exception("Could not update recipe.");
            }
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            try
            {
                await MongoHelper.Collection.DeleteOneAsync(recipe => recipe.Id == new ObjectId(id));

                return true;
            }
            catch (Exception)
            {
                throw new Exception("Could not delete recipe.");
            }
        }

        public async Task<Recipe> GetItemAsync(string id)
        {
            try
            {
                var item = await MongoHelper.Collection.Find(recipe => recipe.Id == new ObjectId(id))
                    .FirstOrDefaultAsync();
                item.Author = new User(item.AuthorId);
                return item;
            }
            catch (Exception)
            {
                throw new Exception("Could not get recipe.");
            }
        }

        public async Task<IEnumerable<Recipe>> GetMyItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Recipe>> GetItemsAsync(int increment = 20, int reloads = 0,
            bool forceRefresh = false)
        {
            try
            {
                return await MongoHelper.Collection.Find(recipe => true).Limit(increment).Skip(increment * reloads)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Could not get recipes.");
            }
        }
    }
}