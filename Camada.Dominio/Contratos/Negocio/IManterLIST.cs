using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Negocio
{
    public interface IManterLIST
    {
        Message Save(Dominio.Entidades.LIST list);

        Message DeleteLISTByID(int id);

    }
}
