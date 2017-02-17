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
    public class STOCK : ISTOCK
    {
        MessageInstance<Dominio.Entidades.Stock> ISTOCK.GetStockById(int id)
        {
            var msg = new MessageInstance<Dominio.Entidades.Stock>();
            try
            {
                using (var contexto = new HostelEntities())
                {
                    msg.Instance = (from a in contexto.Stocks
                                    where a.ID == id
                                    select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageCollection<GridStock> ISTOCK.GetAllStockByFilter(Dominio.Mensagens.Filtro.Stock filtroPesquisa)
        {
            var msg = new MessageCollection<Dominio.Mensagens.GridStock>();
            try
            {
                using (var context = new HostelEntities())
                {
                    ////Não pode ser feito o .ToList nesse momento, a query SQL esta esta sendo montada de acordo com os filtros
                    var query = (from s in context.Stocks
                                 join at in context.Action_Type on s.ID_ACTION_TYPE equals at.ID
                                 join pt in context.Product_Type on s.ID_PRODUCT_TYPE equals pt.ID
                                 select new Dominio.Mensagens.GridStock()
                                 {
                                     ID = s.ID,
                                     DT_Reg = s.DT_Entrada,
                                     Action_Type = at.DESCRIPTION,
                                     Description = pt.Description,
                                     LogLogin = s.LOGLOGIN,
                                     Amount = s.AMOUNT
                                 });

                    //Filtros de Pesquisa
                    //if (!string.IsNullOrEmpty(filtroPesquisa.user))
                    //    query = query.Where(q => q.LogLogin.Trim().ToUpper().Contains(filtroPesquisa.user.Trim().ToUpper()));

                    //if (filtroPesquisa.ID_Calc_Type > 0)
                    //    query = query.Where(q => q.ID_Calc_Type == filtroPesquisa.ID_Calc_Type);

                    if (!string.IsNullOrEmpty(filtroPesquisa.dtInicio))
                    {
                        DateTime dtFim = DateTime.UtcNow;
                        if (!string.IsNullOrEmpty(filtroPesquisa.dtFim))
                            dtFim = Convert.ToDateTime(filtroPesquisa.dtFim);

                        DateTime dtInicio = Convert.ToDateTime(filtroPesquisa.dtInicio);

                        query = query.Where(q => q.DT_Reg >= dtInicio && q.DT_Reg <= dtFim);
                    }

                    //ordenação
                    if (filtroPesquisa.sSortDir_0 == "asc")
                    {
                        if (filtroPesquisa.iSortCol_0 == 1)
                            query = query.OrderBy(q => q.DT_Reg);
                        else if (filtroPesquisa.iSortCol_0 == 2)
                            query = query.OrderBy(q => q.LogLogin);
                        else if (filtroPesquisa.iSortCol_0 == 3)
                            query = query.OrderBy(q => q.Action_Type);
                        else if (filtroPesquisa.iSortCol_0 == 4)
                            query = query.OrderBy(q => q.Description);
                        else if (filtroPesquisa.iSortCol_0 == 5)
                            query = query.OrderBy(q => q.Amount);
                        else
                            query = query.OrderBy(q => q.DT_Reg);
                    }
                    else if (filtroPesquisa.sSortDir_0 == "desc")
                    {
                        if (filtroPesquisa.iSortCol_0 == 1)
                            query = query.OrderByDescending(q => q.DT_Reg);
                        else if (filtroPesquisa.iSortCol_0 == 2)
                            query = query.OrderByDescending(q => q.LogLogin);
                        else if (filtroPesquisa.iSortCol_0 == 3)
                            query = query.OrderByDescending(q => q.Action_Type);
                        else if (filtroPesquisa.iSortCol_0 == 4)
                            query = query.OrderByDescending(q => q.Description);
                        else if (filtroPesquisa.iSortCol_0 == 5)
                            query = query.OrderByDescending(q => q.Amount);
                        else
                            query = query.OrderByDescending(q => q.DT_Reg);
                    }
                    else
                        query = query.OrderByDescending(q => q.DT_Reg);

                    query = query.Take(100);

                    ////Extract only current page
                    msg.Instances = query.Skip(filtroPesquisa.iDisplayStart).Take(filtroPesquisa.iDisplayLength).ToList();

                    ////Campo improvisado para gerar o total de paginacao

                    msg.Code = query.Count();//Total de registro com filtros aplicados
                    //Total de registro no banco sem filtro aplicado Deve ser feito uma pesquisa direto na Tabela outra Query Deverrá ser informado em outro campo no grid CONtroller eu coloquei fixo 110 pra teste

                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageCollection<GridStock> ISTOCK.GetAllStockControlByFilter(Dominio.Mensagens.Filtro.Stock filtroPesquisa)
        {
            var msg = new MessageCollection<Dominio.Mensagens.GridStock>();
            try
            {
                using (var context = new HostelEntities())
                {
                    var insert = (from s in context.Stocks
                                     join at in context.Action_Type on s.ID_ACTION_TYPE equals at.ID
                                     join pt in context.Product_Type on s.ID_PRODUCT_TYPE equals pt.ID
                                     where s.ID_ACTION_TYPE == 1
                                     orderby s.ID_PRODUCT_TYPE
                                     group s by new { s.ID, s.ID_PRODUCT_TYPE, s.AMOUNT } into g
                                     select new
                                     {
                                         ID_STOCK = g.Key.ID,
                                         ID_PRODUCT_TYPE = g.Key.ID_PRODUCT_TYPE,
                                         Amount = g.Sum(s => s.AMOUNT)
                                     }).ToList();

                    var sold = (from s in context.Stocks
                                     join at in context.Action_Type on s.ID_ACTION_TYPE equals at.ID
                                     join pt in context.Product_Type on s.ID_PRODUCT_TYPE equals pt.ID
                                     where s.ID_ACTION_TYPE == 2
                                     orderby s.ID_PRODUCT_TYPE
                                     group s by new { s.ID, s.ID_PRODUCT_TYPE, s.AMOUNT } into g
                                     select new
                                     {
                                         ID_STOCK = g.Key.ID,
                                         ID_PRODUCT_TYPE = g.Key.ID_PRODUCT_TYPE,
                                         Amount = g.Sum(s => s.AMOUNT)
                                     }).ToList();


                    var query = new List<Dominio.Mensagens.GridStock>();
                    foreach (var item in insert)
                    {
                        var msgGridStock = new Dominio.Mensagens.GridStock();
                        msgGridStock.Description = item.ID_PRODUCT_TYPE.ToString();
                        msgGridStock.Amount = item.Amount - sold.Where(w => w.ID_PRODUCT_TYPE == item.ID_PRODUCT_TYPE).Sum(s=>s.Amount);
                        query.Add(msgGridStock);
                    }

                    //Filtros de Pesquisa
                    //if (!string.IsNullOrEmpty(filtroPesquisa.user))
                    //    query = query.Where(q => q.LogLogin.Trim().ToUpper().Contains(filtroPesquisa.user.Trim().ToUpper()));
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
