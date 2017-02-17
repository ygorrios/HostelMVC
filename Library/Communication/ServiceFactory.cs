using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Library.Communication
{
    /// <summary>
    /// Creates and manages the life cicle of the WCF proxies
    /// </summary>
    public class ServiceFactory
    {
        private static Dictionary<string, string> _endpointNames;
        private static Dictionary<string, object> _listOfCreatedFactories;

        /// <summary>
        /// Initializes the dictionary used to hold endpoint configuration names
        /// </summary>
        public static void InitializeEndpoints()
        {
            if (null == _endpointNames)
                _endpointNames = new Dictionary<string, string>();
        }

        /// <summary>
        /// Adds to the config cache an endpoint name 
        /// </summary>
        /// <param name="serviceInterfaceName">The name of the interface that the endpoint will be associated</param>
        /// <param name="endpointName">Endpoint used with the provided interface</param>
        public static void AddEndpoint(string serviceInterfaceName, string endpointName)
        {
            lock (_endpointNames)
            {
                if (!_endpointNames.ContainsKey(serviceInterfaceName))
                    _endpointNames.Add(serviceInterfaceName, endpointName);
            }
        }

        /// <summary>
        /// Factories in cache used to create channels
        /// </summary>
        private static Dictionary<string, object> ListOfCreatedFactories
        {
            get { return _listOfCreatedFactories ?? (_listOfCreatedFactories = new Dictionary<string, object>()); }
        }

        /// <summary>
        /// Closes a proxy
        /// </summary>
        /// <param name="channel">Proxy instance to be closed</param>
        public static void CloseChannel(object channel)
        {
            if (channel is ICommunicationObject)
            {
                try
                {
                    ((IServiceContext)channel).DisposeContext();
                    ((ICommunicationObject)channel).Close();
                }
                catch (CommunicationException)
                {
                    ((ICommunicationObject)channel).Abort();
                }
                catch (TimeoutException)
                {
                    ((ICommunicationObject)channel).Abort();
                }
                catch (Exception)
                {
                    ((ICommunicationObject)channel).Abort();
                    throw;
                }
            }
        }

        /// <summary>
        /// Create instance of the requested service through the message requesting the service context.
        /// </summary>
        /// <typeparam name="TypeRequestedService">type of service requested</typeparam>
        /// <returns>Instance of the type of service requested</returns>
        public static TypeRequestedService CreateInstanceService<TypeRequestedService>()
            where TypeRequestedService : IServiceContext
        {
            TypeRequestedService instancia = new Ioc.Resolver().GetInstance<TypeRequestedService>();
            instancia.CreateContext(
                new List<ContextMessage>
                {                     
                    new ContextMessage(ServiceContextKey.UserLogin, System.Threading.Thread.CurrentPrincipal.Identity.Name)
                });
            return instancia;
        }

        /// <summary>
        /// Creates a service proxy for the given type
        /// </summary>
        /// <typeparam name="T">Service Type</typeparam>
        /// <returns>An opened proxy ready for use</returns>
        public static T CreateServiceChannel<T>(params ContextMessage[] messages) where T : IServiceContext
        {
            List<ContextMessage> messageList = messages.ToList();
            string key = typeof(T).Name;

            lock (ListOfCreatedFactories)
            {
                if (!ListOfCreatedFactories.ContainsKey(key))
                    ListOfCreatedFactories.Add(key, new ChannelFactory<T>(GetEndpointName(key)));
            }

            if (!messageList.Exists(p => p.Key == ServiceContextKey.UserLogin))
                messageList.Add(new ContextMessage(ServiceContextKey.UserLogin, System.Threading.Thread.CurrentPrincipal.Identity.Name));

            T channel = ((ChannelFactory<T>)ListOfCreatedFactories[key]).CreateChannel();

            ((IClientChannel)channel).Open();
            ((IServiceContext)channel).CreateContext(messageList);
            return channel;
        }

        /// <summary>
        /// Creates a service proxy for the given type
        /// </summary>
        /// <typeparam name="T">Service Type</typeparam>
        /// <returns>An opened proxy ready for use</returns>
        public static T CreateServiceChannel<T>(string endpointName)
        {
            string key = typeof(T).Name;
            lock (ListOfCreatedFactories)
            {
                if (!ListOfCreatedFactories.ContainsKey(key))
                    ListOfCreatedFactories.Add(key, new ChannelFactory<T>(GetEndpointName(key)));
            }

            T channel = ((ChannelFactory<T>)ListOfCreatedFactories[key]).CreateChannel();

            ((IClientChannel)channel).Open();
            return channel;
        }


        /// <summary>
        /// Gets an endpoint name from the cache
        /// </summary>
        /// <param name="key">Service key used to associate the endpoint name</param>
        /// <returns>Returns the endpoint name to be used</returns>
        public static string GetEndpointName(string key)
        {
            if (null == _endpointNames)
                throw new InvalidOperationException("Endpoint configuration was not initialized. Call InitializeEndpoints() method before using the factory.");
            if (!_endpointNames.ContainsKey(key))
                throw new InvalidOperationException(String.Format("The endpoint name for service interface {0} was not found. Call AddEndpoint() method first before using the factory", key));
            return _endpointNames[key];
        }
    }
}