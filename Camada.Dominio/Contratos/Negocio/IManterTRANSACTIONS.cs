using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Negocio
{
    public interface IManterTRANSACTIONS
    {
        Message Salvar(Dominio.Entidades.Transaction transactions);

        Message DeleteByIdTotalTransaction(int ids);

        Message SaveList(params Dominio.Entidades.Transaction[] transaction);

    }
}
