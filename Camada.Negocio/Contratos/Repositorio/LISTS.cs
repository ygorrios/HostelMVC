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
    public class LIST : ILISTS
    {
        MessageInstance<Dominio.Entidades.LIST> ILISTS.GetTotalLastCalc()
        {
            var msg = new MessageInstance<Dominio.Entidades.LIST>();
            try
            {
                using (var contexto = new HostelEntities())
                {
                    msg.Instance = (from c in contexto.LISTs
                                    orderby c.ID descending
                                    select c).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageCollection<GridLists> ILISTS.GetAllListsByFilter(Dominio.Mensagens.Filtro.Lists filtroPesquisa)
        {
            var msg = new MessageCollection<Dominio.Mensagens.GridLists>();
            try
            {
                using (var context = new HostelEntities())
                {
                    var query = (from l in context.LISTs
                                 join d in context.DOCUMENTs on l.ID_DOCUMENT equals d.ID
                                 where filtroPesquisa.ID_LIST_TYPE == l.ID_LIST_TYPE || filtroPesquisa.ID_LIST_TYPE == 0
                                 select new Dominio.Mensagens.GridLists()
                                 {
                                     ID = l.ID,
                                     ID_LIST_TYPE = l.ID_LIST_TYPE,
                                     CHECK_IN = l.CHECK_IN,
                                     CHECK_OUT = l.CHECK_OUT,
                                     DT_START_BLOCK = l.DT_START_BLOCK,
                                     DT_START_BOOK = l.DT_START_BOOK,
                                     NOTES = l.NOTES,
                                     FIRST_NAME_GUEST = l.FIRST_NAME_GUEST,
                                     LAST_NAME_GUEST = l.LAST_NAME_GUEST,
                                     LOGLOGIN = l.LOGLOGIN,
                                     ID_DOCUMENT = l.ID_DOCUMENT,
                                     FILE_NAME = d.FILE_NAME,
                                     FILE_SIZE = d.FILE_SIZE,
                                     FILE_TYPE = d.FILE_TYPE,
                                     FILE_CONTENT = d.FILE_CONTENT,
                                     DT_INSERT = d.DT_INSERT,
                                     DT_DELETE = d.DT_DELETE
                                 });

                    if (!string.IsNullOrEmpty(filtroPesquisa.firstName))
                        query = query.Where(q => q.FIRST_NAME_GUEST.Trim().ToUpper().Contains(filtroPesquisa.firstName.Trim().ToUpper()));

                    if (!string.IsNullOrEmpty(filtroPesquisa.lastName))
                        query = query.Where(q => q.LAST_NAME_GUEST.Trim().ToUpper().Contains(filtroPesquisa.lastName.Trim().ToUpper()));

                    if (!string.IsNullOrEmpty(filtroPesquisa.lastFirstName.ToString()) && filtroPesquisa.lastFirstName.Count() > 0)
                    {
                        List<GridLists> aux = new List<GridLists>();
                        foreach (var item in filtroPesquisa.lastFirstName)
                        {
                            string lastName = item.Trim().ToUpper().Split(',')[0];
                            string firstName = item.Trim().ToUpper().Split(',')[1];
                            var sub = query.Where(q => q.LAST_NAME_GUEST.Trim().ToUpper().Contains(lastName) || q.LAST_NAME_GUEST.Trim().ToUpper().Contains(firstName));
                            if (sub != null && sub.Count() > 0)
                            {
                                foreach (var itemSub in sub)
                                    aux.Add(itemSub);
                            }
                        }
                        query = aux.AsQueryable();
                    }

                    //if (!string.IsNullOrEmpty(filtroPesquisa.numeroProcesso))
                    //    query = query.Where(q => q.numeroProcesso == filtroPesquisa.numeroProcesso);

                    //if (filtroPesquisa.Estado == 0)
                    //    query = query.Where(q => string.IsNullOrEmpty(q.numeroProcesso));
                    //else //(filtroPesquisa.Estado == 0)
                    //    query = query.Where(q => !string.IsNullOrEmpty(q.numeroProcesso));

                    //if (filtroPesquisa.TipoAtoPessoal > 0)
                    //    query = query.Where(q => q.TipoAtoPessoal == filtroPesquisa.TipoAtoPessoal.ToString());

                    //if (!string.IsNullOrEmpty(filtroPesquisa.CPF))
                    //    query = query.Where(q => q.CPF == filtroPesquisa.CPF);

                    //if (!string.IsNullOrEmpty(filtroPesquisa.dtInicio))
                    //{
                    //    DateTime dtFim = DateTime.Now;
                    //    if (string.IsNullOrEmpty(filtroPesquisa.dtFim))
                    //        dtFim = Convert.ToDateTime(filtroPesquisa.dtFim);

                    //    query = query.Where(q => (Convert.ToDateTime(q.dtCadastro) >= Convert.ToDateTime(filtroPesquisa.dtInicio) && Convert.ToDateTime(q.dtCadastro) <= dtFim));
                    //}

                    //if (filtroPesquisa.unidade > 0)
                    //    query = query.Where(q => q.unidadeGestora == filtroPesquisa.unidade);

                    //ordenação
                    if (filtroPesquisa.sSortDir_0 == "asc")
                    {
                        if (filtroPesquisa.iSortCol_0 == 1)
                            query = query.OrderByDescending(q => q.ID);
                        else if (filtroPesquisa.iSortCol_0 == 2)
                            query = query.OrderByDescending(q => q.LOGLOGIN);
                        else if (filtroPesquisa.iSortCol_0 == 3)
                            query = query.OrderByDescending(q => q.FullName);
                        else if (filtroPesquisa.iSortCol_0 == 3)
                        {
                            //TO-DO: se for tipo blacklist ou tipo verify
                            query = query.OrderByDescending(q => q.CHECK_IN);
                        }
                        else if (filtroPesquisa.iSortCol_0 == 3)
                        {

                            query = query.OrderByDescending(q => q.CHECK_OUT);
                        }
                        else
                            query = query.OrderByDescending(q => q.ID);

                    }
                    else if (filtroPesquisa.sSortDir_0 == "desc")
                    {
                        if (filtroPesquisa.iSortCol_0 == 1)
                            query = query.OrderBy(q => q.ID);
                        else if (filtroPesquisa.iSortCol_0 == 2)
                            query = query.OrderBy(q => q.LOGLOGIN);
                        else if (filtroPesquisa.iSortCol_0 == 3)
                            query = query.OrderBy(q => q.FullName);
                        else if (filtroPesquisa.iSortCol_0 == 3)
                        {

                            query = query.OrderBy(q => q.CHECK_IN);
                        }
                        else if (filtroPesquisa.iSortCol_0 == 3)
                        {

                            query = query.OrderBy(q => q.CHECK_OUT);
                        }
                        else
                            query = query.OrderBy(q => q.ID);
                    }
                    else
                        query = query.OrderBy(q => q.ID);

                    msg.Instances = query.Skip(filtroPesquisa.iDisplayStart).Take(filtroPesquisa.iDisplayLength).ToList();
                    msg.Code = query.Count();
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
