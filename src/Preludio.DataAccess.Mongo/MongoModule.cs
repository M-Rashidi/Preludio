using MongoDB.Driver;
using Preludio.Config;
using Preludio.Core;

namespace Preludio.DataAccess.Mongo
{
    public class MongoModule : IPreludioModule
    {
        private readonly string _connectionString;
        public MongoModule(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public void Register(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterScoped<IMongoConnection>(x => new MongoConnection(_connectionString));
            serviceRegistry.RegisterScoped<IMongoContext, MongoContext<IMongoConnection>>();
            MongoDomainMapsRegistrator.RegisterDocumentMaps();
            serviceRegistry.RegisterScoped<IUnitOfWork, MongoUnitOfWork>();

        }

    }
}