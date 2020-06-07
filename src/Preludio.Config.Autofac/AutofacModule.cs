using Autofac;
using Preludio.Application.Contracts;
using Preludio.Query;

namespace Preludio.Config.Autofac
{
    public class AutofacModule : IPreludioIocModule
    {
        private readonly ContainerBuilder _builder;
        public AutofacModule(ContainerBuilder builder)
        {
            _builder = builder;
        }
        public void Register(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterScoped<ICommandHandlerFactory, AutofacCommandHandlerFactory>();
            serviceRegistry.RegisterScoped<IQueryHandlerFactory, AutofacQueryHandlerFactory>();
        }

        public IServiceRegistry CreateServiceRegistry()
        {
            return new AutofacServiceRegistry(_builder);
        }
    }
}
