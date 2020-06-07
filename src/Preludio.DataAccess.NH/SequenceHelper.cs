using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Transform;

namespace Preludio.DataAccess.NH
{
    public class SequenceHelper
    {
        private ISession session;
        public SequenceHelper(ISession session)
        {
            this.session = session;
        }

        public async Task<long> Next(string sequenceName)
        {
            return await session.CreateSQLQuery("SELECT NEXT VALUE FOR " + sequenceName).UniqueResultAsync<long>();
        }

        public async Task<List<long>> Range(string sequenceName, long rangeSize)
        {
            var range =  await session.CreateSQLQuery($"DECLARE @firstValue SQL_VARIANT, @lastValue SQL_VARIANT; EXEC sp_sequence_get_range @sequence_name ='{sequenceName}', @range_size = {rangeSize}, @range_first_value = @firstValue OUTPUT, @range_last_value  = @lastValue OUTPUT; SELECT FirstValue = CONVERT(bigint, @firstValue), LastValue = CONVERT(bigint, @lastValue)")
                .SetResultTransformer(Transformers.AliasToBean<SequenceRange>())
                .ListAsync<SequenceRange>();

            return range.First().GenerageValues();
        }
    }
}
