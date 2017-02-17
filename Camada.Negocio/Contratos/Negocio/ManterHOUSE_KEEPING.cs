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
    public class ManterHOUSE_KEEPING : IManterHOUSE_KEEPING
    {

        Message IManterHOUSE_KEEPING.Save(List<House_Keeping> house_keeping)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {
                    foreach (var item in house_keeping)
                    {
                        if (item.ID != 0)
                            contexto.Entry(house_keeping).State = EntityState.Modified;
                        else
                            contexto.House_Keeping.Add(item);
                    }
                    
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        Message IManterHOUSE_KEEPING.DeleteByDtNow(DateTime dtNow)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {
                    //var registros = (from a in contexto.Transactions
                    //                 where ids == a.ID_Total_Transactions
                    //                 select a).ToList();

                    //if (registros.Count == 0)
                    //    throw new BusinessException("Não foi localizado registro para o ID informado");

                    //foreach (var item in registros)
                    //    contexto.Transactions.Remove(item);

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
