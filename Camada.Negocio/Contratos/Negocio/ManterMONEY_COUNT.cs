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
    public class ManterMONEY_COUNT : IManterMONEY_COUNT
    {
        Message IManterMONEY_COUNT.Salvar(Money_Count moneyCount)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {

                    if (moneyCount.ID != 0)
                        contexto.Entry(moneyCount).State = EntityState.Modified;
                    else
                        contexto.Money_Count.Add(moneyCount);

                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        Message IManterMONEY_COUNT.DeleteMoney_CountByID(int id)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {
                    var registros = (from a in contexto.Money_Count
                                     where id == a.ID
                                     select a).ToList();

                    if (registros.Count == 0)
                        throw new BusinessException("Não foi localizado registro para o ID informado");

                    foreach (var item in registros)
                        contexto.Money_Count.Remove(item);

                    contexto.SaveChanges();
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
