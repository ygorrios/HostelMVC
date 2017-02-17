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
    public class ManterCALC : IManterCALC
    {
        Message IManterCALC.Salvar(Dominio.Entidades.Calc CALC)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {

                    if (CALC.ID != 0)
                        contexto.Entry(CALC).State = EntityState.Modified;
                    else
                        contexto.Calcs.Add(CALC);

                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        Message IManterCALC.DeleteCalcByIDMoney_Count(int id)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {
                    var registros = (from a in contexto.Calcs
                                     where id == a.ID_Money_Count
                                     select a).ToList();

                    if (registros.Count == 0)
                        throw new BusinessException("Não foi localizado registro para o ID informado");

                    foreach (var item in registros)
                        contexto.Calcs.Remove(item);

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
