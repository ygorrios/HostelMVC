using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Camada.Dominio.Entidades;
using Camada.Aplicacao.Controllers.ActionsFilters;
using Library.Messages;
using Camada.Dominio.Mensagens;
using System.Web.UI.WebControls;
using Library.Utilities;
using Camada.Dominio;

namespace Camada.Aplicacao.Controllers
{
    [Authorize]
    [LogActionFilter]
    [UtilsActionFilter]
    public class CalcController : Controller
    {
        private HostelEntities db = new HostelEntities();

        // GET: AtosPessoalConsulta
        public ActionResult Index()
        {
            carregaCombos();
            ViewBag.TotalCalc = GetTotalToCalc();
            return View();
        }

        public ActionResult Create(int? id)
        {
            int idCalcType = id.HasValue ? id.Value : (int)TabCalc_Type.Cashier;
            TelaCalc calcView = returnLastCalcByTypeID(idCalcType);
            List<DropDownlist> drp = new List<DropDownlist>();

            drp = db.Calc_Type
                        .Select(x =>
                                    new DropDownlist()
                                    {
                                        Value = x.ID,
                                        Text = x.Description
                                    }).ToList();

            ViewBag.IDCalc_Type = new SelectList(drp, "Value", "Text", idCalcType);
            calcView.TotalCalc = GetTotalToCalc();

            return View(calcView);
        }

        public TelaCalc returnLastCalcByTypeID(int ID_CALC_TYPE)
        {
            TelaCalc telaCalc = new TelaCalc();
            var msg = Fachada.Repositorio.MONEY_COUNT.GetLastMoney_CountByIdCalcType(ID_CALC_TYPE);
            if (msg.Result != Message.ResultType.Success)
                return telaCalc;

            telaCalc.Qnt_1_Cent = msg.Instance.Qnt_1_Cent;
            telaCalc.Qnt_2_Cents = msg.Instance.Qnt_2_Cents;
            telaCalc.Qnt_5_Cents = msg.Instance.Qnt_5_Cents;
            telaCalc.Qnt_10_Cents = msg.Instance.Qnt_10_Cents;
            telaCalc.Qnt_20_Cents = msg.Instance.Qnt_20_Cents;
            telaCalc.Qnt_50_Cents = msg.Instance.Qnt_50_Cents;
            telaCalc.Qnt_1_Euro = msg.Instance.Qnt_1_Euro;
            telaCalc.Qnt_2_Euros = msg.Instance.Qnt_2_Euros;
            telaCalc.Qnt_5_Euros = msg.Instance.Qnt_5_Euros;
            telaCalc.Qnt_10_Euros = msg.Instance.Qnt_10_Euros;
            telaCalc.Qnt_20_Euros = msg.Instance.Qnt_20_Euros;
            telaCalc.Qnt_50_Euros = msg.Instance.Qnt_50_Euros;
            telaCalc.Qnt_100_Euros = msg.Instance.Qnt_100_Euros;
            telaCalc.Qnt_200_Euros = msg.Instance.Qnt_200_Euros;
            telaCalc.Qnt_500_Euros = msg.Instance.Qnt_500_Euros;
            telaCalc.TotalMoney_Count = msg.Instance.Total;

            return telaCalc;
        }

        public ActionResult IndexByTotalTransactions(int? id, int idTransaction, string returnAction, string returnController)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            carregaCombos();

            List<GridMoney_Count> listGridMoney = new List<GridMoney_Count>();

            var displayedCompanies = Fachada.Repositorio.MONEY_COUNT.GetMoney_CountByID_Calc(id.Value);
            foreach (var item in displayedCompanies.Instances)
            {
                GridMoney_Count calcView = new GridMoney_Count();
                calcView.DescriptionCalcType = UtilsLibrary.GetListEnum(typeof(TabCalc_Type)).Where(w => w.Value == item.IDCalc_Type.ToString()).Select(s => s.StringDescription).FirstOrDefault();
                calcView.LogLogin = item.LogLogin;
                calcView.TotalCalc = item.TotalCalc;
                calcView.IDMoney_Count = item.IDMoney_Count;
                calcView.Qnt_1_Cent = item.Qnt_1_Cent;
                calcView.Qnt_2_Cents = item.Qnt_2_Cents;
                calcView.Qnt_5_Cents = item.Qnt_5_Cents;
                calcView.Qnt_10_Cents = item.Qnt_10_Cents;
                calcView.Qnt_20_Cents = item.Qnt_20_Cents;
                calcView.Qnt_50_Cents = item.Qnt_50_Cents;
                calcView.Qnt_1_Euro = item.Qnt_1_Euro;
                calcView.Qnt_2_Euros = item.Qnt_2_Euros;
                calcView.Qnt_5_Euros = item.Qnt_5_Euros;
                calcView.Qnt_10_Euros = item.Qnt_10_Euros;
                calcView.Qnt_20_Euros = item.Qnt_20_Euros;
                calcView.Qnt_50_Euros = item.Qnt_50_Euros;
                calcView.Qnt_100_Euros = item.Qnt_100_Euros;
                calcView.Qnt_200_Euros = item.Qnt_200_Euros;
                calcView.Qnt_500_Euros = item.Qnt_500_Euros;
                calcView.TotalMoney_Count = item.TotalMoney_Count;
                calcView.DT_Reg = item.DT_Reg;
                listGridMoney.Add(calcView);
            }
            ViewBag.TotalCalc = GetTotalToCalc();

            return View(listGridMoney);
        }

        [HttpPost]
        public ActionResult IndexByTotalTransactions()
        {
            int id = Convert.ToInt32(!string.IsNullOrEmpty(Request.Params["idTransaction"].ToString()) && int.TryParse(Request.Params["idTransaction"].ToString(), out id) ? Convert.ToInt32(Request.Params["idTransaction"]) : 0);
            string returnAction = !string.IsNullOrEmpty(Request.Params["returnAction"].ToString()) ? Request.Params["returnAction"].ToString() : string.Empty;
            string returnController = !string.IsNullOrEmpty(Request.Params["returnController"].ToString()) ? Request.Params["returnController"].ToString() : string.Empty;
            if (string.IsNullOrEmpty(returnAction) || string.IsNullOrEmpty(returnAction))
                return RedirectToAction("Inde", "Transactions");

            return RedirectToAction(returnAction, returnController, new { id = id });
        }

        [HttpPost]
        public ActionResult RedirectToTotalTransaction()
        {
            int id = Convert.ToInt32(!string.IsNullOrEmpty(RouteData.Values["id"].ToString()) && int.TryParse(RouteData.Values["id"].ToString(), out id) ? RouteData.Values["id"] : 0);
            if (id == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            return RedirectToAction("Details", "TotalTransactions", new { id = id });
        }

        public decimal GetTotalToCalc()
        {
            List<int> idsCalcType = (from a in db.Calc_Type
                                     select a.ID).ToList();

            decimal total = 0;
            for (int i = 0; i < idsCalcType.Count; i++)
            {
                int idActual = idsCalcType[i];
                total += (from a in db.Money_Count
                          orderby a.DT_Reg descending
                          where a.ID_Calc_Type == idActual
                          select a.Total).FirstOrDefault();
            }

            return total;
        }
        public void carregaCombos()
        {
            var cmbIDCalc_Type = db.Calc_Type
                        .Select(x =>
                                  new DropDownlist()
                                  {
                                      Value = x.ID,
                                      Text = x.Description
                                  }).ToList();

            var dropDownlist = new Camada.Dominio.Mensagens.DropDownlist();
            cmbIDCalc_Type.Add(dropDownlist.AdicionarSelecione());
            ViewBag.IDCalc_Type = new SelectList(cmbIDCalc_Type.OrderBy(o => o.Value), "Value", "Text");

            //ViewBag.cmbEstado = new SelectList
            //    (
            //        new DropDownlist().ListaDDLEstado(),
            //        "Value",
            //        "Text"
            //    );

            //ViewBag.cmbTipoAto = new SelectList
            //    (
            //        new DropDownlist().ListaDDLTipoAto(),
            //        "Value",
            //        "Text"
            //    );

            //var cmbUnidade = db.V_UnidadeGestora
            //            .Where(w=>w.identificador > 0 && !string.IsNullOrEmpty(w.nomeUnidade)).Select(x =>
            //                    new DropDownlist()
            //                    {
            //                        Value = x.identificador,
            //                        Text = x.nomeUnidade
            //                    }).ToList();


            //var dropDownlist = new Camada.Dominio.Mensagens.DropDownlist();
            //cmbUnidade.Add(dropDownlist.AdicionarSelecione());
            //ViewBag.cmbUnidade = new SelectList(cmbUnidade.OrderBy(o=>o.Text), "Value","Text");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            carregaCombos();

            var msg = Fachada.Repositorio.MONEY_COUNT.GetMoney_CountDetailsByID(id.Value);
            if (msg.Result != Message.ResultType.Success)
                return Content(msg.Description);
            if (msg.Instance == null)
                return HttpNotFound();

            TelaCalc calcView = new TelaCalc();
            calcView.DescriptionCalcType = UtilsLibrary.GetListEnum(typeof(TabCalc_Type)).Where(w => w.Value == msg.Instance.IDCalc_Type.ToString()).Select(s => s.StringDescription).FirstOrDefault();
            calcView.LogLogin = msg.Instance.LogLogin;
            calcView.TotalCalc = msg.Instance.TotalCalc;
            calcView.IDMoney_Count = msg.Instance.IDMoney_Count;
            calcView.Qnt_1_Cent = msg.Instance.Qnt_1_Cent;
            calcView.Qnt_2_Cents = msg.Instance.Qnt_2_Cents;
            calcView.Qnt_5_Cents = msg.Instance.Qnt_5_Cents;
            calcView.Qnt_10_Cents = msg.Instance.Qnt_10_Cents;
            calcView.Qnt_20_Cents = msg.Instance.Qnt_20_Cents;
            calcView.Qnt_50_Cents = msg.Instance.Qnt_50_Cents;
            calcView.Qnt_1_Euro = msg.Instance.Qnt_1_Euro;
            calcView.Qnt_2_Euros = msg.Instance.Qnt_2_Euros;
            calcView.Qnt_5_Euros = msg.Instance.Qnt_5_Euros;
            calcView.Qnt_10_Euros = msg.Instance.Qnt_10_Euros;
            calcView.Qnt_20_Euros = msg.Instance.Qnt_20_Euros;
            calcView.Qnt_50_Euros = msg.Instance.Qnt_50_Euros;
            calcView.Qnt_100_Euros = msg.Instance.Qnt_100_Euros;
            calcView.Qnt_200_Euros = msg.Instance.Qnt_200_Euros;
            calcView.Qnt_500_Euros = msg.Instance.Qnt_500_Euros;
            calcView.TotalMoney_Count = msg.Instance.TotalMoney_Count;
            calcView.DT_Reg = msg.Instance.DT_Reg;

            return View(calcView);
        }

        public JsonResult GetGrid_JsonResult(string sEcho, int iDisplayStart, int iDisplayLength, string sSortDir_0, int iSortCol_0)
        {
            var cmbCalcType = Convert.ToString(Request["IDCalc_Type"]);
            var dtInicio = Convert.ToString(Request["dtInicio"]);
            var dtFim = Convert.ToString(Request["dtFim"]);
            var txtUser = Convert.ToString(Request["txtUser"]);

            var displayedCompanies = Fachada.Repositorio.MONEY_COUNT.GetAllMoney_CountByFilter(new Dominio.Mensagens.Filtro.Calc
            {
                //Dados Padrão da Paginação Coluna Clicada, Ordenação ASC DESC, Skip, Take, 
                iDisplayStart = iDisplayStart,
                iDisplayLength = iDisplayLength,
                sSortDir_0 = sSortDir_0,
                iSortCol_0 = iSortCol_0,

                //Filtro Pode ser com Valor ou Empty a Classe irá tratar se a variável tem ou não valor
                ID_Calc_Type = !string.IsNullOrEmpty(cmbCalcType) ? Convert.ToInt32(cmbCalcType) : 0,
                dtInicio = dtInicio.ToString(),
                dtFim = dtFim.ToString(),
                user = txtUser//,
                //ID = !string.IsNullOrEmpty(cmbTipoAto) ? Convert.ToInt32(cmbTipoAto) : 0,
                //CPF = txtCPF,
                //nome = txtNomeServidor,
                //dtInicio = dtInicio,
                //dtFim = dtFim,
                //unidade = !string.IsNullOrEmpty(cmbUnidade) ? Convert.ToInt32(cmbUnidade) : 0
            });

            var result = (from c in displayedCompanies.Instances
                          select new[] {
                                            c.ID.ToString(),
                                            c.DT_reg.ToString("dd/MM/yyyy HH:MM"),
                                            c.Calc_Type.ToString(),
                                            c.LogLogin.ToString(),
                                            c.Total.ToString(),

        // TODOS OS CAMPOS DEVEM SER CONVERTIDOS PARA STRING senão o plugin não reconhece
        //Convert.ToString(c.identificador),
        //c.identificador.ToString(),
        //c.nome,
        //Convert.ToDateTime(c.dtCadastro).ToString("dd/MM/yyyy"),
        //c.Estado,
        //c.numeroProcesso,
        //UtilsLibrary.GetListEnum(typeof(TabTipoSolicitacao)).Where(w=>w.Value == c.TipoAtoPessoal.ToString()).Select(s=>s.StringTitulo).FirstOrDefault(),
    }).Take(10);

            return Json(new
            {
                sEcho = sEcho,// Variável Padraão do plugin
                iTotalRecords = 50,// Deve realizar outra pesquisa no Banco de Daods sem Filtro de pesquisa e mostar a quantidade de registros no Banco nessa variável "iTotalRecords" no exemplo esta 1000 fixo mas deve ser a qtde total de registro que existe na tabela
                iTotalDisplayRecords = displayedCompanies.Code,//Total de Registro com filtro  Aplicado
                aaData = result // Lista que será exibida do grid
            }, JsonRequestBehavior.AllowGet);

        }

        // GET: AtoAposentadoria/Create


        // POST: AtoAposentadoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TotalCalc, IDCalc_Type, Qnt_1_Cent, Qnt_2_Cents, Qnt_5_Cents, Qnt_10_Cents, Qnt_20_Cents, Qnt_20_Cents, Qnt_50_Cents, Qnt_1_Euro, Qnt_2_Euros, Qnt_5_Euros, Qnt_10_Euros, Qnt_10_Euros, Qnt_20_Euros, Qnt_50_Euros, Qnt_100_Euros, Qnt_200_Euros, Qnt_500_Euros, TotalMoney_Count")] TelaCalc calcView)
        {
            if (ModelState.IsValid)
            {
                var IDCalc_Type = Convert.ToString(Request["IDCalc_Type"]);

                var moneyCount = new Money_Count();
                moneyCount.Qnt_1_Cent = calcView.Qnt_1_Cent;
                moneyCount.Qnt_2_Cents = calcView.Qnt_2_Cents;
                moneyCount.Qnt_5_Cents = calcView.Qnt_5_Cents;
                moneyCount.Qnt_10_Cents = calcView.Qnt_10_Cents;
                moneyCount.Qnt_20_Cents = calcView.Qnt_20_Cents;
                moneyCount.Qnt_50_Cents = calcView.Qnt_50_Cents;
                moneyCount.Qnt_1_Euro = calcView.Qnt_1_Euro;
                moneyCount.Qnt_2_Euros = calcView.Qnt_2_Euros;
                moneyCount.Qnt_5_Euros = calcView.Qnt_5_Euros;
                moneyCount.Qnt_10_Euros = calcView.Qnt_10_Euros;
                moneyCount.Qnt_20_Euros = calcView.Qnt_20_Euros;
                moneyCount.Qnt_50_Euros = calcView.Qnt_50_Euros;
                moneyCount.Qnt_100_Euros = calcView.Qnt_100_Euros;
                moneyCount.Qnt_200_Euros = calcView.Qnt_200_Euros;
                moneyCount.Qnt_500_Euros = calcView.Qnt_500_Euros;
                moneyCount.ID_Calc_Type = calcView.IDCalc_Type;
                moneyCount.DT_Reg = DateTime.UtcNow.AddSeconds(1);
                moneyCount.LogLogin = (User != null && User.Identity != null && !string.IsNullOrEmpty(User.Identity.Name)) ? User.Identity.Name : string.Empty;
                moneyCount.Total = calcView.TotalMoney_Count;

                var msg = Fachada.Negocio.ManterMONEY_COUNT.Salvar(moneyCount);
                if (msg.Result != Message.ResultType.Success)
                    return Content(msg.Description);

                var calc = new Calc();
                calc.Total = GetTotalToCalc();
                calc.DT_reg = DateTime.UtcNow;
                calc.LogLogin = (User != null && User.Identity != null && !string.IsNullOrEmpty(User.Identity.Name)) ? User.Identity.Name : string.Empty;
                calc.ID_Money_Count = moneyCount.ID;

                List<int> idsCalcType = (from a in db.Calc_Type
                                         where a.ID != calcView.IDCalc_Type
                                         select a.ID).ToList();


                List<int> idsMoneyCount = new List<int>();
                for (int i = 0; i < idsCalcType.Count; i++)
                {
                    int idActual = idsCalcType[i];
                    idsMoneyCount.Add((from a in db.Money_Count
                                       orderby a.DT_Reg descending
                                       where a.ID_Calc_Type == idActual
                                       select a.ID).FirstOrDefault());
                }

                if (idsMoneyCount.Count > 0 && idsMoneyCount[0] > 0)
                    calc.ID_Money_Count2 = idsMoneyCount[0];
                if (idsMoneyCount.Count > 1 && idsMoneyCount[1] > 0)
                    calc.ID_Money_Count3 = idsMoneyCount[1];

                var msgSaveCalc = Fachada.Negocio.ManterCALC.Salvar(calc);
                if (msgSaveCalc.Result != Message.ResultType.Success)
                    return Content(msgSaveCalc.Description);


                if (msg.Result == Message.ResultType.Success)
                    return RedirectToAction("Index");
                else
                    return Content("Erro ao Salvar " + msg.Description + "");
            }
            else
            {
                carregaCombos();
                return View();
            }
        }

        // GET: AtoAposentadoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            carregaCombos();

            var msg = Fachada.Repositorio.MONEY_COUNT.GetMoney_CountDetailsByID(id.Value);
            if (msg.Result != Message.ResultType.Success)
                return Content(msg.Description);
            if (msg.Instance == null)
                return HttpNotFound();

            TelaCalc calcView = new TelaCalc();
            calcView.DescriptionCalcType = UtilsLibrary.GetListEnum(typeof(TabCalc_Type)).Where(w => w.Value == msg.Instance.IDCalc_Type.ToString()).Select(s => s.StringDescription).FirstOrDefault();
            calcView.LogLogin = msg.Instance.LogLogin;
            calcView.TotalCalc = msg.Instance.TotalCalc;
            calcView.IDMoney_Count = msg.Instance.IDMoney_Count;
            calcView.Qnt_1_Cent = msg.Instance.Qnt_1_Cent;
            calcView.Qnt_2_Cents = msg.Instance.Qnt_2_Cents;
            calcView.Qnt_5_Cents = msg.Instance.Qnt_5_Cents;
            calcView.Qnt_10_Cents = msg.Instance.Qnt_10_Cents;
            calcView.Qnt_20_Cents = msg.Instance.Qnt_20_Cents;
            calcView.Qnt_50_Cents = msg.Instance.Qnt_50_Cents;
            calcView.Qnt_1_Euro = msg.Instance.Qnt_1_Euro;
            calcView.Qnt_2_Euros = msg.Instance.Qnt_2_Euros;
            calcView.Qnt_5_Euros = msg.Instance.Qnt_5_Euros;
            calcView.Qnt_10_Euros = msg.Instance.Qnt_10_Euros;
            calcView.Qnt_20_Euros = msg.Instance.Qnt_20_Euros;
            calcView.Qnt_50_Euros = msg.Instance.Qnt_50_Euros;
            calcView.Qnt_100_Euros = msg.Instance.Qnt_100_Euros;
            calcView.Qnt_200_Euros = msg.Instance.Qnt_200_Euros;
            calcView.Qnt_500_Euros = msg.Instance.Qnt_500_Euros;
            calcView.TotalMoney_Count = msg.Instance.TotalMoney_Count;
            calcView.DT_Reg = msg.Instance.DT_Reg;

            return View(calcView);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var msgTotalTransaction = Fachada.Negocio.ManterMONEY_COUNT.DeleteMoney_CountByID(id);
            if (msgTotalTransaction.Result != Message.ResultType.Success)
                return Content(msgTotalTransaction.Description);

            var msgCalc = Fachada.Negocio.ManterCALC.DeleteCalcByIDMoney_Count(id);
            if (msgCalc.Result != Message.ResultType.Success)
                return Content(msgCalc.Description);

            return RedirectToAction("Index", "Calc");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
