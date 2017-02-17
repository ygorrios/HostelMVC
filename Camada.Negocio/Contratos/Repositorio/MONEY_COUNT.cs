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
    public class MONEY_COUNT : IMONEY_COUNT
    {
        MessageInstance<Dominio.Entidades.Money_Count> IMONEY_COUNT.GetMoney_CountById(int id)
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

        MessageCollection<GridCalc> IMONEY_COUNT.GetAllMoney_CountByFilter(Dominio.Mensagens.Filtro.Calc filtroPesquisa)
        {
            var msg = new MessageCollection<Dominio.Mensagens.GridCalc>();
            try
            {
                using (var context = new HostelEntities())
                {
                    ////Não pode ser feito o .ToList nesse momento, a query SQL esta esta sendo montada de acordo com os filtros
                    var query = (from c in context.Money_Count
                                 join ct in context.Calc_Type on c.ID_Calc_Type equals ct.ID
                                 select new Dominio.Mensagens.GridCalc()
                                 {
                                     ID = c.ID,
                                     DT_reg = c.DT_Reg,
                                     ID_Calc_Type = c.ID_Calc_Type,
                                     Calc_Type = ct.Description.ToString(),
                                     LogLogin = c.LogLogin,
                                     Total = c.Total.ToString()
                                 });

                    //Filtros de Pesquisa
                    if (!string.IsNullOrEmpty(filtroPesquisa.user))
                        query = query.Where(q => q.LogLogin.Trim().ToUpper().Contains(filtroPesquisa.user.Trim().ToUpper()));

                    if (filtroPesquisa.ID_Calc_Type > 0)
                        query = query.Where(q => q.ID_Calc_Type == filtroPesquisa.ID_Calc_Type);

                    if (!string.IsNullOrEmpty(filtroPesquisa.dtInicio))
                    {
                        DateTime dtFim = DateTime.UtcNow;
                        if (!string.IsNullOrEmpty(filtroPesquisa.dtFim))
                            dtFim = Convert.ToDateTime(filtroPesquisa.dtFim);

                        DateTime dtInicio = Convert.ToDateTime(filtroPesquisa.dtInicio);

                        query = query.Where(q => q.DT_reg >= dtInicio && q.DT_reg <= dtFim);
                    }

                    //ordenação
                    if (filtroPesquisa.sSortDir_0 == "asc")
                    {
                        if (filtroPesquisa.iSortCol_0 == 1)
                            query = query.OrderByDescending(q => q.DT_reg);
                        else if (filtroPesquisa.iSortCol_0 == 2)
                            query = query.OrderByDescending(q => q.Calc_Type);
                        else if (filtroPesquisa.iSortCol_0 == 3)
                            query = query.OrderByDescending(q => q.LogLogin);
                        else if (filtroPesquisa.iSortCol_0 == 4)
                            query = query.OrderByDescending(q => q.Total);
                        else
                            query = query.OrderByDescending(q => q.DT_reg);
                    }
                    else if (filtroPesquisa.sSortDir_0 == "desc")
                    {
                        if (filtroPesquisa.iSortCol_0 == 1)
                            query = query.OrderBy(q => q.DT_reg);
                        else if (filtroPesquisa.iSortCol_0 == 2)
                            query = query.OrderBy(q => q.Calc_Type);
                        else if (filtroPesquisa.iSortCol_0 == 3)
                            query = query.OrderBy(q => q.LogLogin);
                        else if (filtroPesquisa.iSortCol_0 == 4)
                            query = query.OrderBy(q => q.Total);
                        else
                            query = query.OrderBy(q => q.DT_reg);
                    }
                    else
                        query = query.OrderByDescending(q => q.DT_reg);

                    query = query.Take(100);

                    ////Extract only current page
                    msg.Instances = query.Skip(filtroPesquisa.iDisplayStart).Take(filtroPesquisa.iDisplayLength).ToList();

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

        MessageCollection<GridMoney_Count> IMONEY_COUNT.GetMoney_CountByID_Calc(int ID_Calc)
        {
            var msg = new MessageCollection<Dominio.Mensagens.GridMoney_Count>();
            try
            {
                using (var context = new HostelEntities())
                {
                    ////Não pode ser feito o .ToList nesse momento, a query SQL esta esta sendo montada de acordo com os filtros
                    var queryCalc = (from mc in context.Calcs
                                     where mc.ID == ID_Calc
                                     select mc).FirstOrDefault();

                    List<int> ids = new List<int>();
                    ids.Add(queryCalc.ID_Money_Count.HasValue ? queryCalc.ID_Money_Count.Value : 0);
                    ids.Add(queryCalc.ID_Money_Count2.HasValue ? queryCalc.ID_Money_Count2.Value : 0);
                    ids.Add(queryCalc.ID_Money_Count3.HasValue ? queryCalc.ID_Money_Count3.Value : 0);

                    msg.Instances = (from c in context.Money_Count
                                     where ids.Contains(c.ID)
                                     orderby c.ID_Calc_Type
                                     select new Dominio.Mensagens.GridMoney_Count()
                                     {
                                         IDMoney_Count = c.ID,
                                         IDCalc_Type = c.ID_Calc_Type,
                                         Qnt_1_Cent = c.Qnt_1_Cent,
                                         Qnt_2_Cents = c.Qnt_2_Cents,
                                         Qnt_5_Cents = c.Qnt_5_Cents,
                                         Qnt_10_Cents = c.Qnt_10_Cents,
                                         Qnt_20_Cents = c.Qnt_20_Cents,
                                         Qnt_50_Cents = c.Qnt_50_Cents,
                                         Qnt_1_Euro = c.Qnt_1_Euro,
                                         Qnt_2_Euros = c.Qnt_2_Euros,
                                         Qnt_5_Euros = c.Qnt_5_Euros,
                                         Qnt_10_Euros = c.Qnt_10_Euros,
                                         Qnt_20_Euros = c.Qnt_20_Euros,
                                         Qnt_50_Euros = c.Qnt_50_Euros,
                                         Qnt_100_Euros = c.Qnt_100_Euros,
                                         Qnt_200_Euros = c.Qnt_200_Euros,
                                         Qnt_500_Euros = c.Qnt_500_Euros,
                                         TotalMoney_Count = c.Total,
                                         DT_Reg = c.DT_Reg,
                                     }).ToList();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageInstance<Dominio.Entidades.Calc> IMONEY_COUNT.GetTotalToCalc()
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


                    //double v1 = (from a in contexto.Money_Count
                    //             where a.ID_Calc_Type == 1
                    //             select a.Total
                    //             ).FirstOrDefault();
                    //msg.Instance = ;
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageInstance<Dominio.Mensagens.TelaCalc> IMONEY_COUNT.GetMoney_CountDetailsByID(int ID)
        {
            var msg = new MessageInstance<Dominio.Mensagens.TelaCalc>();
            try
            {
                using (var context = new HostelEntities())
                {
                    msg.Instance = (from mc in context.Money_Count
                                    join c in context.Calcs on mc.ID equals c.ID_Money_Count
                                    where mc.ID == ID
                                    select new TelaCalc()
                                    {
                                        IDCalc_Type = mc.ID_Calc_Type,
                                        LogLogin = c.LogLogin,
                                        TotalCalc = c.Total,
                                        IDMoney_Count = mc.ID,
                                        Qnt_1_Cent = mc.Qnt_1_Cent,
                                        Qnt_2_Cents = mc.Qnt_2_Cents,
                                        Qnt_5_Cents = mc.Qnt_5_Cents,
                                        Qnt_10_Cents = mc.Qnt_10_Cents,
                                        Qnt_20_Cents = mc.Qnt_20_Cents,
                                        Qnt_50_Cents = mc.Qnt_50_Cents,
                                        Qnt_1_Euro = mc.Qnt_1_Euro,
                                        Qnt_2_Euros = mc.Qnt_2_Euros,
                                        Qnt_5_Euros = mc.Qnt_5_Euros,
                                        Qnt_10_Euros = mc.Qnt_10_Euros,
                                        Qnt_20_Euros = mc.Qnt_20_Euros,
                                        Qnt_50_Euros = mc.Qnt_50_Euros,
                                        Qnt_100_Euros = mc.Qnt_100_Euros,
                                        Qnt_200_Euros = mc.Qnt_200_Euros,
                                        Qnt_500_Euros = mc.Qnt_500_Euros,
                                        TotalMoney_Count = mc.Total,
                                        DT_Reg = c.DT_reg,

                                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageInstance<Dominio.Entidades.Money_Count> IMONEY_COUNT.GetLastMoney_CountByIdCalcType(int idCalcType)
        {
            var msg = new MessageInstance<Dominio.Entidades.Money_Count>();
            try
            {
                using (var contexto = new HostelEntities())
                {
                    msg.Instance = (from a in contexto.Money_Count
                                    orderby a.DT_Reg descending
                                    where a.ID_Calc_Type == idCalcType
                                    select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                msg.Exception = ex;
            }
            return msg;
        }

        MessageInstance<Dominio.Entidades.Money_Count> IMONEY_COUNT.GetMoney_CountEnvelopeByIDCalc(int ID_Calc, int idCalcType)
        {
            var msg = new MessageInstance<Dominio.Entidades.Money_Count>();
            try
            {
                using (var context = new HostelEntities())
                {
                    msg.Instance = (from mc in context.Money_Count
                                    join c in context.Calcs on mc.ID equals c.ID_Money_Count
                                    where c.ID == ID_Calc
                                    && mc.ID_Calc_Type == idCalcType
                                    select mc).FirstOrDefault();
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
