using System;
using System.Threading.Tasks;

namespace Preludio.Application.Contracts.Transactions
{
    public interface ITransactionManager
    {
        Task DoTransactional(Func<Task> func, int timeOutInSeconds = 30);
        Task Suppress(Func<Task> func, int timeOutInSeconds = 30);
    }
}