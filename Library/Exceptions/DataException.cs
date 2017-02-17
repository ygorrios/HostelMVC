using System;

namespace Library.Exceptions
{
    [Serializable]
    public class DataException : Exception
    {
        public Errors Code { get; private set; }

        public DataException()
        { }

        public DataException(Errors error)
            : base(error.GetStringDescription())
        {
            this.Code = error;
        }

        public DataException(Errors error, Exception innerException)
            : base(error.GetStringDescription(), innerException)
        {
            this.Code = error;
        }
    }
}
