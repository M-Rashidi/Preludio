namespace Preludio.Core.CompositeSpec
{
    public interface ISpecification<in T>
    {
        bool IsSatisfiedBy(T entity);
    }
}
