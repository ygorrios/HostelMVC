using System.Collections.Generic;
using System.Linq;

namespace Library.Communication
{     public static class ServiceCache
    {
        private static Dictionary<string, object> _caches = new Dictionary<string, object>();

        public static void SetCache(string key, object value)
        {
            lock (_caches)
            {
                if (_caches.ContainsKey(key))
                    _caches[key] = value;
                else
                    _caches.Add(key, value);
            }
        }

        public static object GetCache(string key)
        {
            lock (_caches)
            {
                return _caches[key];
            }
        }

        public static void RemoveCacheCurrentSession()
        {
            List<string> removeKeys = (from c in _caches
                                       where c.Key.StartsWith(System.ServiceModel.OperationContext.Current.SessionId)
                                       select c.Key).ToList();

            foreach (string k in removeKeys)
                _caches.Remove(k);
        }
    }
}





