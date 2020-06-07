using System;
using System.Collections.Generic;

namespace Preludio.Application.Contracts
{
    public class CommandHandlerContainer<T> : IDisposable
    {
        private readonly IDisposable _scope;
        public IEnumerable<ICommandHandler<T>> Handlers { get; }
        public CommandHandlerContainer(IEnumerable<ICommandHandler<T>> handlers, IDisposable scope = null)
        {
            _scope = scope;
            Handlers = handlers;
        }
        public void Dispose()
        {
            _scope?.Dispose();
        }
    }
}
