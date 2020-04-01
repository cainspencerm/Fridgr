using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Fridgr.Services
{
    public class MongoHelper<T> where T : class
    {
        public IMongoCollection<T> Collection { get; private set; }
        public MongoHelper()
        {
            var client = new MongoClient("mongodb + srv://cainspencerm:<password>@fridgr-bs8ph.gcp.mongodb.net/test?retryWrites=true&w=majority");
            var db = client.GetDatabase("Fridgr");
            Collection = db.GetCollection<T>("user");
        }
    }
}
