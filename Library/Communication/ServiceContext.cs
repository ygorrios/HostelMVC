using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Library.Communication
{
    public enum ServiceContextKey
    {
        UserLogin
    }

    [Serializable]
    [DataContract]
    public class ContextMessage
    {
        public ContextMessage(ServiceContextKey key, object instance)
        {
            this.Key = key;
            this.Instance = instance;
        }
        [DataMember]
        public ServiceContextKey Key { get; set; }
        [DataMember]
        public object Instance { get; set; }
    }

    public abstract class ServiceContext : IServiceContext
    {
        private static Dictionary<string, ServiceContext> _servicesOpen = new Dictionary<string, ServiceContext>();

        public bool IsCurrentRequest
        {
            get
            {
                return (this == GetCurrentRequest());
            }
        }

        protected T GetMessage<T>(ServiceContextKey key)
        {
            if (this.Messages != null && this.Messages.ContainsKey(key) && this.Messages[key] is T)
            {
                return (T)this.Messages[key];
            }
            return default(T);
        }

        private Dictionary<ServiceContextKey, object> Messages { get; set; }

        void IServiceContext.CreateContext(List<ContextMessage> messageContext)
        {
            this.Messages = new Dictionary<ServiceContextKey, object>();
            messageContext.ForEach(i => this.Messages.Add(i.Key, i.Instance));

            if (this.Messages.ContainsKey(ServiceContextKey.UserLogin))
            {
                this.UserLogin = this.Messages[ServiceContextKey.UserLogin].ToString();
                System.Security.Principal.IPrincipal principal =
                    new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(this.UserLogin), new string[0]);
                System.Threading.Thread.CurrentPrincipal = principal;

                ServiceCache.SetCache(
                    string.Format("{0}@{1}", System.ServiceModel.OperationContext.Current.SessionId,
                                  ServiceContextKey.UserLogin.GetName()), this.UserLogin);
            }

            lock (_servicesOpen)
            {
                if (System.ServiceModel.OperationContext.Current != null)
                    if (!_servicesOpen.ContainsKey(System.ServiceModel.OperationContext.Current.SessionId))
                        _servicesOpen.Add(System.ServiceModel.OperationContext.Current.SessionId, this);
            }
        }

        public static ServiceContext GetCurrentRequest()
        {
            if (System.ServiceModel.OperationContext.Current != null)
                if (_servicesOpen.ContainsKey(System.ServiceModel.OperationContext.Current.SessionId))
                    return _servicesOpen[System.ServiceModel.OperationContext.Current.SessionId];
            return null;
        }

        public string UserLogin { get; set; }

        void IServiceContext.DisposeContext()
        {
            this.Messages = null;
            lock (_servicesOpen)
            {
                if (_servicesOpen.ContainsKey(System.ServiceModel.OperationContext.Current.SessionId))
                    _servicesOpen.Remove(System.ServiceModel.OperationContext.Current.SessionId);

                ServiceCache.RemoveCacheCurrentSession();
            }
        }
    }
}  