using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Library.Communication
{
    /// <summary>
    /// Creates a instance of service proxy for the given type.
    /// </summary>
    /// <typeparam name="TProxy">Service Type</typeparam>
    public class ServiceInstance<TProxy> : IDisposable where TProxy : class, IServiceContext
    {
        private readonly TProxy _proxy;

        /// <summary>
        /// An opened proxy ready for use
        /// </summary>
        public TProxy Instance
        {
            get
            {
                if (_proxy != null)
                    return _proxy;
                throw new ObjectDisposedException("Service");
            }
        }

        public ServiceInstance()
        {
            _proxy = ServiceFactory.CreateServiceChannel<TProxy>();
        }

        public void Dispose()
        {
            ServiceFactory.CloseChannel(_proxy);
        }
    }
}
