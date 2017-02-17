using Camada.Dominio.Contratos.Repositorio;
using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camada.Dominio.Entidades;
using Camada.Dominio.Mensagens;
using Camada.Dominio.Mensagens.Filtro;
using Library.Utilities;
using Camada.Dominio;

namespace Camada.Negocio.Contratos.Repositorio
{
    public class EGALI_PASSWORDS : IEGALI_PASSWORDS
    {
        MessageInstance<Dominio.Mensagens.TelaPasswords> IEGALI_PASSWORDS.GetEgaliPasswordsByIdEgaliPasswordsHistory(int ID)
        {
            var msg = new MessageInstance<Dominio.Mensagens.TelaPasswords>();
            try
            {
                using (var context = new HostelEntities())
                {
                    msg.Instance = (from ep in context.EGALI_PASSWORDS
                                    join eph in context.EGALI_PASSWORDS_HISTORY on ep.ID equals eph.ID_EGALI_PASSWORDS
                                    where eph.ID == ID
                                    select new Dominio.Mensagens.TelaPasswords()
                                    {
                                        ID = ep.ID,
                                        ID_EGALI_PASSWORDS = eph.ID_EGALI_PASSWORDS,
                                        Item = ep.Item,
                                        Login = ep.Login,
                                        Password = eph.Password
                                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageCollection<GridEgaliPasswords> IEGALI_PASSWORDS.GetAllEgaliPasswordsByFilter(EgaliPasswords filtroPesquisa)
        {
            var msg = new MessageCollection<Dominio.Mensagens.GridEgaliPasswords>();
            try
            {
                using (var context = new HostelEntities())
                {
                    var subquery = from eph in context.EGALI_PASSWORDS_HISTORY
                                   group eph by eph.ID_EGALI_PASSWORDS into g
                                   select new
                                   {
                                       ID_EGALI_PASSWORDS = g.Key,
                                       DT_Reg = g.Max(a => a.DT_Reg)
                                   };

                    var query = from ep in context.EGALI_PASSWORDS
                                join eph in context.EGALI_PASSWORDS_HISTORY on ep.ID equals eph.ID_EGALI_PASSWORDS
                                join s in subquery on eph.DT_Reg equals s.DT_Reg
                                where !ep.DT_Delete.HasValue
                                select new Dominio.Mensagens.GridEgaliPasswords()
                                {
                                    ID = eph.ID,
                                    DT_Reg = eph.DT_Reg,
                                    LogLogin = eph.LogLogin,
                                    Item = ep.Item,
                                    Login = ep.Login,
                                    Password = eph.Password
                                };

                    if (!string.IsNullOrEmpty(filtroPesquisa.item))
                        query = query.Where(q => q.Item.Contains(filtroPesquisa.item));
                    if (!string.IsNullOrEmpty(filtroPesquisa.login))
                        query = query.Where(q => q.Login.Contains(filtroPesquisa.login));

                    query = query.OrderByDescending(q => q.DT_Reg);
                    msg.Code = query.Count();
                    msg.Instances = query.ToList();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageCollection<GridEgaliPasswords> IEGALI_PASSWORDS.GetEgaliPasswordsByFilter(EgaliPasswords filtroPesquisa)
        {
            var msg = new MessageCollection<Dominio.Mensagens.GridEgaliPasswords>();
            try
            {
                using (var context = new HostelEntities())
                {
                    int idEgaliPasswords = (from eph in context.EGALI_PASSWORDS_HISTORY where eph.ID == filtroPesquisa.ID select eph.ID_EGALI_PASSWORDS).FirstOrDefault();


                    var query = (from ep in context.EGALI_PASSWORDS
                                 join eph in context.EGALI_PASSWORDS_HISTORY on ep.ID equals eph.ID_EGALI_PASSWORDS
                                 where eph.ID_EGALI_PASSWORDS == idEgaliPasswords
                                 select new Dominio.Mensagens.GridEgaliPasswords()
                                 {
                                     ID = ep.ID,
                                     DT_Reg = eph.DT_Reg,
                                     LogLogin = eph.LogLogin,
                                     Item = ep.Item,
                                     Login = ep.Login,
                                     Password = eph.Password
                                 });

                    query = query.OrderByDescending(q => q.DT_Reg);
                    msg.Code = query.Count();
                    msg.Instances = query.ToList();
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
