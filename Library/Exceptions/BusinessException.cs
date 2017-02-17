using System;

namespace Library.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public int Code { get; set; }

        public BusinessException(string message)
            : base(message)
        {

        }

        public BusinessException(int code, string message)
            : base(message)
        {
            this.Code = code;
        }

        public BusinessException(int code, string message, params object[] arg)
            : base(string.Format(message, arg))
        {
            this.Code = code;
        }

        public BusinessException(Enum businessException, params object[] arg)
            : base(string.Format(businessException.GetStringDescription(), arg))
        {
            this.Code = (int)businessException.GetValue();
        }
    }
}
