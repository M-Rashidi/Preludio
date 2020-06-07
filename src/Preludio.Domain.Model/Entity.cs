
namespace Preludio.Domain.Model
{
    public abstract class Entity<TKey>
    {
        public TKey Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;
            return SameIdentityAs(obj as Entity<TKey>);
        }

        private bool SameIdentityAs(Entity<TKey> other)
        {
            if (other == null) return false;
            return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
