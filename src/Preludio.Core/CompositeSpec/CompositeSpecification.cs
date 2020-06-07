namespace Preludio.Core.CompositeSpec
{
    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T entity);

        public CompositeSpecification<T> And(CompositeSpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public CompositeSpecification<T> Or(CompositeSpecification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }

        public CompositeSpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }
}