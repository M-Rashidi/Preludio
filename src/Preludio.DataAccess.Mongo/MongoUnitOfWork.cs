using System.Threading.Tasks;
using Preludio.Core;

namespace Preludio.DataAccess.Mongo
{
    public class MongoUnitOfWork : IUnitOfWork
    {
        public Task Begin()
        {
            return Task.CompletedTask;
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