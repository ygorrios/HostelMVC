using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace Library.Messages
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Message))]
    public class MessageInstance<T> : Message
    {
        [DataMember]
        public T Instance
        {
            get;
            set;
        }

        /// <summary>
        /// Indica se a instância(<code>this.Instance</code>) possuí um valor. 
        /// </summary>
        public bool HasValue
        {
            get
            {
                return Instance != null && !Instance.Equals(default(T));
            }
        }

        /// <summary>
        /// Executa uma operação sobre a instância da mensagem. Caso a instância não possua valor(<code>!HasValue</code>) não faz nad.
        /// </summary>
        /// <param name="operation">Operação a ser executada sobre a instância</param>
        public void ExecuteInInstance(Action<T> operation)
        {
            if (this.HasValue && operation != null)
            {
                operation(this.Instance);
            }
        }

        /// <summary>
        /// Executa uma das operação sobre a instância da mensagem. Caso a instância possua valor(<code>!HasValue</code>) 
        /// será executada a operação (<paramref name="operationHasValue"/>) caso contrário será executada a operação (<paramref name="defaultOperation"/>).
        /// </summary>
        /// <param name="operationHasValue">Operação a ser executada sobre a instância caso a mesma possua valor.</param>
        /// <param name="defaultOperation">Operação default caso a instância não possui valor.</param>
        public void ExecuteInInstance(Action<T> operationHasValue, Action defaultOperation)
        {
            if (this.HasValue && operationHasValue != null)
            {
                operationHasValue(this.Instance);
            }
            else if (defaultOperation != null)
            {
                defaultOperation();
            }
        }

        /// <summary>
        /// Levanta uma exceção se a mensagem é de sucesso e nenhuma instância foi retornada.
        /// </summary>
        public void ThrowErrorIfInstanceIsEmpty(string error)
        {
            if (IsSuccessMessage() && Instance == null)
            {
                throw new Exception(error);
            }
        }

        /// <summary>
        /// Indica se a instancia esta vazia ou null.
        /// </summary>
        public bool IsInstanceEmptyOrNull()
        {
            return Instance == null;
        }

        /// <summary>
        /// Executa uma operação caso a instancia esteja null
        /// </summary>
        public void IsInstanceEmptyOrNull(Action<string> showFunction, string mensagem)
        {
            if (Instance == null)
                showFunction(mensagem);
        }

        /// <summary>
        /// Executa uma operação caso a instancia esteja null
        /// </summary>
        public void IsInstanceEmptyOrNull(Action<string> showFunction, Enum mensagem)
        {
            if (Instance == null)
                showFunction(mensagem.GetStringDescription());
        }

    }
}