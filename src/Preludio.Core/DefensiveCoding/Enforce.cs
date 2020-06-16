using System.Collections.Generic;

namespace Preludio.Core.DefensiveCoding
{
    public static class Enforce
    {
        public static class That
        {
            public static void CollectionHasBeenInitialized<T>(ref List<T> list)
            {
                if (list == null)
                    list = new List<T>();
            }

            public static void CollectionHasBeenInitialized<TKey, T>(ref Dictionary<TKey, T> dictioary)
            {
                if (dictioary == null)
                    dictioary = new Dictionary<TKey, T>();
            }
        }
    }
}