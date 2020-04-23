using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
        public User(string firstName, string lastName, string email, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
        }

        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
