using MongoDB.Driver;

namespace Fridgr.Services
{
    public class MongoHelper<T> where T : class
    {
        public MongoHelper(string collectionName = null)
        {
            var client =
                new MongoClient(
                    "mongodb+srv://user:BzypQCaeTwbudvWt@fridgr-bs8ph.gcp.mongodb.net/test?retryWrites=true&w=majority");
            var db = client.GetDatabase("Fridgr");

            Collection = !collectionName.Equals(string.Empty) ? db.GetCollection<T>(collectionName) : null;
        }

        public IMongoCollection<T> Collection { get; set; }
    }
}