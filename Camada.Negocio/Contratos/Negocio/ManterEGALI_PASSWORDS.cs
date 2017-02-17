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
    public class ManterEGALI_PASSWORDS : IManterEGALI_PASSWORDS
    {
        Message IManterEGALI_PASSWORDS.Save(Dominio.Entidades.EGALI_PASSWORDS eP)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {

                    if (eP.ID != 0)
                        contexto.Entry(eP).State = EntityState.Modified;
                    else
                        contexto.EGALI_PASSWORDS.Add(eP);

                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        Message IManterEGALI_PASSWORDS.DeleteById(int id)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {
                    var registros = (from a in contexto.EGALI_PASSWORDS
                                     where id == a.ID
                                     select a).ToList();

                    if (registros.Count == 0)
                    {
                        foreach (var item in registros)
                            contexto.EGALI_PASSWORDS.Remove(item);

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
