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
    public class TOTAL_TRANSACTIONS : ITOTAL_TRANSACTIONS
    {
        MessageInstance<Dominio.Entidades.Total_Transactions> ITOTAL_TRANSACTIONS.GetLastTotal_Transaction()
        {
            var msg = new MessageInstance<Dominio.Entidades.Total_Transactions>();
            try
            {
                using (var contexto = new HostelEntities())
                {
                    msg.Instance = (from a in contexto.Total_Transactions
                                    where a.ID_Report_Type == (int)TabReport_Type.Cash
                                    || a.ID_Report_Type == (int)TabReport_Type.Bank
                                    orderby a.ID descending
                                    select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageInstance<Dominio.Entidades.Total_Transactions> ITOTAL_TRANSACTIONS.GetLastTotal_Vagner()
        {
            var msg = new MessageInstance<Dominio.Entidades.Total_Transactions>();
            try
            {
                using (var contexto = new HostelEntities())
                {
                    msg.Instance = (from a in contexto.Total_Transactions
                                    where a.ID_Report_Type == (int)TabReport_Type.Bank
                                    orderby a.ID descending
                                    select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageInstance<Dominio.Entidades.Total_Transactions> ITOTAL_TRANSACTIONS.GetLastTotal_Card()
        {
            var msg = new MessageInstance<Dominio.Entidades.Total_Transactions>();
            try
            {
                using (var contexto = new HostelEntities())
                {
                    DateTime dtDayMonthYearReference = DateTime.UtcNow;
                    if (dtDayMonthYearReference.Hour >= 0 && dtDayMonthYearReference.Hour <= 5)
                        dtDayMonthYearReference = dtDayMonthYearReference.AddDays(-1);

                    var query = (from a in contexto.Total_Transactions
                                    where a.ID_Report_Type == (int)TabReport_Type.Card
                                    && dtDayMonthYearReference.Date == a.DayMonthYearReference
                                    orderby a.ID descending
                                    select a).FirstOrDefault();

                    msg.Instance = query;
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageInstance<Dominio.Mensagens.TelaTotalTransaction> ITOTAL_TRANSACTIONS.GetTotalTransactionByID(int ID)
        {
            var msg = new MessageInstance<Dominio.Mensagens.TelaTotalTransaction>();
            try
            {
                using (var context = new HostelEntities())
                {
                    msg.Instance = (from t in context.Total_Transactions
                                    join c in context.Calcs on t.ID_Calc equals c.ID into cLeft
                                    join lt in context.Total_Transactions on t.ID_Last_Transaction equals lt.ID into ltLeft
                                    from subC in cLeft.DefaultIfEmpty()
                                    from subLT in ltLeft.DefaultIfEmpty()
                                    where t.ID == ID
                                    select new TelaTotalTransaction
                                    {
                                        ID = ID,
                                        TotalTransactions = t.TotalTransactions.Value,
                                        TotalFinal = t.TotalFinal.Value,
                                        DifferenceFinalCalc = t.DifferenceFinalCalc.HasValue ? t.DifferenceFinalCalc.Value : 0,
                                        DT_Reg = t.DT_Reg.Value,
                                        LogLogin = t.LogLogin,
                                        IDLastTotalCalc = (subC != null && subC.ID > 0 ? subC.ID : 0),
                                        TotalCalc = (subC != null && subC.Total > 0 ? subC.Total : 0),
                                        IDLastTotalCashier = (subLT != null && subLT.ID > 0 ? subLT.ID : 0),
                                        LastTotalCashier = (subLT != null && subLT.TotalFinal.HasValue ? subLT.TotalFinal.Value : t.Last_Cashier_Total.HasValue ? t.Last_Cashier_Total.Value : 0)
                                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageCollection<GridTotalTransactionIndex> ITOTAL_TRANSACTIONS.GetAllTotalTransactions(Dominio.Mensagens.Filtro.TotalTransactions filtroPesquisa)
        {
            var msg = new MessageCollection<Dominio.Mensagens.GridTotalTransactionIndex>();
            try
            {
                using (var context = new HostelEntities())
                {
                    var query = (from tt in context.Total_Transactions
                                 join c in context.Calcs on tt.ID_Calc equals c.ID into cLeft
                                 join lt in context.Total_Transactions on tt.ID_Last_Transaction equals lt.ID into ltLeft
                                 from subC in cLeft.DefaultIfEmpty()
                                 from subLT in ltLeft.DefaultIfEmpty()
                                 where tt.ID_Report_Type != filtroPesquisa.ID_REPORT_TYPE
                                 select new Dominio.Mensagens.GridTotalTransactionIndex()
                                 {
                                     ID = tt.ID,
                                     DT_Reg = tt.DT_Reg.Value,
                                     LogLogin = tt.LogLogin,
                                     TotalTransactions = tt.TotalTransactions.Value,
                                     IDLastTotalCalc = (subC != null && subC.ID > 0 ? subC.ID : 0),
                                     TotalCalc = (subC != null && subC.Total > 0 ? subC.Total : 0),
                                     IDLastTotalCashier = (subLT != null && subLT.ID > 0 ? subLT.ID : 0),
                                     LastTotalCashier = (subLT != null && subLT.TotalFinal.HasValue ? subLT.TotalFinal.Value : 0),
                                     TotalFinal = tt.TotalFinal.Value,
                                     DifferenceFinalCalc = tt.DifferenceFinalCalc.HasValue ? tt.DifferenceFinalCalc.Value : 0,
                                     ID_Report_Type = tt.ID_Report_Type.HasValue ? tt.ID_Report_Type.Value : 0,
                                     ID_SHIFT_TYPE = tt.ID_SHIFT_TYPE.HasValue ? tt.ID_SHIFT_TYPE.Value : 0,
                                     DAY_MONTH_YEAR_REFERENCE = tt.DayMonthYearReference.HasValue ? tt.DayMonthYearReference.Value : (DateTime?)null
                                 });

                    ////Filtros de Pesquisa
                    if (filtroPesquisa.Report_Type > 0)
                        query = query.Where(q => (q.ID_Report_Type == filtroPesquisa.Report_Type));

                    if (!string.IsNullOrEmpty(filtroPesquisa.txtNome))
                        query = query.Where(q => q.LogLogin.Contains(filtroPesquisa.txtNome));

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
                            query = query.OrderBy(q => q.TotalTransactions);
                        else if (filtroPesquisa.iSortCol_0 == 4)
                            query = query.OrderBy(q => q.LastTotalCashier);
                        else if (filtroPesquisa.iSortCol_0 == 5)
                            query = query.OrderBy(q => q.TotalFinal);
                        else if (filtroPesquisa.iSortCol_0 == 6)
                            query = query.OrderBy(q => q.DifferenceFinalCalc);
                        else if (filtroPesquisa.iSortCol_0 == 7)
                            query = query.OrderBy(q => q.Desc_Report_Type);
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
                            query = query.OrderByDescending(q => q.TotalTransactions);
                        else if (filtroPesquisa.iSortCol_0 == 4)
                            query = query.OrderByDescending(q => q.LastTotalCashier);
                        else if (filtroPesquisa.iSortCol_0 == 5)
                            query = query.OrderByDescending(q => q.TotalFinal);
                        else if (filtroPesquisa.iSortCol_0 == 6)
                            query = query.OrderByDescending(q => q.DifferenceFinalCalc);
                        else if (filtroPesquisa.iSortCol_0 == 7)
                            query = query.OrderByDescending(q => q.Desc_Report_Type);
                        else
                            query = query.OrderByDescending(q => q.DT_Reg);
                    }
                    else
                        query = query.OrderByDescending(q => q.DT_Reg);

                    query = query.Take(100);

                    ////Extract only current page
                    msg.Instances = query.Skip(filtroPesquisa.iDisplayStart).Take(filtroPesquisa.iDisplayLength).ToList();
                    //msg.Instances = query.ToList();


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


        MessageCollection<GridTotalTransactionIndex> ITOTAL_TRANSACTIONS.GetReportEndOfTheDayTotalTransactions(Dominio.Mensagens.Filtro.TotalTransactions filtroPesquisa)
        {
            var msg = new MessageCollection<Dominio.Mensagens.GridTotalTransactionIndex>();
            try
            {
                using (var context = new HostelEntities())
                {
                    var query = (from tt in context.Total_Transactions
                                 join c in context.Calcs on tt.ID_Calc equals c.ID into cLeft
                                 join lt in context.Total_Transactions on tt.ID_Last_Transaction equals lt.ID into ltLeft
                                 from subC in cLeft.DefaultIfEmpty()
                                 from subLT in ltLeft.DefaultIfEmpty()
                                 where tt.DayMonthYearReference != null
                                 && (tt.ID_Report_Type == filtroPesquisa.ID_REPORT_TYPE || filtroPesquisa.ID_REPORT_TYPE == 0)
                                 && (tt.ID_SHIFT_TYPE == filtroPesquisa.ID_SHIFT_TYPE || filtroPesquisa.ID_SHIFT_TYPE == 0)
                                 
                                 select new Dominio.Mensagens.GridTotalTransactionIndex()
                                 {
                                     ID = tt.ID,
                                     DT_Reg = tt.DT_Reg.Value,
                                     LogLogin = tt.LogLogin,
                                     TotalTransactions = tt.TotalTransactions.Value,
                                     IDLastTotalCalc = (subC != null && subC.ID > 0 ? subC.ID : 0),
                                     TotalCalc = (subC != null && subC.Total > 0 ? subC.Total : 0),
                                     IDLastTotalCashier = (subLT != null && subLT.ID > 0 ? subLT.ID : 0),
                                     LastTotalCashier = (subLT != null && subLT.TotalFinal.HasValue ? subLT.TotalFinal.Value : 0),
                                     TotalFinal = tt.TotalFinal.Value,
                                     DifferenceFinalCalc = tt.DifferenceFinalCalc.HasValue ? tt.DifferenceFinalCalc.Value : 0,
                                     ID_Report_Type = tt.ID_Report_Type.HasValue ? tt.ID_Report_Type.Value : 0,
                                     ID_SHIFT_TYPE = tt.ID_SHIFT_TYPE.HasValue ? tt.ID_SHIFT_TYPE.Value : 0,
                                     DAY_MONTH_YEAR_REFERENCE = tt.DayMonthYearReference.Value
                                 });

                    query.OrderByDescending(q => q.DAY_MONTH_YEAR_REFERENCE);

                    ////Filtros de Pesquisa
                    if (!string.IsNullOrEmpty(filtroPesquisa.Username))
                        query = query.Where(q => q.LogLogin.Contains(filtroPesquisa.Username));

                    if (!string.IsNullOrEmpty(filtroPesquisa.dtInicio))
                    {
                        DateTime dtFim = DateTime.UtcNow;
                        if (!string.IsNullOrEmpty(filtroPesquisa.dtFim))
                            dtFim = Convert.ToDateTime(filtroPesquisa.dtFim);

                        DateTime dtInicio = Convert.ToDateTime(filtroPesquisa.dtInicio);

                        query = query.Where(q => q.DAY_MONTH_YEAR_REFERENCE >= dtInicio.Date && q.DAY_MONTH_YEAR_REFERENCE <= dtFim.Date);
                    }

                    msg.Instances = query.ToList();
                    msg.Code = query.Count();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageCollection<GridTotalTransactionIndex> ITOTAL_TRANSACTIONS.GetAllTotalTransactionsByBank(TotalTransactions filtroPesquisa)
        {
            var msg = new MessageCollection<Dominio.Mensagens.GridTotalTransactionIndex>();
            try
            {
                using (var context = new HostelEntities())
                {
                    var query = (from tt in context.Total_Transactions
                                 join c in context.Calcs on tt.ID_Calc equals c.ID into cLeft
                                 join rt in context.Report_Type on tt.ID_Report_Type equals rt.ID
                                 from subC in cLeft.DefaultIfEmpty()
                                 orderby tt.DT_Reg descending
                                 where filtroPesquisa.ID_REPORT_TYPE == tt.ID_Report_Type
                                 select new Dominio.Mensagens.GridTotalTransactionIndex()
                                 {
                                     ID = tt.ID,
                                     DT_Reg = tt.DT_Reg.Value,
                                     LogLogin = tt.LogLogin,
                                     TotalTransactions = tt.TotalTransactions.Value,
                                     IDLastTotalCalc = (subC != null && subC.ID > 0 ? subC.ID : 0),
                                     TotalCalc = (subC != null && subC.Total > 0 ? subC.Total : 0),
                                     LastTotalCashier = (tt.Last_Cashier_Total.HasValue ? tt.Last_Cashier_Total.Value : 0),
                                     TotalFinal = tt.TotalFinal.Value,
                                     DifferenceFinalCalc = tt.DifferenceFinalCalc.HasValue ? tt.DifferenceFinalCalc.Value : 0,
                                     ID_Report_Type = tt.ID_Report_Type.HasValue ? tt.ID_Report_Type.Value : 0,
                                     Desc_Report_Type = rt.Description
                                 });


                    ////Filtros de Pesquisa
                    if (filtroPesquisa.Report_Type > 0)
                        query = query.Where(q => (q.ID_Report_Type == filtroPesquisa.Report_Type));

                    if (!string.IsNullOrEmpty(filtroPesquisa.txtNome))
                        query = query.Where(q => q.LogLogin.Contains(filtroPesquisa.txtNome));

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
                            query = query.OrderBy(q => q.TotalTransactions);
                        else if (filtroPesquisa.iSortCol_0 == 4)
                            query = query.OrderBy(q => q.LastTotalCashier);
                        else if (filtroPesquisa.iSortCol_0 == 5)
                            query = query.OrderBy(q => q.TotalFinal);
                        else if (filtroPesquisa.iSortCol_0 == 6)
                            query = query.OrderBy(q => q.DifferenceFinalCalc);
                        else if (filtroPesquisa.iSortCol_0 == 7)
                            query = query.OrderBy(q => q.Desc_Report_Type);
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
                            query = query.OrderByDescending(q => q.TotalTransactions);
                        else if (filtroPesquisa.iSortCol_0 == 4)
                            query = query.OrderByDescending(q => q.LastTotalCashier);
                        else if (filtroPesquisa.iSortCol_0 == 5)
                            query = query.OrderByDescending(q => q.TotalFinal);
                        else if (filtroPesquisa.iSortCol_0 == 6)
                            query = query.OrderByDescending(q => q.DifferenceFinalCalc);
                        else if (filtroPesquisa.iSortCol_0 == 7)
                            query = query.OrderByDescending(q => q.Desc_Report_Type);
                        else
                            query = query.OrderByDescending(q => q.DT_Reg);
                    }
                    else
                        query = query.OrderByDescending(q => q.DT_Reg);

                    query = query.Take(100);
                    msg.Instances = query.Skip(filtroPesquisa.iDisplayStart).Take(filtroPesquisa.iDisplayLength).ToList();
                    msg.Code = query.Count();//Total de registro com filtros aplicados
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
