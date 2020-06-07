using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Preludio.Core;

namespace Preludio.Domain.Model
{
    public abstract class ValueObject<T> : IValueObject<T> where T : class
    {
        public virtual bool SameValueAs(T valueObject)
        {
            return EqualsBuilder.ReflectionEquals(this, valueObject);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return SameValueAs(obj as T);
        }

        public override int GetHashCode()
        {
            return HashCodeBuilder.ReflectionHashCode(this);
        }

    }
}
