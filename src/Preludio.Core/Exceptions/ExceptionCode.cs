namespace Preludio.Core.Exceptions
{
    public class ExceptionCode
    {
        public long Code { get; private set; }
        public string Message { get; private set; }
        public ExceptionCode(long code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
