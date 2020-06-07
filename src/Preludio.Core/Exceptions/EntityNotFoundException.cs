namespace Preludio.Core.Exceptions
{
    public class EntityNotFoundException : BusinessException
    {
        public EntityNotFoundException(string entityName, long id) : base(new ExceptionCode(-1, $"Entity '{entityName}' not found with Id : {id}"))
        {
        }

        public EntityNotFoundException(long id) : base(new ExceptionCode(-1, $"Entity not found with Id : {id}"))

        {

        }
    }
}
