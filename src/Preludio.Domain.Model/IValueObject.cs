namespace Preludio.Domain.Model
{
    public interface IValueObject<T> where T : class
    {
        bool SameValueAs(T valueObject);
    }
}