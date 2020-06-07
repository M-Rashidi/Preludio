using System.Data.Common;
using System.Threading.Tasks;
using NHibernate;
using Preludio.Core;

namespace Preludio.DataAccess.NH
{
    public class NHUnitOfWork : IUnitOfWork
    {
        private readonly ISession session;
        public NHUnitOfWork(ISession session)
        {
            this.session = session;
        }

        public Task Begin()
        {
            this.session.Transaction.Begin();
            return Task.CompletedTask;
        }

        public async Task Commit()
        {
            await session.Transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await session.Transaction.RollbackAsync();
        }
    }
}
