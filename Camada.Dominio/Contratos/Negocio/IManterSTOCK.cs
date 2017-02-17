using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Negocio
{
    public interface IManterSTOCK
    {
        Message Save(Dominio.Entidades.Stock stk);

        Message DeleteStockByIDTotal_Transaction(int id);

    }
}
