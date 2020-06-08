using System.Reflection;
using MongoDB.Driver;
using Preludio.Config;
using Preludio.Core;

namespace Preludio.DataAccess.Mongo
{
    public class MongoModule : IPreludioModule
    {
        private readonly string _connectionString;
        private readonly Assembly _assembly;
        public MongoModule(string connectionString, Assembly assembly)
        {
            this._connectionString = connectionString;
            this._assembly = assembly;
        }

        public void Register(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterScoped<IMongoConnection>(x => new MongoConnection(_connectionString));
            serviceRegistry.RegisterScoped<IMongoContext, MongoContext<IMongoConnection>>();
            MongoDomainMapsRegistrator.RegisterDocumentMaps(_assembly);
            serviceRegistry.RegisterScoped<IUnitOfWork, MongoUnitOfWork>();
        }

    }
}