using System.Data.Common;
using System.Threading.Tasks;
using Preludio.Core;

namespace Preludio.DataAccess.EventStore
{
    //TODO: complete this unit of work
    public class FakeUnitOfWork : IUnitOfWork
    {
        public Task Begin()
        {
            return Task.CompletedTask;
        }

        public void Enlist(DbCommand command)
        {
        }

        public Task Commit()
        {
            return Task.CompletedTask;
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }
    }
}
