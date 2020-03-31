using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace Fridgr.Models.Database
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Food.Food[] foods { get; set; }

        public User() { }
        public User(string firstName, string lastName, string email, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
        }

        //public bool CheckLogin(string email, string password)
        //{
        //    if (email != null && password != null)
        //    {
        //        var user = App.users.Find(u => u.email == email).Limit(1).ToListAsync().Result;
        //        return user.ElementAt(0).password == password;
        //    }
        //    else return false;

        //    //var user = App.users.Find(u => u.email == email).Limit(1).ToListAsync().Result;
        //    //return user.ElementAt(0).password == password;

        //}
    }
}
