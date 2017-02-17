using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Negocio
{
    public interface IManterDOCUMENT
    {
        Message Save(Dominio.Entidades.DOCUMENT doc);

        Message DeleteByID(int id);

    }
}
