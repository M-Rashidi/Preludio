using MongoDB.Driver;
using Preludio.Domain.Model;
using Humanizer;
namespace Preludio.DataAccess.Mongo.Repositories
{
    public abstract class MongoRepository<T, TKey> where T : AggregateRoot<TKey>
    {
        public IMongoCollection<T> AggregateCollection { get; private set; }
        protected MongoRepository(IMongoDatabase database)
        {
            AggregateCollection = database.GetCollection<T>(typeof(T).Name.Pluralize());
        }
    }
}