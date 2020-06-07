using System.Linq;
using System.Threading.Tasks;

namespace Preludio.Query
{
    public interface IQueryFilterService
    {
        Task<PagedResult<T>> ApplyAsync<T>(IQueryable<T> query, IQueryFilter filter);
        PagedResult<T> Apply<T>(IQueryable<T> query, IQueryFilter filter);
    }
}