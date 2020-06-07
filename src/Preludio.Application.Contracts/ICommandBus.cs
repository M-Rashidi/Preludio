using System.Threading.Tasks;

namespace Preludio.Application.Contracts
{
    public interface ICommandBus
    {
        Task Dispatch<T>(T command);
    }
}
