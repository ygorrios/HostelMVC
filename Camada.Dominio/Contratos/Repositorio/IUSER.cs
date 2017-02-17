using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Repositorio
{
    public interface IUSER
    {
        MessageInstance<Dominio.Entidades.User> GetUserByUsernamePassword(string username, string password);
        MessageInstance<Dominio.Entidades.User> GetUserById(int ID);
    }
}
