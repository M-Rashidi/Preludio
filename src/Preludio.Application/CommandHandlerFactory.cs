using Preludio.Application.Contracts;
using Preludio.Core;

namespace Preludio.Application
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        public CommandHandlerContainer<T> CreateHandlers<T>(T command)
        {
            if (ServiceLocator.Current.HasInstance(typeof(TransactionalCommandHandlerDecorator<T>)))
                return new CommandHandlerContainer<T>(ServiceLocator.Current.GetAllInstances<TransactionalCommandHandlerDecorator<T>>());
            else 
                return new CommandHandlerContainer<T>(ServiceLocator.Current.GetAllInstances<ICommandHandler<T>>());
        }
    }
}
