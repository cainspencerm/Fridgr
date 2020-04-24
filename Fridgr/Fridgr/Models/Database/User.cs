using Fridgr.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Fridgr.Models.Database
{
    public class User
    {
        public static readonly UserDataStore DataStore = new UserDataStore();

        public User(ObjectId id)
        {
            var user = DataStore.GetItemAsync(id.ToString()).Result;
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.Password = user.Password;
            this.FoodIds = user.FoodIds;

            Foods = new Food[FoodIds.Count];
            for (int i = 0; i < FoodIds.Count; i++)
            {
                Foods[i] = new Food(FoodIds[i].AsBsonDocument);
            }
        }

        public User(string firstName, string lastName, string email, string password)
        {
            Id = new ObjectId();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public ObjectId Id { get; set; }
        [BsonElement("firstName")] public string FirstName { get; set; }
        [BsonElement("lastName")] public string LastName { get; set; }
        [BsonElement("email")] public string Email { get; set; }
        [BsonElement("password")] public string Password { get; set; }
        [BsonElement("foodIds")] public BsonArray FoodIds { get; set; }
        public Food[] Foods { get; set; }
    }
}