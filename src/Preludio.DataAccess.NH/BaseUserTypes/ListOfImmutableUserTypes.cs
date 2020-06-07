using System;
using System.Collections.Generic;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Preludio.DataAccess.NH.BaseUserTypes
{
    public abstract class ListOfImmutableUserTypes<T> : IUserType
    {
//        public abstract object NullSafeGet(IDataReader rs, string[] names, object owner);
//        public abstract void NullSafeSet(IDbCommand cmd, object value, int index);
        public abstract SqlType[] SqlTypes { get; }

        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public abstract object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner);
        

        public abstract void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session);

        public object DeepCopy(object value)
        {
            var list = value as List<T>;
            if (list == null)return new List<T>();

            var output = new List<T>();
            output.AddRange(list);
            return output;
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }

        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }

        public Type ReturnedType => typeof(T);
        public bool IsMutable => false;
    }
}
