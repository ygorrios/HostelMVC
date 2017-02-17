using Camada.Dominio;
using Camada.Dominio.Entidades;
using Library.Messages;
using Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Camada.Aplicacao.Controllers
{
    public class BankController : Controller
    {
        //public ActionResult BeforeDetails(int? id)
        //{
        //    if (id == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //   return RedirectToAction("Details", "Bank", new { id = id });
        //}


        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var msg = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetTotalTransactionByID(id.Value);
            if (msg.Result != Message.ResultType.Success)
                return Content(msg.Description);
            if (msg.Instance == null)
                return HttpNotFound();

            ViewBag.ID_TOTAL_TRANSACTIONS = id;
            
            var msgEnvelope = Fachada.Repositorio.MONEY_COUNT.GetMoney_CountEnvelopeByIDCalc(msg.Instance.IDLastTotalCalc, (int)TabCalc_Type.Envelope);
            if (msgEnvelope != null && msgEnvelope.Instance != null)
            {
                msg.Instance.CashierLastReturn = msg.Instance.TotalFinal;
                msg.Instance.BankEnvelope = msgEnvelope.Instance.Total;
                msg.Instance.TotalFinal = msg.Instance.TotalTransactions + msg.Instance.LastTotalCashier;
            }
            return View(msg.Instance);
        }

        public JsonResult getGridTransactionDetails(string sEcho, int iDisplayStart, int iDisplayLength, string sSortDir_0, int iSortCol_0)
        {
            var msgTransactions = new Dominio.Mensagens.Filtro.Transactions();
            msgTransactions.iDisplayStart = iDisplayStart;
            msgTransactions.iDisplayLength = iDisplayLength;
            msgTransactions.sSortDir_0 = sSortDir_0;
            msgTransactions.iSortCol_0 = iSortCol_0;
            msgTransactions.ID_TOTAL_TRANSACTION = Convert.ToInt32(!string.IsNullOrEmpty(RouteData.Values["id"].ToString()) && int.TryParse(RouteData.Values["id"].ToString(), out msgTransactions.ID_TOTAL_TRANSACTION) ? RouteData.Values["id"] : 0);

            var displayedCompanies = Fachada.Repositorio.TRANSACTIONS.GetAllTransactionsByID_Total_Transactions(msgTransactions);

            var result = (from c in displayedCompanies.Instances
                          select new[] {
                                            //c.ID.ToString(),
                                            Convert.ToDateTime(c.Date).ToString("dd/MM/yyyy"),
                                            Convert.ToDateTime(c.Date).ToString("HH:MM"),
                                            c.User,
                                            c.Reservation_Number,
                                            c.Guest_Name,
                                            c.Transaction_Type,
                                            c.Payment_Type,
                                            c.Description,
                                            c.Total.ToString("C2")

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

        // GET: Bank
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getGridTransaction(string sEcho, int iDisplayStart, int iDisplayLength, string sSortDir_0, int iSortCol_0)
        {
            var msgTotalTransactions = new Dominio.Mensagens.Filtro.TotalTransactions();
            msgTotalTransactions.iDisplayStart = iDisplayStart;
            msgTotalTransactions.iDisplayLength = iDisplayLength;
            msgTotalTransactions.sSortDir_0 = "desc";// sSortDir_0;
            msgTotalTransactions.iSortCol_0 = iSortCol_0;
            msgTotalTransactions.Report_Type = Convert.ToInt32(Request["cmbReport_Type"]);
            msgTotalTransactions.dtInicio = Convert.ToString(Request["txtDtInicio"]);
            msgTotalTransactions.dtFim = Convert.ToString(Request["txtDtFim"]);
            msgTotalTransactions.txtNome = Convert.ToString(Request["txtNome"]);
            msgTotalTransactions.ID_REPORT_TYPE = (int)TabReport_Type.Bank;

            var displayedCompanies = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetAllTotalTransactionsByBank(msgTotalTransactions);
            foreach (var item in displayedCompanies.Instances)
            {
                var msgEnvelope = Fachada.Repositorio.MONEY_COUNT.GetMoney_CountEnvelopeByIDCalc(item.IDLastTotalCalc, (int)TabCalc_Type.Envelope);
                if (msgEnvelope != null && msgEnvelope.Instance != null)
                {
                    item.CashierLastReturn = item.TotalFinal;
                    item.BankEnvelope = msgEnvelope.Instance.Total;
                    item.TotalFinal = item.TotalTransactions + item.LastTotalCashier;
                }
            }
            var result = (from c in displayedCompanies.Instances
                          select new[] {
                                            c.ID.ToString(),
                                            c.DT_Reg.ToString("dd/MM/yyyy HH:MM"),
                                            c.LogLogin,
                                            c.TotalTransactions.ToString("C2"),
                                            c.LastTotalCashier.ToString("C2"),
                                            c.TotalFinal.ToString("C2"),
                                            c.CashierLastReturn.ToString("C2"),
                                            c.BankEnvelope.ToString("C2")
                                            //c.TotalCalc.ToString("C2"),
                                            //c.DifferenceFinalCalc.ToString("C2"),
                        }).Take(iDisplayLength);

            return Json(new
            {
                sEcho = sEcho,// Variável Padraão do plugin
                iTotalRecords = 50,// Deve realizar outra pesquisa no Banco de Daods sem Filtro de pesquisa e mostar a quantidade de registros no Banco nessa variável "iTotalRecords" no exemplo esta 1000 fixo mas deve ser a qtde total de registro que existe na tabela
                iTotalDisplayRecords = displayedCompanies.Code,//Total de Registro com filtro  Aplicado
                aaData = result // Lista que será exibida do grid
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cmbReport_Type, txtCopied")] Dominio.Mensagens.TelaTransactions transaction)
        {
            if (ModelState.IsValid)
            {
                string txtCopied = Convert.ToString(Request["txtCopied"]);
                decimal txtTotalLastCashierVagner = 0;
                if (!string.IsNullOrEmpty(Request["TotalLastCashierVagner"]))
                    txtTotalLastCashierVagner = Convert.ToDecimal(Request["TotalLastCashierVagner"]);

                decimal totalLastEnvelope = 0, totalTransactions = 0;
                int idLastCalc = 0;
                List<Transaction> listTrans = new List<Transaction>();

                var lsTabTransaction = UtilsLibrary.GetListEnum(typeof(TabTransaction_Type));
                var lsTabPayment = UtilsLibrary.GetListEnum(typeof(TabPayment_Type));

                foreach (string row in txtCopied.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    int i = 0;
                    Transaction trans = new Transaction();
                    if (!string.IsNullOrEmpty(row))
                    {
                        foreach (string cell in row.Split('\t'))
                        {
                            if (i == 0)
                                trans.DT_Reg = Convert.ToDateTime(cell);
                            else if (i == 1)
                                trans.DT_Reg = trans.DT_Reg.AddHours(Convert.ToDateTime(cell).Hour).AddMinutes(Convert.ToDateTime(cell).Minute).AddSeconds(Convert.ToDateTime(cell).Second);
                            else if (i == 2)
                                trans.LogLogin = cell;
                            else if (i == 3)
                                trans.Reservation_Number = cell;
                            else if (i == 4)
                                trans.GuestName = cell;
                            else if (i == 5)
                            {
                                if (!string.IsNullOrEmpty(cell))
                                    trans.ID_Transaction_Type = Convert.ToInt32(lsTabTransaction.Where(w => w.StringDescription.Trim().ToUpper() == cell.Trim().ToUpper()).Select(s => s.StringValue).FirstOrDefault());
                            }
                            else if (i == 6)
                            {
                                if (!string.IsNullOrEmpty(cell))
                                    trans.ID_Payment_Type = Convert.ToInt32(lsTabPayment.Where(w => w.StringDescription.Trim().ToUpper() == cell.Trim().ToUpper()).Select(s => s.StringValue).FirstOrDefault());
                                i++;
                            }
                            else if (i == 8)
                                trans.Description = cell;
                            else if (i == 9)
                            {
                                string[] totalT = cell.Split('€');
                                if (totalT[0].Contains("-"))
                                    trans.Total = Convert.ToDecimal("-" + totalT[1].Trim());
                                else if (totalT[0].Contains("("))
                                    trans.Total = Convert.ToDecimal("-" + totalT[1].Trim().Split(')')[0]);
                                else
                                    trans.Total = Convert.ToDecimal(totalT[1].Trim());

                                totalTransactions += trans.Total;
                            }
                            i++;
                        }
                        listTrans.Add(trans);
                    }
                }

                var msgTotalLastCalc = Fachada.Repositorio.CALC.GetTotalLastCalc();
                if (msgTotalLastCalc != null)
                    idLastCalc = msgTotalLastCalc.Instance.ID;

                var totalTransaction = new Total_Transactions();
                totalTransaction.ID_Report_Type = (int)TabReport_Type.Bank;

                var msgEnvelope = Fachada.Repositorio.MONEY_COUNT.GetLastMoney_CountByIdCalcType((int)TabCalc_Type.Envelope);
                if (msgEnvelope != null)
                    totalLastEnvelope = msgEnvelope.Instance.Total;

                totalTransaction.Last_Cashier_Total = txtTotalLastCashierVagner;
                totalTransaction.TotalTransactions = totalTransactions;
                totalTransaction.TotalFinal = (totalTransactions + txtTotalLastCashierVagner) - totalLastEnvelope;
                totalTransaction.LogLogin = (User != null && User.Identity != null && !string.IsNullOrEmpty(User.Identity.Name)) ? User.Identity.Name : string.Empty;
                totalTransaction.DT_Reg = DateTime.UtcNow;
                totalTransaction.ID_Calc = idLastCalc;


                var msgSaveTotal_Transactions = Fachada.Negocio.ManterTOTAL_TRANSACTIONS.Salvar(totalTransaction);
                if (msgSaveTotal_Transactions.Result != Message.ResultType.Success)
                    return Content(msgSaveTotal_Transactions.Description);

                foreach (var item in listTrans)
                    item.ID_Total_Transactions = totalTransaction.ID;

                var msgSaveTransactions = Fachada.Negocio.ManterTRANSACTIONS.SaveList(listTrans.ToArray());
                if (msgSaveTransactions.Result != Message.ResultType.Success)
                    return Content(msgSaveTransactions.Description);

                return RedirectToAction("Details", "Bank", new { id = totalTransaction.ID });
            }

            return View(transaction);
        }

        public ActionResult DetailsCalc()
        {
            int id = Convert.ToInt32(!string.IsNullOrEmpty(RouteData.Values["id"].ToString()) && int.TryParse(RouteData.Values["id"].ToString(), out id) ? RouteData.Values["id"] : 0);
            if (id == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var msg = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetTotalTransactionByID(id);
            if (msg.Result != Message.ResultType.Success)
                return Content(msg.Description);
            if (msg.Instance == null)
                return HttpNotFound();

            return RedirectToAction("IndexByTotalTransactions", "Calc", new { id = msg.Instance.IDLastTotalCalc, idTransaction = id, returnAction = "Details", returnController = "Bank" });
        }

    }
}