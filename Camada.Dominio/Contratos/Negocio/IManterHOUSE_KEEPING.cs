using Camada.Dominio.Entidades;
using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Negocio
{
    public interface IManterHOUSE_KEEPING
    {
        Message Save(List<House_Keeping> house_keeping);

        Message DeleteByDtNow(DateTime dtNow);
    }
}
