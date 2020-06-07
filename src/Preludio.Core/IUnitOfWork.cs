using System.Threading.Tasks;

namespace Preludio.Core
{
    public interface IUnitOfWork
    {
        Task Begin();
        Task Commit();
        Task Rollback();
    }
}
