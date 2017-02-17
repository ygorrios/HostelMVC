using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Negocio
{
    public interface IManterMONEY_COUNT
    {
        Message Salvar(Dominio.Entidades.Money_Count CALC);

        Message DeleteMoney_CountByID(int id);
    }
}
