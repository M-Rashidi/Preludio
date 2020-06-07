using System.Data.Common;
using NHibernate.Driver;
using Preludio.DataAccess.NH.Locale;

namespace Preludio.DataAccess.NH
{
    public class YekeSqlClientDriver : SqlClientDriver
    {
        public override void AdjustCommand(DbCommand command)
        {
            DbCommandExtensions.ApplyYeke(command);
            base.AdjustCommand(command);
        }
    }
}
