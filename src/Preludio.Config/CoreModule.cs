using Preludio.Application;
using Preludio.Application.Contracts;
using Preludio.Core.EventHandling;
using Preludio.Domain.Model;
using Preludio.Query;

namespace Preludio.Config
{
    public class CoreModule : IPreludioModule
    {
        public void Register(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterScoped<IQueryBus, QueryBus>();
            serviceRegistry.RegisterScoped<ICommandBus, CommandBus>();
            serviceRegistry.RegisterScoped<IEventAggregator, EventAggregator>();
            serviceRegistry.RegisterScoped<IEventListener, EventListener>();
            serviceRegistry.RegisterScoped<IEventPublisher, EventPublisher>();
            serviceRegistry.RegisterScoped<IAggregateRootConfigurator, AggregateRootConfigurator>();
            serviceRegistry.RegisterDecorator(typeof(ICommandHandler<>),typeof(TransactionalCommandHandlerDecorator<>));
        }
    }
}
