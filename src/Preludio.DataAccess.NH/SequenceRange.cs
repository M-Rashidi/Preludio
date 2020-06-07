using System.Collections.Generic;

namespace Preludio.DataAccess.NH
{
    public class SequenceRange
    {
        public long FirstValue { get; private set; }
        public long LastValue { get; private set; }
        protected SequenceRange() { }
        public SequenceRange(long firstValue, long lastValue)
        {
            FirstValue = firstValue;
            LastValue = lastValue;
        }

        public List<long> GenerageValues()
        {
            var values = new List<long>();
            for (long j = FirstValue; j <= LastValue; j++)
            {
               values.Add(j);
            }
            return values;
        }
    }
}