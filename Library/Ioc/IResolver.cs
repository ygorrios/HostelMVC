using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Ioc
{
    public interface IResolver
    {
        T GetInstance<T>();
    }
}
