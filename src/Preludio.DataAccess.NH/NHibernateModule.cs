using System.Data.Common;
using NHibernate;
using Preludio.Config;
using Preludio.Core;

namespace Preludio.DataAccess.NH
{
    public class NHibernateModule : IPreludioModule
    {
        private readonly ISessionFactory _factory;
        private string connectionString;
        public NHibernateModule(SessionFactoryBuilder builder)
        {
            connectionString = builder.ConnectionString;
            _factory = builder.Build();
        }
        public void Register(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterScoped(CreateConnection);
            serviceRegistry.RegisterScoped(CreateSession, a=> a.Close());
            serviceRegistry.RegisterScoped<IUnitOfWork,NHUnitOfWork>();
            if(InterceptorContainer.IsInterceptorRegistered())
                serviceRegistry.RegisterScoped(InterceptorContainer.InterceptorType);

        }

        private ISession CreateSession(IDependencyResolver dependencyResolver)
        {
            var connectionManager = dependencyResolver.Resolve<IConnectionManager>();
            var connection = connectionManager.Get();

            var builder = _factory.WithOptions();
            if (InterceptorContainer.IsInterceptorRegistered())
            {
                var interceptor = dependencyResolver.Resolve(InterceptorContainer.InterceptorType);
                builder.Interceptor(interceptor as IInterceptor);
            }
            return builder.Connection(connection as DbConnection).OpenSession();
        }

        private IConnectionManager CreateConnection()
        {
            return new SqlConnectionManager(connectionString);
        }
    }
}
