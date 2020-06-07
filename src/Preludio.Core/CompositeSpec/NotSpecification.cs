namespace Preludio.Core.CompositeSpec
{
    public class NotSpecification<T> : CompositeSpecification<T>
    {
        public CompositeSpecification<T> Specification { get; private set; }

        public NotSpecification(CompositeSpecification<T> specification)
        {
            this.Specification = specification;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return !Specification.IsSatisfiedBy(entity);
        }
    }
}