using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace Library.Ioc
{
    public class Resolver : IResolver
    {
        private CompositionContainer _container;
        private AggregateCatalog _catalog;

        public Resolver()
        {
            string path = System.AppDomain.CurrentDomain.RelativeSearchPath;

            if (string.IsNullOrEmpty(path))
            {
                path = System.AppDomain.CurrentDomain.BaseDirectory;
            }
            _catalog = new AggregateCatalog();
            _catalog.Catalogs.Add(new DirectoryCatalog(path));
            _container = new CompositionContainer(_catalog);
        }
        
        #region IResolver<T> Members

        public T GetInstance<T>()
        {
            try
            {
                return _container.GetExportedValue<T>();
            }
            catch (ReflectionTypeLoadException tLException)
            {
                var loaderMessages = new StringBuilder();
                loaderMessages.AppendLine("While trying to load composable parts the following loader exceptions were found: ");
                foreach (var loaderException in tLException.LoaderExceptions)
                    loaderMessages.AppendLine(loaderException.Message);


                throw new Exception(loaderMessages.ToString(), tLException);
            }
        }

        #endregion
    }
}
