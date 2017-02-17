using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Camada.Dominio.Entidades;
using Library.Messages;
using Camada.Dominio.Mensagens;
using Camada.Aplicacao.Controllers.ActionsFilters;

namespace Camada.Aplicacao.Controllers
{
    [Authorize]
    [LogActionFilter]
    [UtilsActionFilter]
    public class EGALI_PASSWORDSController : Controller
    {
        private HostelEntities db = new HostelEntities();

        // GET: EGALI_PASSWORDS
        public ActionResult Index()
        {
            //var eGALI_PASSWORDS = db.EGALI_PASSWORDS.Include(e => e.EGALI_PASSWORDS2);
            return View();
        }

        public JsonResult GetGridIndexEgali_Password(string sEcho, int iDisplayStart, int iDisplayLength)
        {
            var displayedCompanies = Fachada.Repositorio.EGALI_PASSWORDS.GetAllEgaliPasswordsByFilter(new Dominio.Mensagens.Filtro.EgaliPasswords
            {
                //Dados Padrão da Paginação Coluna Clicada, Ordenação ASC DESC, Skip, Take, 
                iDisplayStart = iDisplayStart,
                iDisplayLength = iDisplayLength,
                item = Convert.ToString(Request["txtItem"]),
                login = Convert.ToString(Request["txtLogin"]),
            });

            var result = (from c in displayedCompanies.Instances
                          select new[] {
                                            c.ID.ToString(),
                                            c.DT_Reg.ToString("dd/MM/yyyy HH:MM"),
                                            c.LogLogin,
                                            c.Item,
                                            c.Login,
                                            c.Password
                            }).Take(10);

            return Json(new
            {
                sEcho = sEcho,// Variável Padraão do plugin
                iTotalRecords = 50,// Deve realizar outra pesquisa no Banco de Daods sem Filtro de pesquisa e mostar a quantidade de registros no Banco nessa variável "iTotalRecords" no exemplo esta 1000 fixo mas deve ser a qtde total de registro que existe na tabela
                iTotalDisplayRecords = displayedCompanies.Code,//Total de Registro com filtro  Aplicado
                aaData = result // Lista que será exibida do grid
            }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetGridEditEgali_Password(string sEcho, int iDisplayStart, int iDisplayLength)
        {
            var displayedCompanies = Fachada.Repositorio.EGALI_PASSWORDS.GetEgaliPasswordsByFilter(new Dominio.Mensagens.Filtro.EgaliPasswords
            {
                //Dados Padrão da Paginação Coluna Clicada, Ordenação ASC DESC, Skip, Take, 
                iDisplayStart = iDisplayStart,
                iDisplayLength = iDisplayLength,
                ID = Convert.ToInt32(Request["ID"])
            });

            var result = (from c in displayedCompanies.Instances
                          select new[] {
                                            c.ID.ToString(),
                                            c.DT_Reg.ToString("dd/MM/yyyy HH:MM"),
                                            c.LogLogin,
                                            c.Item,
                                            c.Login,
                                            c.Password
                            }).Take(10);

            return Json(new
            {
                sEcho = sEcho,// Variável Padraão do plugin
                iTotalRecords = 50,// Deve realizar outra pesquisa no Banco de Daods sem Filtro de pesquisa e mostar a quantidade de registros no Banco nessa variável "iTotalRecords" no exemplo esta 1000 fixo mas deve ser a qtde total de registro que existe na tabela
                iTotalDisplayRecords = displayedCompanies.Code,//Total de Registro com filtro  Aplicado
                aaData = result // Lista que será exibida do grid
            }, JsonRequestBehavior.AllowGet);

        }

        // GET: EGALI_PASSWORDS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EGALI_PASSWORDS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TelaPasswords eGALI_PASSWORDS)
        {
            if (ModelState.IsValid)
            {
                var eP = new EGALI_PASSWORDS();
                eP.Item = eGALI_PASSWORDS.Item;
                eP.Login = eGALI_PASSWORDS.Login;

                var msgEP = Fachada.Negocio.ManterEGALI_PASSWORDS.Save(eP);
                if (msgEP.Result != Message.ResultType.Success)
                    return Content(msgEP.Description);

                var ePH = new EGALI_PASSWORDS_HISTORY();
                ePH.ID_EGALI_PASSWORDS = eP.ID;
                ePH.Password = eGALI_PASSWORDS.Password;
                ePH.LogLogin = User.Identity.Name;
                ePH.DT_Reg = DateTime.UtcNow;

                var msgEPH = Fachada.Negocio.ManterEGALI_PASSWORDS_HISTORY.Save(ePH);
                if (msgEPH.Result != Message.ResultType.Success)
                    return Content(msgEPH.Description);

                return RedirectToAction("Index");
            }
            return View(eGALI_PASSWORDS);
        }

        // GET: EGALI_PASSWORDS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var msg = Fachada.Repositorio.EGALI_PASSWORDS.GetEgaliPasswordsByIdEgaliPasswordsHistory(id.Value);
            if (msg.Result != Message.ResultType.Success)
                return Content(msg.Description);
            if (msg == null || msg.Instance == null)
                return HttpNotFound();

            ViewBag.ID_EGALI_PASSWORDS = msg.Instance.ID;

            return View(msg.Instance);
        }

        // POST: EGALI_PASSWORDS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Item,Login,Password,ID_ORIGINAL, ID_EGALI_PASSWORDS")] TelaPasswords eGALI_PASSWORDS)
        {
            if (ModelState.IsValid)
            {
                var ePH = new EGALI_PASSWORDS_HISTORY();
                //ePH.ID_EGALI_PASSWORDS = Convert.ToInt32(Request["ID"]);
                ePH.ID_EGALI_PASSWORDS = eGALI_PASSWORDS.ID_EGALI_PASSWORDS;
                ePH.Password = eGALI_PASSWORDS.Password;
                //ePH.Password = Convert.ToString(Request["Password"]);
                ePH.LogLogin = User.Identity.Name;
                ePH.DT_Reg = DateTime.UtcNow;

                var msgSaveTransactions = Fachada.Negocio.ManterEGALI_PASSWORDS_HISTORY.Save(ePH);
                if (msgSaveTransactions.Result != Message.ResultType.Success)
                    return Content(msgSaveTransactions.Description);

                return RedirectToAction("Index");
            }

            return View(eGALI_PASSWORDS);
        }

        // GET: EGALI_PASSWORDS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var msg = Fachada.Repositorio.EGALI_PASSWORDS.GetEgaliPasswordsByIdEgaliPasswordsHistory(id.Value);
            if (msg.Result != Message.ResultType.Success)
                return Content(msg.Description);
            if (msg == null || msg.Instance == null)
                return HttpNotFound();

            ViewBag.ID_EGALI_PASSWORDS = msg.Instance.ID;

            return View(msg.Instance);
        }

        // POST: EGALI_PASSWORDS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int idEgaliPasswords = (from eph in db.EGALI_PASSWORDS_HISTORY where eph.ID == id select eph.ID_EGALI_PASSWORDS).FirstOrDefault();

            EGALI_PASSWORDS eGALI_PASSWORDS = db.EGALI_PASSWORDS.Find(idEgaliPasswords);
            if (eGALI_PASSWORDS == null)
                return HttpNotFound();

            var EP = new EGALI_PASSWORDS();
            EP.ID = eGALI_PASSWORDS.ID;
            EP.Item = eGALI_PASSWORDS.Item;
            EP.Login = eGALI_PASSWORDS.Login;
            EP.DT_Delete = DateTime.UtcNow;

            var msgEP = Fachada.Negocio.ManterEGALI_PASSWORDS.Save(EP);
            if (msgEP.Result != Message.ResultType.Success)
                return Content(msgEP.Description);

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
