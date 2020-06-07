using System;

namespace Preludio.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public ExceptionCode Code { get; private set; }
        public BusinessException(ExceptionCode code)
        {
            Code = code;
        }

        protected BusinessException()
        {
            
        }
        public BusinessException(long errorCode, string errorMessage)
        {
            this.Code = new ExceptionCode(errorCode, errorMessage);
        }

        public BusinessException(Enum errorEnumValue, string errorMessage)
        {
            var value = Convert.ToInt32(errorEnumValue);
            this.Code = new ExceptionCode(value, errorMessage);
        }

        public BusinessException(string errorMessage)
        {
            this.Code = new ExceptionCode(-1,errorMessage);
        }
        public override string ToString()
        {
            if (this.Code.Code != -1)
                return $"{this.Code.Code} - {this.Code.Message}";
            return this.Code.Message;
        }
    }
}
