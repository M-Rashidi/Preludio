using MongoDB.Driver;

namespace Preludio.DataAccess.Mongo
{
    public class MongoContext<TConn> : IMongoContext
        where TConn : IMongoConnection
    {
        private IMongoClient _client;
        private IMongoDatabase _database;


        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        IMongoDatabase IMongoContext.Database
        {
            get { return _database; }
        }

        public MongoContext(TConn conn)
        {
            this.Create(conn);
        }

        private void Create(IMongoConnection mongoConn)
        {
            var databaseName = MongoUrl.Create(mongoConn.MongoStore()).DatabaseName;
            this._client = new MongoClient();
            this._database = this._client.GetDatabase(databaseName);
        }
    }

}