using System.Collections.Generic;
using System.Linq;

namespace Preludio.Domain.Model
{
    public static class ValueObjectExtensions
    {
        public static void Update<T>(this IList<T> currentList, IList<T> list) where T : class, IValueObject<T> 
        {
            var added = list.Except(currentList).ToList();
            var removed = currentList.Except(list).ToList(); ;
            
            added.ForEach(currentList.Add);
            removed.ForEach(a => currentList.Remove(a));
        }
    }
}
