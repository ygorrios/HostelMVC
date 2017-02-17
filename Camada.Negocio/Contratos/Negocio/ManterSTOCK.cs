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
    public class ManterSTOCK : IManterSTOCK
    {
        Message IManterSTOCK.Save(Dominio.Entidades.Stock stk)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {

                    if (stk.ID != 0)
                        contexto.Entry(stk).State = EntityState.Modified;
                    else
                        contexto.Stocks.Add(stk);

                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        Message IManterSTOCK.DeleteStockByIDTotal_Transaction(int id)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {
                    var registros = (from a in contexto.Stocks
                                     where id == a.ID_TOTAL_TRANSACTION
                                     select a).ToList();

                    if (registros.Count > 0)
                    {
                        foreach (var item in registros)
                            contexto.Stocks.Remove(item);
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



        //Message IManterCALC.Salvar(params Dominio.Entidades.Calc[] CALC)
        //{
        //    Message msg = new Message();
        //    try
        //    {
        //        using (var contexto = new Dominio.Entidades.HostelEntities())
        //        {
        //            foreach (var item in CALC)
        //            {
        //                if (item.ID != 0)
        //                    contexto.Entry(CALC).State = EntityState.Modified;
        //                else
        //                    contexto.Calc.Add(item);

        //            }
        //            contexto.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg.Exception = ex;
        //    }
        //    return msg;
        //}
    }
}
