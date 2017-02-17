using Camada.Dominio;
using Camada.Dominio.Entidades;
using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Camada.Aplicacao.Controllers
{
    [Authorize]
    public class TotalTransactionsController : Controller
    {
        private HostelEntities db = new HostelEntities();

        // GET: TotalTransactions
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

            return View(msg.Instance);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var msg = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetTotalTransactionByID(id.Value);
            if (msg.Result != Message.ResultType.Success)
                return Content(msg.Description);
            if (msg.Instance == null)
                return HttpNotFound();

            ViewBag.ID_TOTAL_TRANSACTIONS = id;

            return View(msg.Instance);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var msgStock = Fachada.Negocio.ManterSTOCK.DeleteStockByIDTotal_Transaction(id);
            if (msgStock.Result != Message.ResultType.Success)
                return Content(msgStock.Description);

            var msgTransactions = Fachada.Negocio.ManterTRANSACTIONS.DeleteByIdTotalTransaction(id);
            if (msgTransactions.Result != Message.ResultType.Success)
                return Content(msgTransactions.Description);

            var msgTotalTransaction = Fachada.Negocio.ManterTOTAL_TRANSACTIONS.DeleteById(id);
            if (msgTotalTransaction.Result != Message.ResultType.Success)
                return Content(msgTotalTransaction.Description);

            return RedirectToAction("Index", "Transactions");
        }

        public ActionResult DetailsCalc()
        {
            int id = Convert.ToInt32(!string.IsNullOrEmpty(RouteData.Values["id"].ToString()) && int.TryParse(RouteData.Values["id"].ToString(),out id) ? RouteData.Values["id"]:0);
            if (id == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var msg = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetTotalTransactionByID(id);
            if (msg.Result != Message.ResultType.Success)
                return Content(msg.Description);
            if (msg.Instance == null)
                return HttpNotFound();
            
            return RedirectToAction("IndexByTotalTransactions", "Calc", new { id = msg.Instance.IDLastTotalCalc, idTransaction = id, returnAction = "Details", returnController = "TotalTransactions" });
        }



        public JsonResult getGridTransaction(string sEcho, int iDisplayStart, int iDisplayLength, string sSortDir_0, int iSortCol_0)
        {
            var msgTransactions = new Dominio.Mensagens.Filtro.Transactions();
            msgTransactions.iDisplayStart = iDisplayStart;
            msgTransactions.iDisplayLength = iDisplayLength;
            msgTransactions.sSortDir_0 = sSortDir_0;
            msgTransactions.iSortCol_0 = iSortCol_0;
            msgTransactions.ID_TOTAL_TRANSACTION = Convert.ToInt32(!string.IsNullOrEmpty(RouteData.Values["id"].ToString()) && int.TryParse(RouteData.Values["id"].ToString(),out msgTransactions.ID_TOTAL_TRANSACTION) ? RouteData.Values["id"]:0);

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
                                            c.Card_Type,
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
    }
}