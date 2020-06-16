using System;
using System.Threading.Tasks;
using System.Transactions;

namespace Preludio.Application.Contracts.Transactions
{
    public class TransactionManager : ITransactionManager
    {
        public async Task DoTransactional(Func<Task> func, int timeOutInSeconds = 30)
        {
            await Do(func, timeOutInSeconds, TransactionScopeOption.Required);
        }
        public async Task Suppress(Func<Task> func, int timeOutInSeconds = 30)
        {
            await Do(func, timeOutInSeconds, TransactionScopeOption.Suppress);
        }
        private async Task Do(Func<Task> func, int timeOutInSeconds, TransactionScopeOption transactionScope)
        {
            var options = new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = new TimeSpan(0, 0, timeOutInSeconds)
            };

            using (var transaction = new TransactionScope(transactionScope, options,
                TransactionScopeAsyncFlowOption.Enabled))
            {
                await func.Invoke();
                transaction.Complete();
            }
        }
    }

}