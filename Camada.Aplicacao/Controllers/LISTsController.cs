using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Camada.Dominio.Entidades;
using iTextSharp.text;
using System.IO;
using Library.Utilities;
using Camada.Dominio;
using Library.Messages;
using System.Web.Routing;

namespace Camada.Aplicacao.Controllers
{
    public class LISTsController : Controller
    {
        private HostelEntities db = new HostelEntities();

        //Upload image
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);

                byte[] uploadedFile = new byte[file.InputStream.Length];
                file.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                int id = Convert.ToInt32(Request["ID"]);
                var list = db.LISTs.Where(w => w.ID == id).FirstOrDefault();
                var msgL = new LIST();

                var doc = new DOCUMENT();
                doc.DT_INSERT = DateTime.UtcNow;
                doc.FILE_CONTENT = uploadedFile;
                doc.FILE_NAME = list.LAST_NAME_GUEST;
                doc.FILE_TYPE = file.FileName.Split('.')[1];

                var msgDoc = Fachada.Negocio.ManterDOCUMENT.Save(doc);
                if (msgDoc.Result != Message.ResultType.Success)
                    return Content(msgDoc.Description);

                msgL.ID = list.ID;
                msgL.ID_LIST_TYPE = list.ID_LIST_TYPE;
                msgL.DT_START_BLOCK = list.DT_START_BLOCK;
                msgL.DT_START_BOOK = list.DT_START_BOOK;
                msgL.FIRST_NAME_GUEST = list.FIRST_NAME_GUEST;
                msgL.LAST_NAME_GUEST = list.LAST_NAME_GUEST;
                msgL.LOGLOGIN = list.LOGLOGIN;
                msgL.NOTES = Convert.ToString(Request["txtNotes"]);
                msgL.ID_DOCUMENT = doc.ID;

                var msgSaveList = Fachada.Negocio.ManterLIST.Save(msgL);
                if (msgSaveList.Result != Message.ResultType.Success)
                    return Content(msgSaveList.Description);

                return RedirectToAction("IndexVerifyBooking");
            }

            return RedirectToAction("EditVerifyBooking", "LISTS", new { id = Convert.ToInt32(Request["ID"]), uploadImage = true });

        }

        public ActionResult FileUploadBlacklist(HttpPostedFileBase file)
        {
            var firstName = Convert.ToString(Request["FIRST_NAME_GUEST"]);
            var lastName = Convert.ToString(Request["LAST_NAME_GUEST"]);
            var doc = new DOCUMENT();

            if (file != null && file.ContentLength > 0)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);

                byte[] uploadedFile = new byte[file.InputStream.Length];
                file.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                doc.DT_INSERT = DateTime.UtcNow;
                doc.FILE_CONTENT = uploadedFile;
                doc.FILE_NAME = lastName;
                doc.FILE_TYPE = file.FileName.Split('.')[1];

                var msgDoc = Fachada.Negocio.ManterDOCUMENT.Save(doc);
                if (msgDoc.Result != Message.ResultType.Success)
                    return Content(msgDoc.Description);
            }

            var msgL = new LIST();
            if (!string.IsNullOrEmpty(Request["CHECK_IN"]))
                msgL.CHECK_IN = Convert.ToDateTime(Request["CHECK_IN"]);
            if (!string.IsNullOrEmpty(Request["CHECK_OUT"]))
                msgL.CHECK_OUT = Convert.ToDateTime(Request["CHECK_OUT"]);

            if (!string.IsNullOrEmpty(Request["NOTES"]))
                msgL.NOTES = Convert.ToString(Request["NOTES"]);

            msgL.ID_LIST_TYPE = (int)TabList_Type.BLACKLIST;
            msgL.FIRST_NAME_GUEST = firstName;
            msgL.LAST_NAME_GUEST = lastName;
            msgL.LOGLOGIN = User.Identity.Name;
            if (doc.ID > 0)
                msgL.ID_DOCUMENT = doc.ID;

            var msgSaveList = Fachada.Negocio.ManterLIST.Save(msgL);
            if (msgSaveList.Result != Message.ResultType.Success)
                return Content(msgSaveList.Description);

            return RedirectToAction("IndexBlacklist");
        }

        public string getLastFirstName(string txtCopied)
        {
            string listLastFirstName = string.Empty;

            string[] splitTxtCopied = txtCopied.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            int countSplit = 0;
            DateTime dtTest = DateTime.Now;

            foreach (string row in splitTxtCopied)
            {
                if (countSplit == 0 && DateTime.TryParse(row, out dtTest))
                {
                    countSplit++;
                    continue;
                }
                else if (countSplit == 0)
                    continue;
                else if (countSplit == 1)
                {
                    if (!string.IsNullOrEmpty(row))
                        listLastFirstName += row.Trim() + "|";

                    countSplit++;
                    continue;
                }
                else if (countSplit == 2 || countSplit == 3 || countSplit == 4)
                {
                    if (countSplit == 4)
                    {
                        countSplit = 1;
                        continue;
                    }
                    countSplit++;
                    continue;
                }
            }

            return listLastFirstName.Remove(listLastFirstName.Length - 1, 1);
        }

        //getGrid
        private JsonResult getJsonResult(string sEcho, int iDisplayStart, int iDisplayLength, string sSortDir_0, int iSortCol_0, int ID_LIST_TYPE)
        {
            var firstName = Convert.ToString(Request["txtFirstName"]);
            var lastName = Convert.ToString(Request["txtLastName"]);
            var lastFirstName = Convert.ToString(Request["txtCopied"]);
            string[] listLastFirstName = new string[] { };
            if (!string.IsNullOrEmpty(lastFirstName))
                listLastFirstName = lastFirstName.Split('|');

            var displayedCompanies = Fachada.Repositorio.LIST.GetAllListsByFilter(new Dominio.Mensagens.Filtro.Lists
            {
                //Dados Padrão da Paginação Coluna Clicada, Ordenação ASC DESC, Skip, Take, 
                iDisplayStart = iDisplayStart,
                iDisplayLength = iDisplayLength,
                sSortDir_0 = sSortDir_0,
                iSortCol_0 = iSortCol_0,
                ID_LIST_TYPE = ID_LIST_TYPE,
                firstName = firstName,
                lastName = lastName,
                lastFirstName = listLastFirstName
            });


            string pastaImages = @"~/Images/Profile/";
            string folderServerMapPath = string.Empty;

            folderServerMapPath = Server.MapPath(pastaImages);
            if (!Directory.Exists(folderServerMapPath))
                Directory.CreateDirectory(folderServerMapPath);

            foreach (var item in displayedCompanies.Instances)
            {
                string url = string.Empty;
                string altText = item.LAST_NAME_GUEST;

                string physicalPath = Path.Combine(folderServerMapPath, item.FullFileName);

                DirectoryInfo diDocument = new DirectoryInfo(folderServerMapPath);
                if (diDocument.Exists)
                {
                    bool existe = false;
                    foreach (var iD in diDocument.GetFiles())
                    {
                        if (iD.Name == item.FullFileName)
                        {
                            existe = true;
                            break;
                        }
                    }
                    if (!existe)
                        System.IO.File.WriteAllBytes(physicalPath, item.FILE_CONTENT);
                }

                var qUrl = Url.Content(Path.Combine(pastaImages, item.FullFileName));
                item.htmlImage = string.Format("<img src=\"{0}\" alt=\"{1}\" id=\"puscicaL_0\" class=\"puscica puscical\" style=\"opacity:1;width:120px;\" />", qUrl, altText);
            }

            IEnumerable<string[]> result = Enumerable.Empty<string[]>();
            if (ID_LIST_TYPE == (int)TabList_Type.VERIFY_BOOKING)
            {
                result = (from l in displayedCompanies.Instances
                          select new[] {
                                            l.ID.ToString(),
                                            l.LOGLOGIN,
                                            l.FullName,
                                            l.DT_START_BLOCK.Value.ToString("dd/MM/yyyy"),
                                            l.DT_START_BOOK.Value.ToString("dd/MM/yyyy"),
                                            l.NOTES,
                                            l.htmlImage
                            }).Take(iDisplayLength);
            }
            else if (ID_LIST_TYPE == (int)TabList_Type.BLACKLIST)
            {
                result = (from l in displayedCompanies.Instances
                          select new[] {
                                            l.ID.ToString(),
                                            l.LOGLOGIN,
                                            l.FullName,
                                            l.CHECK_IN.HasValue ? l.CHECK_IN.Value.ToString("dd/MM/yyyy") : "",
                                            l.CHECK_OUT.HasValue ? l.CHECK_OUT.Value.ToString("dd/MM/yyyy"): "",
                                            l.NOTES,
                                            l.htmlImage
                            }).Take(iDisplayLength);
            }
            else
            {
                result = (from l in displayedCompanies.Instances
                          select new[] {
                                            l.ID.ToString(),
                                            l.LOGLOGIN,
                                            l.FullName,
                                            l.CHECK_IN.HasValue ? l.CHECK_IN.Value.ToString("dd/MM/yyyy") : "",
                                            l.CHECK_OUT.HasValue ? l.CHECK_OUT.Value.ToString("dd/MM/yyyy"): "",
                                            l.NOTES,
                                            l.htmlImage
                            }).Take(iDisplayLength);
            }

            return Json(new
            {
                sEcho = sEcho,// Variável Padraão do plugin
                iTotalRecords = 50,// Deve realizar outra pesquisa no Banco de Daods sem Filtro de pesquisa e mostar a quantidade de registros no Banco nessa variável "iTotalRecords" no exemplo esta 1000 fixo mas deve ser a qtde total de registro que existe na tabela
                iTotalDisplayRecords = displayedCompanies.Code,//Total de Registro com filtro  Aplicado
                aaData = result // Lista que será exibida do grid
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGridVerifyLimit(string sEcho, int iDisplayStart, int iDisplayLength, string sSortDir_0, int iSortCol_0)
        {
            return getJsonResult(sEcho, iDisplayStart, iDisplayLength, sSortDir_0, iSortCol_0, (int)TabList_Type.VERIFY_BOOKING);
        }

        public JsonResult GetGridBlacklist(string sEcho, int iDisplayStart, int iDisplayLength, string sSortDir_0, int iSortCol_0)
        {
            return getJsonResult(sEcho, iDisplayStart, iDisplayLength, sSortDir_0, iSortCol_0, (int)TabList_Type.BLACKLIST);
        }

        // GET: LISTs
        public ActionResult IndexVerifyBooking()
        {
            return View();
        }
        public ActionResult IndexBlacklist()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "")] Total_Transactions transaction)
        {
            if (ModelState.IsValid)
            {
                string lastFirstName = Convert.ToString(Request["txtCopied"]);
                string listLastFirstName = string.Empty;
                if (!string.IsNullOrEmpty(lastFirstName))
                    listLastFirstName = getLastFirstName(lastFirstName);

                return RedirectToAction("IndexValid", "LISTS", new { msg = listLastFirstName });
            }
            //else
            //return RedirectToAction("CreateVerifyBooking", "LISTS", new { msg = "Guest encontra-se dentro do limite dos " + txtDays + " Dias" });

            return View();
        }

        public ActionResult IndexValid(string msg)
        {
            ViewBag.txtCopied = msg;
            return View();
        }


        public JsonResult GetGridIndexLists(string sEcho, int iDisplayStart, int iDisplayLength, string sSortDir_0, int iSortCol_0)
        {
            return getJsonResult(sEcho, iDisplayStart, iDisplayLength, sSortDir_0, iSortCol_0, 0);
        }

        // GET: LISTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIST lIST = db.LISTs.Find(id);
            if (lIST == null)
            {
                return HttpNotFound();
            }
            return View(lIST);
        }

        // GET: LISTs/Create
        public ActionResult CreateVerifyBooking(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
                ViewBag.StatusSucess = msg;

            ViewBag.ID_LIST_TYPE = new SelectList(db.LIST_TYPE, "ID", "DESCRIPTION");
            return View();
        }

        public ActionResult CreateBlacklist(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
                ViewBag.StatusSucess = msg;

            return View();
        }

        public ActionResult EditVerifyBooking(int id, bool uploadImage = false)
        {
            ViewBag.uploadImage = uploadImage;
            ViewBag.ID = id;

            ViewBag.ID_LIST_TYPE = new SelectList(db.LIST_TYPE, "ID", "DESCRIPTION");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVerifyBooking([Bind(Include = "")] Total_Transactions transaction)
        {
            if (ModelState.IsValid)
            {
                string txtCopied = Convert.ToString(Request["txtCopied"]);
                int txtDays = Convert.ToInt32(Request["txtDays"]);


                int cmbReport_Type = Convert.ToInt32(Request["cmbReport_Type"]);
                decimal totalTransactions = 0;
                var listStocks = new List<Camada.Dominio.Entidades.Stock>();
                List<LIST> listLITS = new List<LIST>();
                string[] splitTxtCopied = txtCopied.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                int countSplit = 0;
                DateTime dtTest = DateTime.Now;
                LIST list = new LIST();

                foreach (string row in splitTxtCopied)
                {
                    if (countSplit == 0 && DateTime.TryParse(row, out dtTest))
                    {
                        countSplit++;
                        continue;
                    }
                    else if (countSplit == 0)
                        continue;
                    if (countSplit == 2 ||
                        countSplit == 3)
                    {
                        countSplit++;
                        continue;
                    }

                    int i = 0;
                    if (!string.IsNullOrEmpty(row))
                    {
                        string[] splitRow = row.Split('\t');
                        if (splitRow.Count() > 1)
                        {
                            foreach (string cell in splitRow)
                            {
                                if (i == 1)
                                    list.CHECK_IN = Convert.ToDateTime(cell);
                                else if (i == 2)
                                {
                                    list.CHECK_OUT = Convert.ToDateTime(cell);
                                    listLITS.Add(list);
                                    list = new LIST();
                                }
                                i++;
                            }
                        }
                        else
                        {
                            if (countSplit == 1)
                            {
                                list.FIRST_NAME_GUEST = row.Split(',')[0];
                                list.LAST_NAME_GUEST = row.Split(',')[1];
                            }
                        }
                    }
                    countSplit++;
                    if (countSplit >= 4)
                        countSplit = 0;
                }

                int totalDifference = 0;
                foreach (var item in listLITS)
                {
                    int aux = (item.CHECK_OUT.Value - item.CHECK_IN.Value).Days;
                    if (aux < 0)
                        aux = (item.CHECK_OUT.Value.AddYears(1) - item.CHECK_IN.Value).Days;
                    totalDifference += aux;
                }

                if (totalDifference > txtDays)
                {
                    list = new LIST();
                    list.DT_START_BLOCK = listLITS.OrderByDescending(o => o.CHECK_OUT).FirstOrDefault().CHECK_OUT;
                    list.DT_START_BOOK = list.DT_START_BLOCK.Value.AddMonths(1);
                    list.FIRST_NAME_GUEST = listLITS.FirstOrDefault().FIRST_NAME_GUEST;
                    list.LAST_NAME_GUEST = listLITS.FirstOrDefault().LAST_NAME_GUEST;
                    list.ID_LIST_TYPE = Convert.ToInt32(UtilsLibrary.GetListEnum(typeof(TabList_Type)).Where(w => w.Name == TabList_Type.VERIFY_BOOKING.ToString()).Select(s => s.StringValue).FirstOrDefault());
                    list.LOGLOGIN = User.Identity.Name;

                    var msgSaveList = Fachada.Negocio.ManterLIST.Save(list);
                    if (msgSaveList.Result != Message.ResultType.Success)
                        return Content(msgSaveList.Description);

                    return RedirectToAction("EditVerifyBooking", "LISTS", new { id = list.ID, uploadImage = true });
                }
                else
                    return RedirectToAction("CreateVerifyBooking", "LISTS", new { msg = "Guest encontra-se dentro do limite dos " + txtDays + " Dias" });
            }
            return View();
        }

        // GET: LISTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIST lIST = db.LISTs.Find(id);
            if (lIST == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_LIST_TYPE = new SelectList(db.LIST_TYPE, "ID", "DESCRIPTION", lIST.ID_LIST_TYPE);
            return View(lIST);
        }

        // POST: LISTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FILE_NAME,FILE_SIZE,FILE_TYPE,FILE_CONTENT,DT_INSERT,DT_DELETE,ID_LIST_TYPE,CHECK_IN,CHECK_OUT,DT_START_BLOCK,DT_START_BOOK,NOTES,NAME_GUEST,LOGLOGIN")] LIST lIST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lIST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexVerifyBooking");
            }
            ViewBag.ID_LIST_TYPE = new SelectList(db.LIST_TYPE, "ID", "DESCRIPTION", lIST.ID_LIST_TYPE);
            return View(lIST);
        }

        // GET: LISTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIST lIST = db.LISTs.Find(id);
            if (lIST == null)
            {
                return HttpNotFound();
            }
            return View(lIST);
        }

        // POST: LISTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LIST lIST = db.LISTs.Find(id);
            db.LISTs.Remove(lIST);
            db.SaveChanges();
            return RedirectToAction("IndexVerifyBooking");
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
