using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace Library.Messages
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Message))]
    public class MessageCollection<T> : Message
    {
        private List<T> _instances;

        [DataMember]
        public List<T> Instances
        {
            get { return _instances ?? (_instances = new List<T>()); }
            set { _instances = value; }
        }

        [DataMember]
        public int Count
        {
            get
            {
                if (this.Instances != null)
                    return Instances.Count;
                return 0;
            }
            set
            {
                //
            }
        }


        /// <summary>
        /// Levanta uma exceção se a mensagem é de sucesso e nenhuma instância foi retornada.
        /// </summary>
        public void ThrowErrorIfInstancesIsEmpty(string error)
        {
            if (IsSuccessMessage() && Instances.Count == 0)
            {
                throw new Exception(error);
            }
        }

        /// <summary>
        /// Executa uma acao sobre a lista de elementos retornados na mensagem.
        /// </summary>
        /// <param name="actionToExecute">Acao a ser executada sobre cada elemento</param>
        /// <returns>Referencia para o objeto(this)</returns>
        public MessageCollection<T> Do(Action<T> actionToExecute)
        {
            if (actionToExecute != null && IsSuccessMessage() && Instances.Count > 0)
            {
                foreach (var item in Instances)
                {
                    actionToExecute(item);
                }
            }
            return this;
        }
    }
}
