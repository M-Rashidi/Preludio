namespace Preludio.Core.CompositeSpec
{
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        public CompositeSpecification<T> FirstSpecification { get; private set; }
        public CompositeSpecification<T> SecondSpecification { get; private set; }

        public AndSpecification(CompositeSpecification<T> firstSpecification, CompositeSpecification<T> secondSpecification)
        {
            this.FirstSpecification = firstSpecification;
            this.SecondSpecification = secondSpecification;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return FirstSpecification.IsSatisfiedBy(entity) && SecondSpecification.IsSatisfiedBy(entity);
        }
    }
}