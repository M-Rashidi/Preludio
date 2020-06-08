using MongoDB.Driver;

namespace Preludio.DataAccess.Mongo
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
        IMongoDatabase Database { get; }
    }

}