using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fridgr.Models.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Fridgr.Services
{
    public class UserDataStore : IDataStore<User>
    {
        private static readonly MongoHelper<User> MongoHelper = new MongoHelper<User>("User");

        public async Task<bool> AddItemAsync(User item)
        {
            if (MongoHelper.Collection.Find(user => user.Email == item.Email).ToListAsync().Result.Count > 0)
                return false;

            try
            {
                await MongoHelper.Collection.InsertOneAsync(item);
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Could not add user.");
            }
        }

        public async Task<bool> UpdateItemAsync(User item)
        {
            try
            {
                var updateDefinition = Builders<User>.Update;
                var update = updateDefinition.Set(user => user.FirstName, item.FirstName)
                    .Set(user => user.LastName, item.LastName)
                    .Set(user => user.Password, item.Password)
                    .Set(user => user.FoodIds, item.FoodIds);

                var filterDefinition = Builders<User>.Filter;
                var filter = filterDefinition.Eq(user => user.Id, item.Id);

                await MongoHelper.Collection.UpdateOneAsync(filter, update);

                return true;
            }
            catch (Exception)
            {
                throw new Exception("Could not update user.");
            }
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            try
            {
                await MongoHelper.Collection.DeleteOneAsync(user => user.Id == new ObjectId(id));

                return true;
            }
            catch (Exception)
            {
                throw new Exception("Could not delete user.");
            }
        }

        public async Task<User> GetItemAsync(string id)
        {
            try
            {
                return await MongoHelper.Collection.Find(user => user.Id == new ObjectId(id)).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw new Exception("Could not get user.");
            }
        }

        public async Task<IEnumerable<User>> GetMyItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetItemAsync(string email, string password)
        {
            var user = await App.UserCollection.Find(u => u.Email == email).FirstOrDefaultAsync();
            //var users = await MongoHelper.Collection.Find(u => u.Email == email).Limit(1).ToListAsync();
            
            if (user != null)
            {
                user.Foods = new Food[user.FoodIds.Count];
                for (int i = 0; i < user.FoodIds.Count; i++)
                    user.Foods[i] = new Food(user.FoodIds[i].AsBsonDocument);
            }
            
            return user;
        }

        public async Task<IEnumerable<User>> GetItemsAsync(int increment = 20, int reloads = 0,
            bool forceRefresh = false)
        {
            return null;
        }
    }
}