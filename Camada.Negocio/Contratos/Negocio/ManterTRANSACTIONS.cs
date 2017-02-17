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
    public class ManterTRANSACTIONS : IManterTRANSACTIONS
    {
        Message IManterTRANSACTIONS.Salvar(Transaction transactions)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {

                    if (transactions.ID != 0)
                        contexto.Entry(transactions).State = EntityState.Modified;
                    else
                        contexto.Transactions.Add(transactions);

                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        Message IManterTRANSACTIONS.DeleteByIdTotalTransaction(int ids)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {
                    var registros = (from a in contexto.Transactions
                                     where ids == a.ID_Total_Transactions
                                     select a).ToList();

                    if (registros.Count > 0)
                    {
                        foreach (var item in registros)
                            contexto.Transactions.Remove(item);

                        contexto.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }



        Message IManterTRANSACTIONS.SaveList(params Dominio.Entidades.Transaction[] transaction)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {
                    foreach (var item in transaction)
                    {
                        if (item.ID != 0)
                            contexto.Entry(transaction).State = EntityState.Modified;
                        else
                            contexto.Transactions.Add(item);

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
    }
}
