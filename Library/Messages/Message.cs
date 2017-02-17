using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Library.Messages
{
    [DataContract(IsReference = true)]
    public class Message
    {
        private Exception _exception;

        public enum ResultType
        {
            GeneralError = -1,
            Success = 0,
            BusinessError = 1,
            SmtpError = 2,
            DataError = 3
        }

        [DataMember]
        public ResultType Result
        {
            get;
            set;
        }

        [DataMember]
        public string Description
        {
            get;
            set;
        }

        [DataMember]
        public int Code
        {
            get;
            set;
        }

        [DataMember]
        public Exception Exception
        {
            get
            {
                return this._exception;
            }
            set
            {
                if (OperationContext.Current == null)
                {
                    _exception = value;
                }
                else
                {
                    if (value != null)
                    {
                        _exception = new Exception(value.Message, value.InnerException)
                        {
                            HelpLink = value.HelpLink,
                            Source = value.Source
                        };
                    }
                }

                if (value is Exceptions.BusinessException)
                {
                    this.Result = ResultType.BusinessError;
                    this.Description = value.Message;
                    this.Code = ((Exceptions.BusinessException)value).Code;
                    if (this.Code == 0)
                        this.Code = (int)ResultType.BusinessError;
                }
                else if (value is System.Net.Mail.SmtpException)
                {
                    this.Result = ResultType.SmtpError;
                    this.Description = value.Message;
                    this.Code = (int)((System.Net.Mail.SmtpException)value).StatusCode;
                    if (this.Code == 0)
                        this.Code = (int)ResultType.SmtpError;
                }
                else if (value is Exceptions.DataException)
                {
                    this.Result = ResultType.DataError;
                    this.Description = value.Message;
                    this.Code = (int)((Exceptions.DataException)value).Code;
                    if (this.Code == 0)
                        this.Code = (int)ResultType.DataError;
                }
                else
                {
                    if (value != null)
                    {
                        this.Result = ResultType.GeneralError;
                        this.Description = value.Message;
                        this.Code = (int)ResultType.GeneralError;
                    }
                }
                if (this.Result != ResultType.Success && this.Code == 0)
                    this.Code = -1;
            }
        }

        public Message()
        {
            this.Result = ResultType.Success;
            this.Description = "Success";
        }

        /// <summary>
        /// Indica se a mensagem é de sucesso(<code>ResultType.Success</code>).
        /// </summary>
        /// <returns></returns>
        public bool IsSuccessMessage()
        {
            return Result != null && Result == ResultType.Success;
        }

        /// <summary>
        /// Indica se a mensagem é de erro de negocio(<code>ResultType.BusinessError</code>).
        /// </summary>
        /// <returns></returns>
        public bool IsBusinessError()
        {
            return Result != null && Result == ResultType.BusinessError;
        }

        /// <summary>
        /// Levanta a exceção(<code>this.Exception</code>) se a mensagem é de erro.
        /// </summary>
        public void ThrowErrorIfNotSuccess()
        {
            if (!IsSuccessMessage())
            {
                throw this.Exception;
            }
        }

        /// <summary>
        /// Levanta uma nova exceção se a mensagem é de erro.
        /// </summary>
        public void ThrowErrorIfNotSuccess(string error)
        {
            if (!IsSuccessMessage())
            {
                throw new Exception(error);
            }
        }

        /// <summary>
        /// Apresenta o erro de negócio ou levanta uma exceção, caso throwExceptionIfNotBusinessError=true.
        /// </summary>
        public void ShowBusinessErrorIfNotSuccess(Action<string> showFunction, bool throwExceptionIfNotBusinessError)
        {
            if (IsBusinessError())
            {
                showFunction(this.Description);
            }
            else if (throwExceptionIfNotBusinessError)
            {
                ThrowErrorIfNotSuccess();
            }
        }

        /// <summary>
        /// Apresenta o erro de negócio ou levanta uma exceção.
        /// </summary>
        public void ShowBusinessErrorIfNotSuccess(Action<string> showFunction)
        {
            ShowBusinessErrorIfNotSuccess(showFunction, true);
        }

        /// <summary>
        /// Apresenta o erro da mensagem.
        /// </summary>
        public void ShowErrorIfNotSuccess(Action<string> showFunction)
        {
            if (!IsSuccessMessage())
            {
                showFunction(this.Description);
            }
        }
    }
}