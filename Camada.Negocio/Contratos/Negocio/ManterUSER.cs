using Camada.Dominio.Contratos.Negocio;
using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camada.Dominio.Entidades;
using System.Data.Entity;
using Library.Exceptions;

namespace Camada.Negocio.Contratos.Negocio
{
    public class ManterUSER : IManterUSER
    {
        Message IManterUSER.Salvar(User user)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {

                    if (user.ID != 0)
                        contexto.Entry(user).State = EntityState.Modified;
                    else
                        contexto.Users.Add(user);

                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        Message IManterUSER.DeleteUSERByID(int id)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {
                    var registros = (from a in contexto.Users
                                     where id == a.ID
                                     select a).ToList();

                    if (registros.Count == 0)
                        throw new BusinessException("Não foi localizado registro para o ID informado");

                    foreach (var item in registros)
                        contexto.Users.Remove(item);

                    contexto.SaveChanges();
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
