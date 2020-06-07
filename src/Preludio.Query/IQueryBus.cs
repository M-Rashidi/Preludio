using System.Threading.Tasks;

namespace Preludio.Query
{
    public interface IQueryBus
    {
        Task<TResult> Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}