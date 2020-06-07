using System.Net;
using EventStore.ClientAPI;
using Preludio.Config;
using Preludio.Core;

namespace Preludio.DataAccess.EventStore
{
    public class EventStoreModule : IPreludioModule
    {
        private readonly IPEndPoint _ipEndpoint;
        public EventStoreModule(IPEndPoint ipEndpoint)
        {
            _ipEndpoint = ipEndpoint;
        }
        public void Register(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterScoped(CreateConnection, CloseConnection);
            serviceRegistry.RegisterScoped<IUnitOfWork, FakeUnitOfWork>();
        }
        private IEventStoreConnection CreateConnection()
        {
            var connection = EventStoreConnection.Create(_ipEndpoint);
            connection.ConnectAsync().Wait();   //TODO: make it async-await
            return connection;
        }
        private void CloseConnection(IEventStoreConnection connection)
        {
            connection.Close();
        }

       
    }
}