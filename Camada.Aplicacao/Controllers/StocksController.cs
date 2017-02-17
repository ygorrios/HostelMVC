using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Camada.Dominio.Entidades;
using Library.Utilities;
using Camada.Dominio;
using Library.Messages;

namespace Camada.Aplicacao.Controllers
{
    [Authorize]
    public class StocksController : Controller
    {
        private HostelEntities db = new HostelEntities();

        // GET: Stocks
        public ActionResult Index()
        {
            return View();
        }

        // GET: Stocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        public ActionResult StockControl()
        {
            return View();
        }

        public JsonResult getGridStock(string sEcho, int iDisplayStart, int iDisplayLength, string sSortDir_0, int iSortCol_0)
        {
            var msgTotalTransactions = new Dominio.Mensagens.Filtro.Stock();
            msgTotalTransactions.iDisplayStart = iDisplayStart;
            msgTotalTransactions.iDisplayLength = iDisplayLength;
            msgTotalTransactions.sSortDir_0 = "desc";// sSortDir_0;
            msgTotalTransactions.iSortCol_0 = iSortCol_0;
            //msgTotalTransactions.Report_Type = Convert.ToInt32(Request["cmbReport_Type"]);
            //msgTotalTransactions.dtInicio = Convert.ToString(Request["txtDtInicio"]);
            //msgTotalTransactions.dtFim = Convert.ToString(Request["txtDtFim"]);
            //msgTotalTransactions.txtNome = Convert.ToString(Request["txtNome"]);

            var displayedCompanies = Fachada.Repositorio.STOCK.GetAllStockByFilter(msgTotalTransactions);

            var result = (from c in displayedCompanies.Instances
                          select new[] {
                                            c.ID.ToString(),
                                            c.DT_Reg.ToString("dd/MM/yyyy HH:MM"),
                                            c.LogLogin,
                                            c.Action_Type,
                                            c.Description,
                                            c.Amount.ToString()
                        }).Take(iDisplayLength);

            return Json(new
            {
                sEcho = sEcho,// Variável Padraão do plugin
                iTotalRecords = 50,// Deve realizar outra pesquisa no Banco de Daods sem Filtro de pesquisa e mostar a quantidade de registros no Banco nessa variável "iTotalRecords" no exemplo esta 1000 fixo mas deve ser a qtde total de registro que existe na tabela
                iTotalDisplayRecords = displayedCompanies.Code,//Total de Registro com filtro  Aplicado
                aaData = result // Lista que será exibida do grid
            }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult getGridStockControl(string sEcho, int iDisplayStart, int iDisplayLength)
        {
            var msgTotalTransactions = new Dominio.Mensagens.Filtro.Stock();
            msgTotalTransactions.iDisplayStart = iDisplayStart;
            msgTotalTransactions.iDisplayLength = iDisplayLength;
            //msgTotalTransactions.Report_Type = Convert.ToInt32(Request["cmbReport_Type"]);
            //msgTotalTransactions.dtInicio = Convert.ToString(Request["txtDtInicio"]);
            //msgTotalTransactions.dtFim = Convert.ToString(Request["txtDtFim"]);
            //msgTotalTransactions.txtNome = Convert.ToString(Request["txtNome"]);

            var displayedCompanies = Fachada.Repositorio.STOCK.GetAllStockControlByFilter(msgTotalTransactions);

            var result = (from c in displayedCompanies.Instances
                          select new[] {
                                            UtilsLibrary.GetListEnum(typeof(TabProduct_Type)).Where(w=>w.StringValue == c.Description.ToString()).Select(s=>s.StringDescription).FirstOrDefault(),
                                            c.Amount.ToString()
                        }).Take(iDisplayLength);

            return Json(new
            {
                sEcho = sEcho,// Variável Padraão do plugin
                iTotalRecords = 50,// Deve realizar outra pesquisa no Banco de Daods sem Filtro de pesquisa e mostar a quantidade de registros no Banco nessa variável "iTotalRecords" no exemplo esta 1000 fixo mas deve ser a qtde total de registro que existe na tabela
                iTotalDisplayRecords = displayedCompanies.Code,//Total de Registro com filtro  Aplicado
                aaData = result // Lista que será exibida do grid
            }, JsonRequestBehavior.AllowGet);

        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            ViewBag.ID_ACTION_TYPE = new SelectList(db.Action_Type, "ID", "DESCRIPTION");
            ViewBag.ID_PRODUCT_TYPE = new SelectList(db.Product_Type, "ID", "Description");
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_PRODUCT_TYPE,AMOUNT,DT_Entrada,LOGLOGIN,ID_ACTION_TYPE")] Dominio.Mensagens.TelaStock stock)
        {
            if (ModelState.IsValid)
            {
                Stock stk = new Stock();
                stk.ID_PRODUCT_TYPE = stock.ID_PRODUCT_TYPE;
                stk.AMOUNT = stock.AMOUNT;
                stk.DT_Entrada = stock.DT_Entrada.AddHours(DateTime.UtcNow.Hour).AddMinutes(DateTime.UtcNow.Minute).AddSeconds(DateTime.UtcNow.Second);
                stk.ID_ACTION_TYPE = stock.ID_ACTION_TYPE;
                stk.LOGLOGIN = User.Identity.Name;

                var msgStock = Fachada.Negocio.ManterSTOCK.Save(stk);
                if (msgStock.Result != Message.ResultType.Success)
                    return Content(msgStock.Description);

                return RedirectToAction("Index");
            }

            ViewBag.ID_ACTION_TYPE = new SelectList(db.Action_Type, "ID", "DESCRIPTION", stock.ID_ACTION_TYPE);
            ViewBag.ID_PRODUCT_TYPE = new SelectList(db.Product_Type, "ID", "Description", stock.ID_PRODUCT_TYPE);
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_ACTION_TYPE = new SelectList(db.Action_Type, "ID", "DESCRIPTION", stock.ID_ACTION_TYPE);
            ViewBag.ID_PRODUCT_TYPE = new SelectList(db.Product_Type, "ID", "Description", stock.ID_PRODUCT_TYPE);
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_PRODUCT_TYPE,AMOUNT,DT_Entrada,LOGLOGIN,ID_ACTION_TYPE")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_ACTION_TYPE = new SelectList(db.Action_Type, "ID", "DESCRIPTION", stock.ID_ACTION_TYPE);
            ViewBag.ID_PRODUCT_TYPE = new SelectList(db.Product_Type, "ID", "Description", stock.ID_PRODUCT_TYPE);
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
            db.SaveChanges();
            return RedirectToAction("Index");
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
