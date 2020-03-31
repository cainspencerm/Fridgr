using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace Fridgr.Models.Database
{
    public class User
    {
        public ObjectId Id { get; set; }
        [BsonElement("firstName")]
        public string firstName { get; set; }
        [BsonElement("lastName")]
        public string lastName { get; set; }
        [BsonElement("email")]
        public string email { get; set; }
        [BsonElement("password")]
        public string password { get; set; }
        [BsonElement("foods")]
        public Food[] foods { get; set; }

        public User() { }
        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public bool checkLogin()
        {
            if (this.email == null || this.password == null)
                return false;
            
            var user = App.UserCollection.Find(u => u.email == this.email).Limit(1).ToListAsync().Result;
            return user.Count > 0 && user.ElementAt(0).password == password;
            

        }
    }
}
