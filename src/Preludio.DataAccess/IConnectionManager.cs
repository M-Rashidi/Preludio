using System.Data;

namespace Preludio.DataAccess
{
    public interface IConnectionManager
    {
        IDbConnection Get();
        void Override(string connectionString);
        string GetConnectionString();
    }
}
