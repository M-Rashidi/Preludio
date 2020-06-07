namespace Preludio.Domain.Model
{
    public abstract class Id<T> : ValueObject<Id<T>>
    {
        public virtual T DbId { get; private set; }

        protected Id()
        {
        }

        protected Id(T dbId)
        {
            DbId = dbId;
        }
    }
}
