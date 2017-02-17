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
    public class ManterEGALI_PASSWORDS_HISTORY : IManterEGALI_PASSWORDS_HISTORY
    {
        Message IManterEGALI_PASSWORDS_HISTORY.Save(Dominio.Entidades.EGALI_PASSWORDS_HISTORY eP)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {

                    if (eP.ID != 0)
                        contexto.Entry(eP).State = EntityState.Modified;
                    else
                        contexto.EGALI_PASSWORDS_HISTORY.Add(eP);

                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        Message IManterEGALI_PASSWORDS_HISTORY.DeleteByIdEgaliPasswords(int id)
        {
            Message msg = new Message();
            try
            {
                using (var contexto = new Dominio.Entidades.HostelEntities())
                {
                    var registros = (from a in contexto.EGALI_PASSWORDS_HISTORY
                                     where id == a.ID_EGALI_PASSWORDS
                                     select a).ToList();

                    if (registros.Count == 0)
                    {
                        foreach (var item in registros)
                            contexto.EGALI_PASSWORDS_HISTORY.Remove(item);

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
