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
    public class ManterLIST : IManterLIST
    {
        Message IManterLIST.Save(Dominio.Entidades.LIST list)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {

                    if (list.ID != 0)
                        contexto.Entry(list).State = EntityState.Modified;
                    else
                        contexto.LISTs.Add(list);

                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        Message IManterLIST.DeleteLISTByID(int id)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {
                    var registros = (from a in contexto.LISTs
                                     where id == a.ID
                                     select a).ToList();

                    if (registros.Count == 0)
                        throw new BusinessException("Não foi localizado registro para o ID informado");

                    foreach (var item in registros)
                        contexto.LISTs.Remove(item);

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
