using Camada.Dominio.Contratos.Repositorio;
using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camada.Dominio.Entidades;
using Camada.Dominio.Mensagens;
using Camada.Dominio.Mensagens.Filtro;
using Library.Utilities;
using Camada.Dominio;

namespace Camada.Negocio.Contratos.Repositorio
{
    public class USER : IUSER
    {
        MessageInstance<Dominio.Entidades.User> IUSER.GetUserByUsernamePassword(string username, string password)
        {
            var msg = new MessageInstance<Dominio.Entidades.User>();
            try
            {
                using (var contexto = new HostelEntities())
                {
                    msg.Instance = (from u in contexto.Users
                                    where u.Username == username
                                    && u.Password == password
                                    select u).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageInstance<Dominio.Entidades.User> IUSER.GetUserById(int ID)
        {
            var msg = new MessageInstance<Dominio.Entidades.User>();
            try
            {
                using (var contexto = new HostelEntities())
                {
                    msg.Instance = (from u in contexto.Users
                                    where u.ID == ID
                                    select u).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }
    }
}
