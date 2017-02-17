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
    public class CALC : ICALC
    {
        MessageInstance<Dominio.Entidades.Calc> ICALC.GetTotalLastCalc()
        {
            var msg = new MessageInstance<Dominio.Entidades.Calc>();
            try
            {
                using (var contexto = new HostelEntities())
                {
                    msg.Instance = (from c in contexto.Calcs
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

        MessageCollection<GridAtosPessoalConsulta> ICALC.GetAllCalcByFilter(Dominio.Mensagens.Filtro.Calc filtroPesquisa)
        {
            var msg = new MessageCollection<Dominio.Mensagens.GridAtosPessoalConsulta>();
            try
            {
                using (var context = new HostelEntities())
                {
                    var enumTabTipoSolicitacao = UtilsLibrary.GetListEnum(typeof(TabTipoSolicitacao));
                    string ssa = enumTabTipoSolicitacao.Where(w => w.Value == "6").Select(s => s.StringTabTipoSolicitacao).FirstOrDefault();
                    ////Não pode ser feito o .ToList nesse momento, a query SQL esta esta sendo montada de acordo com os filtros
                   var query = (from c in context.Calcs
                                //join ct in context.Calc_Type on c.ID_Calc_Type equals ct.ID
                                //join mc in context.Money_Count on c.ID_Money_Count equals mc.ID

                                //join s in context.V_SFI_Servidor on a.identificadorSFI_Servidor equals s.identificador
                                //join soli in context.V_SV_Solicitacao on a.identificador_Solicitacao equals soli.identificador
                                //join apo in context.AP_APOSENTADORIA on a.identificador equals apo.identificador

                                //join soli in context.V_SV_Solicitacao on a.identificador_Solicitacao equals soli.identificador into soliLeft
                                //join apo in context.AP_APOSENTADORIA on a.identificador equals apo.identificador into apoLeft
                                //from subSoli in soliLeft.DefaultIfEmpty()
                                //from subApo in apoLeft.DefaultIfEmpty()
                                //where a.identificador_Solicitacao != null
                                select new Dominio.Mensagens.GridCalc()
                                 {
                                     ID = c.ID,
                                     DT_reg = c.DT_reg,
                                     //Calc_Type = ct.Description.ToString(),
                                     LogLogin = c.LogLogin,
                                     Total = c.Total.ToString()
                                     //identificador = a.identificador,
                                     // nome = s.nomeServidor,
                                     // dtCadastro = (subApo != null && subApo.dataEnvioAposentadoria.HasValue && !string.IsNullOrEmpty(subApo.dataEnvioAposentadoria.Value.ToString()) ? subApo.dataEnvioAposentadoria.Value.ToString() : string.Empty),
                                     // Estado = a.numeroProcesso.HasValue ? "Ato Enviado" : "Ato Não Enviado",
                                     // numeroProcesso = a.numeroProcesso.ToString(),
                                     // TipoAtoPessoal = (subSoli == null ? a.identificadorTAB_TipoAtoPessoal.Value.ToString() : subSoli.identificadorSV_TabTipoSolicitacao.Value.ToString()),
                                     // CPF = s.numeroCpf.ToString(),
                                     // unidadeGestora = a.identificadorUnidadeGestora
                                 });

                    //Filtros de Pesquisa
                    //if (!string.IsNullOrEmpty(filtroPesquisa.nome))
                    //    query = query.Where(q => q.nome.Trim().ToUpper().Contains(filtroPesquisa.nome.Trim().ToUpper()));

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
                            query = query.OrderBy(q => q.ID);
                        else if (filtroPesquisa.iSortCol_0 == 2)
                            query = query.OrderBy(q => q.DT_reg);
                        else if (filtroPesquisa.iSortCol_0 == 3)
                            query = query.OrderBy(q => q.Calc_Type);
                        else
                            query = query.OrderBy(q => q.ID);
                    }
                    else if (filtroPesquisa.sSortDir_0 == "desc")
                    {
                        if (filtroPesquisa.iSortCol_0 == 1)
                            query = query.OrderByDescending(q => q.ID);
                        else if (filtroPesquisa.iSortCol_0 == 2)
                            query = query.OrderByDescending(q => q.Calc_Type);
                        else if (filtroPesquisa.iSortCol_0 == 3)
                            query = query.OrderByDescending(q => q.DT_reg);
                        else
                            query = query.OrderByDescending(q => q.Total);
                    }
                    else
                        query = query.OrderBy(q => q.ID);

                    query = query.Take(100);

                    ////Extract only current page
                    //msg.Instances = query.Skip(filtroPesquisa.iDisplayStart).Take(filtroPesquisa.iDisplayLength).ToList();


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
        
    }
}
