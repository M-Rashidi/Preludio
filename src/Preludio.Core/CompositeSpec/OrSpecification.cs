namespace Preludio.Core.CompositeSpec
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        public CompositeSpecification<T> FirstSpecification { get; private set; }
        public CompositeSpecification<T> SecondSpecification { get; private set; }

        public OrSpecification(CompositeSpecification<T> firstSpecification, CompositeSpecification<T> secondSpecification)
        {
            this.FirstSpecification = firstSpecification;
            this.SecondSpecification = secondSpecification;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return FirstSpecification.IsSatisfiedBy(entity) || SecondSpecification.IsSatisfiedBy(entity);
        }
    }
}