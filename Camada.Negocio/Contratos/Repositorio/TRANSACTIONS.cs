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
    public class TRANSACTIONS : ITRANSACTIONS
    {
        MessageInstance<Dominio.Entidades.Money_Count> ITRANSACTIONS.GetMoney_CountById(int id)
        {
            var msg = new MessageInstance<Dominio.Entidades.Money_Count>();
            try
            {
                using (var contexto = new HostelEntities())
                {
                    msg.Instance = (from a in contexto.Money_Count
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

        MessageCollection<GridTransactions> ITRANSACTIONS.GetAllTransactionsByID_Total_Transactions(Dominio.Mensagens.Filtro.Transactions filtroPesquisa)
        {
            var msg = new MessageCollection<Dominio.Mensagens.GridTransactions>();
            try
            {
                using (var context = new HostelEntities())
                {
                    ////Não pode ser feito o .ToList nesse momento, a query SQL esta esta sendo montada de acordo com os filtros
                    var query = (from c in context.Transactions
                                 join transType in context.Transaction_Type on c.ID_Transaction_Type equals transType.ID
                                 join pt in context.Payment_Type on c.ID_Payment_Type equals pt.ID
                                 join tt in context.Total_Transactions on c.ID_Total_Transactions equals tt.ID
                                 join ct in context.Card_Type on c.ID_Card_Type equals ct.ID into ptLeft
                                 from subCT in ptLeft.DefaultIfEmpty()
                                 where c.ID_Total_Transactions == filtroPesquisa.ID_TOTAL_TRANSACTION
                                 select new Dominio.Mensagens.GridTransactions()
                                 {
                                    ID = c.ID,
                                    Date = c.DT_Reg.ToString(),
                                    User = c.LogLogin,
                                    Reservation_Number = c.Reservation_Number, 
                                    Guest_Name = c.GuestName,
                                    Transaction_Type = transType.Description,
                                    Card_Type = (subCT != null && !string.IsNullOrEmpty(subCT.Description) ? subCT.Description : string.Empty),
                                    Payment_Type = pt.Description,
                                    Description = c.Description,
                                    Total = c.Total,
                                    ID_TOTAL_TRANSACTIONS = tt.ID
                                 });



                    ////Filtros de Pesquisa
                    //if (filtroPesquisa.ID_TOTAL_TRANSACTION> 0)
                        //query = query.Where(q => q.ID_TOTAL_TRANSACTIONS == 23);

                    //if (filtroPesquisa.ID_Calc_Type > 0)
                    //    query = query.Where(q => q.Calc_Type == (from c in context.Calc_Type where c.ID == filtroPesquisa.ID_Calc_Type select c.Description).FirstOrDefault().ToString());

                    //if (!string.IsNullOrEmpty(filtroPesquisa.dtInicio))
                    //{
                    //    DateTime dtFim = DateTime.Now;
                    //    if (!string.IsNullOrEmpty(filtroPesquisa.dtFim))
                    //        dtFim = Convert.ToDateTime(filtroPesquisa.dtFim);

                    //    query = query.Where(q => (Convert.ToDateTime(q.DT_reg) >= Convert.ToDateTime(filtroPesquisa.dtInicio) && Convert.ToDateTime(q.DT_reg) <= dtFim));
                    //}

                    //ordenação
                    if (filtroPesquisa.sSortDir_0 == "asc")
                    {
                        if (filtroPesquisa.iSortCol_0 == 1)
                            query = query.OrderByDescending(q => q.Date);
                        else if (filtroPesquisa.iSortCol_0 == 2)
                            query = query.OrderByDescending(q => q.User);
                        else if (filtroPesquisa.iSortCol_0 == 3)
                            query = query.OrderByDescending(q => q.ID);
                        else
                            query = query.OrderByDescending(q => q.Date);
                    }
                    else if (filtroPesquisa.sSortDir_0 == "desc")
                    {
                        if (filtroPesquisa.iSortCol_0 == 1)
                            query = query.OrderBy(q => q.Date);
                        else if (filtroPesquisa.iSortCol_0 == 2)
                            query = query.OrderBy(q => q.User);
                        else if (filtroPesquisa.iSortCol_0 == 3)
                            query = query.OrderBy(q => q.ID);
                        else
                            query = query.OrderBy(q => q.Date);
                    }
                    else
                        query = query.OrderByDescending(q => q.Date);

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

        MessageInstance<Dominio.Entidades.Calc> ITRANSACTIONS.GetTotalToCalc()
        {
            var msg = new MessageInstance<Dominio.Entidades.Calc>();
            try
            {
                using (var contexto = new HostelEntities())
                {
                    List<int> idsCalcType = (from a in contexto.Calc_Type
                                             select a.ID).ToList();

                    double total = 0;
                    for (int i = 0; i < idsCalcType.Count; i++)
                    {
                        total += (from a in contexto.Money_Count
                                  orderby a.DT_Reg descending
                                  where a.ID_Calc_Type == idsCalcType[i]
                                  select a.ID_Calc_Type).FirstOrDefault();
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
