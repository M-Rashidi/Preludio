using System.Threading.Tasks;

namespace Preludio.Application.Contracts
{
    public interface ICommandHandler<in TCommand> 
    {
        Task Handle(TCommand command);
    }

}
