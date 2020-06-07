using System.Collections.Generic;
using Autofac;
using Preludio.Application.Contracts;

namespace Preludio.Config.Autofac
{
    public class AutofacCommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IComponentContext _context;
        public AutofacCommandHandlerFactory(IComponentContext context)
        {
            _context = context;
        }

        public CommandHandlerContainer<T> CreateHandlers<T>(T command)
        {
            var handlers = _context.Resolve<IEnumerable<ICommandHandler<T>>>();
            return new CommandHandlerContainer<T>(handlers);
        }
    }
}
