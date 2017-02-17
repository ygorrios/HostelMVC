using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Negocio
{
    public interface IManterEGALI_PASSWORDS
    {
        Message Save(Dominio.Entidades.EGALI_PASSWORDS eP);

        Message DeleteById(int id);

    }
}
