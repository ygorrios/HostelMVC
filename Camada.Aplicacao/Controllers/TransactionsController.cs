using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Camada.Dominio.Entidades;
using System.Collections;
using System.Web.Script.Serialization;
using Library.Utilities;
using Camada.Dominio;
using Library.Messages;
using Camada.Dominio.Mensagens;
using System.ComponentModel.DataAnnotations;

namespace Camada.Aplicacao.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private HostelEntities db = new HostelEntities();

        public void ShowDialogMessage(TabDialogMessage dialogMessage, string message)
        {
            if (dialogMessage.ToString() == TabDialogMessage.Danger.ToString())
            {
                ViewBag.StatusDanger = message;
                ModelState.AddModelError("", message);
            }
            else if (dialogMessage.ToString() == TabDialogMessage.Sucess.ToString())
                ViewBag.StatusSucess = message;
            else if (dialogMessage.ToString() == TabDialogMessage.Info.ToString())
            {
                ViewBag.StatusInfo = message;
                ModelState.AddModelError("", message);
            }
            else if (dialogMessage.ToString() == TabDialogMessage.Warning.ToString())
            {
                ViewBag.StatusWarning = message;
                ModelState.AddModelError("", message);
            }
        }

        // GET: Transactions
        public ActionResult Index()
        {
            carregaCombos();

            return View();
        }

        public void carregaCombos()
        {
            var cmbReport_Type = db.Report_Type
                        .Where(w => w.ID != (int)TabReport_Type.Bank)
                        .Select(x =>
                                  new DropDownlist()
                                  {
                                      Value = x.ID,
                                      Text = x.Description
                                  }).ToList();

            var dropDownlist = new Camada.Dominio.Mensagens.DropDownlist();
            cmbReport_Type.Add(dropDownlist.AdicionarSelecione());
            ViewBag.cmbReport_Type = new SelectList(cmbReport_Type.OrderBy(o => o.Value), "Value", "Text");

            var cmbSHIFT_TYPE = db.SHIFT_TYPE
                        .Select(x =>
                                  new DropDownlist()
                                  {
                                      Value = x.ID,
                                      Text = x.DESCRIPTION
                                  }).ToList();

            ViewBag.cmbSHIFT_TYPE = new SelectList(cmbSHIFT_TYPE.OrderBy(o => o.Value), "Value", "Text");

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var msg = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetTotalTransactionByID(id.Value);
            if (msg.Result != Message.ResultType.Success)
                return Content(msg.Description);
            if (msg.Instance == null)
                return HttpNotFound();

            return RedirectToAction("Details", "TotalTransactions", new { id = id });
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            TelaTransactions trans = new TelaTransactions();
            carregaCombos();
            var msgTotalLastCalc = Fachada.Repositorio.CALC.GetTotalLastCalc();
            if (msgTotalLastCalc != null)
            {
                trans.ID_Last_Calc = msgTotalLastCalc.Instance.ID;
                trans.TotalCalc = msgTotalLastCalc.Instance.Total;
            }

            var msgTotalLastCashier = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetLastTotal_Transaction();
            if (msgTotalLastCashier != null && msgTotalLastCashier.Instance != null && msgTotalLastCashier.Instance.TotalFinal.HasValue)
            {
                trans.ID_Last_Transaction = msgTotalLastCashier.Instance.ID;
                trans.TotalLastCashier = msgTotalLastCashier.Instance.TotalFinal.Value;
            }

            var msgTotalLastCashierVagner = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetLastTotal_Vagner();
            if (msgTotalLastCashierVagner != null && msgTotalLastCashierVagner.Instance != null && msgTotalLastCashierVagner.Instance.TotalFinal.HasValue)
            {
                trans.ID_Last_TransactionVagner = msgTotalLastCashierVagner.Instance.ID;
                trans.TotalLastCashierVagner = msgTotalLastCashierVagner.Instance.TotalFinal.Value;
            }

            var msgTotalLastCashierCard = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetLastTotal_Card();
            if (msgTotalLastCashierCard != null && msgTotalLastCashierCard.Instance != null && msgTotalLastCashierCard.Instance.TotalFinal.HasValue)
            {
                trans.ID_Last_TransactionCard = msgTotalLastCashierCard.Instance.ID;
                trans.TotalLastCashierCard = msgTotalLastCashierCard.Instance.TotalFinal.Value;
            }

            return View(trans);
        }

        public ActionResult CreateStart()
        {
            carregaCombos();
            return View();
        }
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStart(Total_Transactions transaction)
        {
            if (ModelState.IsValid)
            {
                decimal txtTotal = Convert.ToDecimal(Request["TotalFinal"]);

                var totalTransaction = new Total_Transactions();
                totalTransaction.ID_Report_Type = (int)TabReport_Type.Cash;
                totalTransaction.TotalFinal = txtTotal;
                totalTransaction.LogLogin = (User != null && User.Identity != null && !string.IsNullOrEmpty(User.Identity.Name)) ? User.Identity.Name : string.Empty;
                totalTransaction.DT_Reg = DateTime.UtcNow;
                int idLasCashier = 0, idLastCalc = 0;
                decimal totalLastCashier = 0, totalLastCalc = 0;

                var msgTotalLastCalc = Fachada.Repositorio.CALC.GetTotalLastCalc();
                if (msgTotalLastCalc != null)
                {
                    totalLastCalc = msgTotalLastCalc.Instance.Total;
                    idLastCalc = msgTotalLastCalc.Instance.ID;
                }
                var msgTotalLastCashier = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetLastTotal_Transaction();
                if (msgTotalLastCashier != null && msgTotalLastCashier.Instance != null && msgTotalLastCashier.Instance.TotalFinal.HasValue)
                {
                    totalLastCashier = msgTotalLastCashier.Instance.TotalFinal.Value;
                    idLasCashier = msgTotalLastCashier.Instance.ID;
                    totalTransaction.ID_Last_Transaction = idLasCashier;
                }

                totalTransaction.DifferenceFinalCalc = totalLastCalc - txtTotal;
                totalTransaction.TotalTransactions = txtTotal;
                totalTransaction.ID_Calc = idLastCalc;

                var msgSaveTotal_Transactions = Fachada.Negocio.ManterTOTAL_TRANSACTIONS.Salvar(totalTransaction);
                if (msgSaveTotal_Transactions.Result != Message.ResultType.Success)
                    return Content(msgSaveTotal_Transactions.Description);


                List<Transaction> listTrans = new List<Transaction>();
                Transaction trans = new Transaction();
                trans.DT_Reg = DateTime.UtcNow;
                trans.LogLogin = User.Identity.Name;
                trans.Reservation_Number = string.Empty;
                trans.GuestName = string.Empty;
                trans.Total = txtTotal;
                trans.ID_Transaction_Type = (int)TabTransaction_Type.Payment;
                trans.ID_Payment_Type = (int)TabPayment_Type.Cash;
                trans.ID_Total_Transactions = totalTransaction.ID;
                trans.Description = string.Empty;
                listTrans.Add(trans);
                var msgSaveTransactions = Fachada.Negocio.ManterTRANSACTIONS.SaveList(listTrans.ToArray());
                if (msgSaveTransactions.Result != Message.ResultType.Success)
                    return Content(msgSaveTransactions.Description);

                return RedirectToAction("Details", "TotalTransactions", new { id = totalTransaction.ID });
            }
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cmbSHIFT_TYPE, cmbReport_Type, txtCopied, ID_Last_Calc, TotalCalc, ID_Last_Transaction, TotalLastCashier, ID_Last_TransactionVagner, TotalLastCashierVagner, ID_Last_TransactionCard, TotalLastCashierCard")] Dominio.Mensagens.TelaTransactions transaction)
        {
            if (string.IsNullOrEmpty(transaction.txtCopied))
                ShowDialogMessage(TabDialogMessage.Danger, "Please, fill out the field: 'Past the Sheet here'");
            if (transaction.cmbReport_Type == 0)
                ShowDialogMessage(TabDialogMessage.Danger, "Please, select the Report Type");

            if (ModelState.IsValid)
            {
                string txtCopied = Convert.ToString(Request["txtCopied"]);

                int cmbReport_Type = Convert.ToInt32(Request["cmbReport_Type"]);
                decimal totalTransactions = 0;
                var listStocks = new List<Camada.Dominio.Entidades.Stock>();
                List<Transaction> listTrans = new List<Transaction>();

                var lsTabTransaction = UtilsLibrary.GetListEnum(typeof(TabTransaction_Type));
                var lsTabPayment = UtilsLibrary.GetListEnum(typeof(TabPayment_Type));
                var lsTabCard = UtilsLibrary.GetListEnum(typeof(TabCard_Type));
                var lsTabProduct = UtilsLibrary.GetListEnum(typeof(TabProduct_Type));

                if (cmbReport_Type > 0)
                {
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
                                    {
                                        trans.ID_Payment_Type = Convert.ToInt32(lsTabPayment.Where(w => w.StringDescription.Trim().ToUpper() == cell.Trim().ToUpper()).Select(s => s.StringValue).FirstOrDefault());
                                        //if (trans.ID_Payment_Type != (int)TabPayment_Type.Card)
                                        //    i++;
                                    }
                                }
                                else if (i == 7)
                                {
                                    if (!string.IsNullOrEmpty(cell) && trans.ID_Payment_Type == (int)TabPayment_Type.Card)
                                        trans.ID_Card_Type = Convert.ToInt32(lsTabCard.Where(w => w.StringDescription.Trim().ToUpper() == cell.Trim().ToUpper()).Select(s => s.StringValue).FirstOrDefault());
                                }
                                else if (i == 8)
                                {
                                    string stock = string.Empty;
                                    if (!string.IsNullOrEmpty(cell) && (transaction.cmbReport_Type == (int)TabReport_Type.Cash || transaction.cmbReport_Type == (int)TabReport_Type.Card))
                                    {
                                        stock = lsTabProduct.Where(w => w.StringDescription.Trim().ToUpper().Contains(cell.Trim().ToUpper())).Select(s => s.StringDescription).FirstOrDefault();
                                        if (!string.IsNullOrEmpty(stock))
                                        {
                                            var msgstock = new Camada.Dominio.Entidades.Stock();
                                            msgstock.AMOUNT = 1;
                                            msgstock.DT_Entrada = DateTime.UtcNow;
                                            msgstock.ID_ACTION_TYPE = (int)TabAction_Type.Sold;
                                            msgstock.ID_PRODUCT_TYPE = Convert.ToInt32(stock);
                                            msgstock.LOGLOGIN = User.Identity.Name;
                                            listStocks.Add(msgstock);
                                        }
                                    }
                                    trans.Description = !string.IsNullOrEmpty(stock) ? stock : cell;
                                }
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

                    decimal totalLastCashier = 0, totalFinal = 0, totalLastCalc = 0;
                    int idLastCalc = 0;

                    var msgTotalLastCalc = Fachada.Repositorio.CALC.GetTotalLastCalc();
                    if (msgTotalLastCalc != null)
                    {
                        totalLastCalc = msgTotalLastCalc.Instance.Total;
                        idLastCalc = msgTotalLastCalc.Instance.ID;
                    }

                    var totalTransaction = new Total_Transactions();
                    totalTransaction.ID_Report_Type = cmbReport_Type;

                    if (cmbReport_Type == (int)TabReport_Type.Cash)
                    {
                        var msgTotalLastCashier = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetLastTotal_Transaction();
                        if (msgTotalLastCashier != null && msgTotalLastCashier.Instance != null && msgTotalLastCashier.Instance.TotalFinal.HasValue)
                        {
                            totalTransaction.ID_Last_Transaction = msgTotalLastCashier.Instance.ID;
                            totalLastCashier = msgTotalLastCashier.Instance.TotalFinal.Value;
                        }

                        totalFinal = totalTransactions + totalLastCashier;
                        totalTransaction.Last_Cashier_Total = totalLastCashier;
                    }
                    else if (cmbReport_Type == (int)TabReport_Type.Vagner)
                    {
                        var msgTotalLastCashierVagner = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetLastTotal_Vagner();
                        if (msgTotalLastCashierVagner != null && msgTotalLastCashierVagner.Instance != null && msgTotalLastCashierVagner.Instance.TotalFinal.HasValue)
                        {
                            totalTransaction.ID_Last_Transaction = msgTotalLastCashierVagner.Instance.ID;
                            totalLastCashier = msgTotalLastCashierVagner.Instance.TotalFinal.Value;
                        }

                        totalFinal = totalTransactions + totalLastCashier;
                        totalTransaction.Last_Cashier_Total = totalLastCashier;
                    }
                    else if (cmbReport_Type == (int)TabReport_Type.Card)
                    {
                        var msgTotalLastCashierCard = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetLastTotal_Card();
                        if (msgTotalLastCashierCard != null && msgTotalLastCashierCard.Instance != null && msgTotalLastCashierCard.Instance.TotalFinal.HasValue)
                        {
                            totalTransaction.ID_Last_Transaction = msgTotalLastCashierCard.Instance.ID;
                            totalLastCashier = msgTotalLastCashierCard.Instance.TotalFinal.Value;
                        }

                        totalFinal = totalTransactions + totalLastCashier;
                        totalTransaction.Last_Cashier_Total = totalLastCashier;

                        DateTime dtDayMonthYearReference = DateTime.UtcNow;
                        if (dtDayMonthYearReference.Hour >= 0 && dtDayMonthYearReference.Hour <= 5)
                            dtDayMonthYearReference = dtDayMonthYearReference.AddDays(-1);

                        totalTransaction.DayMonthYearReference = dtDayMonthYearReference.Date;
                    }

                    if (cmbReport_Type != (int)TabReport_Type.Card)
                    {
                        totalTransaction.ID_Calc = idLastCalc;
                        totalTransaction.DifferenceFinalCalc = totalLastCalc - totalFinal;
                        totalTransaction.DayMonthYearReference = DateTime.UtcNow.Date;
                    }

                    totalTransaction.ID_SHIFT_TYPE = transaction.cmbSHIFT_TYPE;

                    totalTransaction.TotalTransactions = totalTransactions;
                    totalTransaction.TotalFinal = totalFinal;
                    totalTransaction.LogLogin = (User != null && User.Identity != null && !string.IsNullOrEmpty(User.Identity.Name)) ? User.Identity.Name : string.Empty;
                    totalTransaction.DT_Reg = DateTime.UtcNow;



                    var msgSaveTotal_Transactions = Fachada.Negocio.ManterTOTAL_TRANSACTIONS.Salvar(totalTransaction);
                    if (msgSaveTotal_Transactions.Result != Message.ResultType.Success)
                        return Content(msgSaveTotal_Transactions.Description);

                    foreach (var item in listStocks)
                    {
                        item.ID_TOTAL_TRANSACTION = totalTransaction.ID;
                        var msgSaveStock = Fachada.Negocio.ManterSTOCK.Save(item);
                        if (msgSaveStock.Result != Message.ResultType.Success)
                            return Content(msgSaveStock.Description);
                    }

                    foreach (var item in listTrans)
                        item.ID_Total_Transactions = totalTransaction.ID;

                    var msgSaveTransactions = Fachada.Negocio.ManterTRANSACTIONS.SaveList(listTrans.ToArray());
                    if (msgSaveTransactions.Result != Message.ResultType.Success)
                        return Content(msgSaveTransactions.Description);

                    return RedirectToAction("Details", "TotalTransactions", new { id = totalTransaction.ID });
                }
            }

            carregaCombos();
            return View(transaction);
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

            var displayedCompanies = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetAllTotalTransactions(msgTotalTransactions);

            var result = (from c in displayedCompanies.Instances
                          select new[] {
                                            c.ID.ToString(),
                                            c.DT_Reg.ToString(),
                                            UtilsLibrary.GetListEnum(typeof(TabSHIFT_TYPE)).Where(w=>w.Value == c.ID_SHIFT_TYPE.ToString()).Select(s=>s.StringDescription).FirstOrDefault(),
                                            c.LogLogin,
                                            c.TotalTransactions.ToString("C2"),
                                            c.LastTotalCashier.ToString("C2"),
                                            c.TotalFinal.ToString("C2"),
                                            c.TotalCalc.ToString("C2"),
                                            c.DifferenceFinalCalc.ToString("C2"),
                                            UtilsLibrary.GetListEnum(typeof(TabReport_Type)).Where(w=>w.Value == c.ID_Report_Type.ToString()).Select(s=>s.StringDescription).FirstOrDefault(),
                        }).Take(iDisplayLength);

            return Json(new
            {
                sEcho = sEcho,// Variável Padraão do plugin
                iTotalRecords = 50,// Deve realizar outra pesquisa no Banco de Daods sem Filtro de pesquisa e mostar a quantidade de registros no Banco nessa variável "iTotalRecords" no exemplo esta 1000 fixo mas deve ser a qtde total de registro que existe na tabela
                iTotalDisplayRecords = displayedCompanies.Code,//Total de Registro com filtro  Aplicado
                aaData = result // Lista que será exibida do grid
            }, JsonRequestBehavior.AllowGet);

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
