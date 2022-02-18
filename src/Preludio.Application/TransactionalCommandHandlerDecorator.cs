using System;
using System.Threading.Tasks;
using Preludio.Application.Contracts;
using Preludio.Core;

namespace Preludio.Application
{
    public class TransactionalCommandHandlerDecorator<T> : ICommandHandler<T>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandHandler<T> _commandHandler;

        public TransactionalCommandHandlerDecorator(ICommandHandler<T> commandHandler, 
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            this._commandHandler = commandHandler;
        }

        public async Task Handle(T command)
        {
            try
            {
                await _unitOfWork.Begin();
                await _commandHandler.Handle(command);
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                try
                {
                    await _unitOfWork.Rollback();
                }
                catch (Exception) {}
                throw;
            }
        }

        private void AddCommandLog<T>(T command) 
        {
          
        }
    }
}
