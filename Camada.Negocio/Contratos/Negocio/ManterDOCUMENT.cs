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
    public class ManterDOCUMENT : IManterDOCUMENT
    {
        Message IManterDOCUMENT.Save(Dominio.Entidades.DOCUMENT doc)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {

                    if (doc.ID != 0)
                        contexto.Entry(doc).State = EntityState.Modified;
                    else
                        contexto.DOCUMENTs.Add(doc);

                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        Message IManterDOCUMENT.DeleteByID(int id)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {
                    var registros = (from a in contexto.DOCUMENTs
                                     where id == a.ID
                                     select a).ToList();

                    if (registros.Count == 0)
                    {
                        foreach (var item in registros)
                            contexto.DOCUMENTs.Remove(item);

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
    }
}
