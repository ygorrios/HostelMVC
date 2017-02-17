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
    public class ManterTOTAL_TRANSACTIONS : IManterTOTAL_TRANSACTIONS
    {
        Message IManterTOTAL_TRANSACTIONS.Salvar(Total_Transactions transactions)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {

                    if (transactions.ID != 0)
                        contexto.Entry(transactions).State = EntityState.Modified;
                    else
                        contexto.Total_Transactions.Add(transactions);

                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        Message IManterTOTAL_TRANSACTIONS.DeleteById(int ids)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {
                    var registros = (from a in contexto.Total_Transactions
                                     where ids == a.ID
                                     select a).ToList();

                    if (registros.Count == 0)
                        throw new BusinessException("Não foi localizado registro para o ID informado");

                    foreach (var item in registros)
                        contexto.Total_Transactions.Remove(item);

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
