using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Negocio
{
    public interface IManterTOTAL_TRANSACTIONS
    {
        Message Salvar(Dominio.Entidades.Total_Transactions transactions);

        Message DeleteById(int ids);
    }
}
