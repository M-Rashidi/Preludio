using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Preludio.Application.Contracts;

namespace Preludio.Application
{

    public class CommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;

        public CommandBus(ICommandHandlerFactory commandHandlerFactory)
        {
            _commandHandlerFactory =
                commandHandlerFactory ?? throw new ArgumentNullException(nameof(commandHandlerFactory));
        }

        public async Task Dispatch<T>(T command)
        {
            ValidateCommand(command);

            using (var container = _commandHandlerFactory.CreateHandlers(command))
            {
                foreach (var handler in container.Handlers)
                {
                    await handler.Handle(command);
                }
            }
        }

        private void ValidateCommand<T>(T command)
        {
            var context = new ValidationContext(command, null, null);
            var result = new List<ValidationResult>();

            if (Validator.TryValidateObject(command, context, result) != true)
            {
                throw new Exception();
            }
        }
    }
}
