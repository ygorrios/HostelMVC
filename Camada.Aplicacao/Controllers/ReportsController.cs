using Camada.Dominio;
using Camada.Dominio.Entidades;
using Camada.Dominio.Mensagens;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Library.Messages;
using Library.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Camada.Aplicacao.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private HostelEntities db = new HostelEntities();

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult IndexShift()
        {
            return View();
        }

        /// <summary>
        /// Load dropDownList
        /// </summary>
        public void carregaCombos()
        {
            var ID_Report_Type = db.Report_Type
                        .Where(w => w.ID != (int)TabReport_Type.Bank)
                        .Select(x =>
                                  new DropDownlist()
                                  {
                                      Value = x.ID,
                                      Text = x.Description
                                  }).ToList();

            var dropDownlistReport = new Camada.Dominio.Mensagens.DropDownlist();
            ID_Report_Type.Add(dropDownlistReport.AdicionarSelecione());
            ViewBag.ID_Report_Type = new SelectList(ID_Report_Type.OrderBy(o => o.Value), "Value", "Text");

            var ID_User = db.Users
                       .Where(w => w.ID != (int)TabReport_Type.Bank)
                       .Select(x =>
                                 new DropDownlist()
                                 {
                                     Value = x.ID,
                                     Text = x.Username
                                 }).ToList();

            var dropDownlistUser = new Camada.Dominio.Mensagens.DropDownlist();
            ID_User.Add(dropDownlistUser.AdicionarSelecione());
            ViewBag.ID_User = new SelectList(ID_User.OrderBy(o => o.Value), "Value", "Text");

            var ID_SHIFT_TYPE = db.SHIFT_TYPE
                        .Select(x =>
                                  new DropDownlist()
                                  {
                                      Value = x.ID,
                                      Text = x.DESCRIPTION
                                  }).ToList();

            var dropDownlistShift = new Camada.Dominio.Mensagens.DropDownlist();
            ID_SHIFT_TYPE.Add(dropDownlistShift.AdicionarSelecione());
            ViewBag.ID_SHIFT_TYPE = new SelectList(ID_SHIFT_TYPE.OrderBy(o => o.Value), "Value", "Text");
        }


        /// <summary>
        /// Report to get statment after each shift
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportEndOfTheDay([Bind(Include = "txtDtInicio, txtDtFim, txtNome, ID_Report_Type, ID_User, ID_SHIFT_TYPE")] Dominio.Mensagens.GridTotalTransactionIndex transaction)
        {
            var msgTotalTransactions = new Dominio.Mensagens.Filtro.TotalTransactions();
            msgTotalTransactions.dtInicio = Convert.ToString(Request["txtDtInicio"]);
            msgTotalTransactions.dtFim = Convert.ToString(Request["txtDtFim"]);
            msgTotalTransactions.txtNome = Convert.ToString(Request["txtNome"]);
            msgTotalTransactions.ID_REPORT_TYPE = transaction.ID_Report_Type;
            msgTotalTransactions.ID_SHIFT_TYPE = transaction.ID_SHIFT_TYPE;

            if (transaction.ID_User > 0)
            {
                var msg = Fachada.Repositorio.USER.GetUserById(transaction.ID_User);
                if (msg != null)
                    msgTotalTransactions.Username = msg.Instance.Username;
            }

            var displayedCompanies = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetReportEndOfTheDayTotalTransactions(msgTotalTransactions);

            string returnFolder = returnFolderPDF("EndOfTheDay.pdf");

            //Create new PDF document
            Document document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(returnFolder, FileMode.Create));

                writer.PageEvent = new PDFUtils();
                document.Open();
                document.Add(createHeader("End Of The Day"));

                decimal total = 0;
                decimal totalReport = 0;

                List<Dominio.Mensagens.GridTotalTransactionIndex> listGTT = new List<Dominio.Mensagens.GridTotalTransactionIndex>();
                DateTime dtNow = DateTime.Now;
                foreach (var item in displayedCompanies.Instances.OrderByDescending(q => q.DAY_MONTH_YEAR_REFERENCE))
                {
                    if (item.DAY_MONTH_YEAR_REFERENCE.HasValue)
                    {
                        if (dtNow != item.DAY_MONTH_YEAR_REFERENCE.Value)
                        {
                            if (listGTT != null && listGTT.Count > 0)
                            {
                                document.Add(createTableEndOfTheDay(listGTT, total));
                                listGTT = new List<Dominio.Mensagens.GridTotalTransactionIndex>();
                                total = 0;
                            }
                            dtNow = item.DAY_MONTH_YEAR_REFERENCE.Value;
                        }

                        listGTT.Add(item);
                        total += item.TotalTransactions;
                        totalReport += item.TotalTransactions;
                    }
                }


                PdfPTable table = new PdfPTable(2);
                table.LockedWidth = true;
                float[] widths = new float[] { 5f, 2f };
                table.SetWidths(widths);
                table.TotalWidth = 300F;
                PdfPCell cell = new PdfPCell();
                cell.Colspan = 2;
                cell.PaddingBottom = 5f;
                cell.Border = 0;
                cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                table.AddCell(cell);

                Font fontTitle2 = new Font(Font.FontFamily.TIMES_ROMAN, 14f, Font.BOLD);
                Font font = new Font(Font.FontFamily.TIMES_ROMAN, 12f, Font.NORMAL);
                table.AddCell(new Phrase("Total End Of The Day: ", fontTitle2));
                table.AddCell(new Phrase(totalReport.ToString("C2"), font));

                table.AddCell(cell);
                document.Add(table);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                document.Close();
            }

            ViewBag.ShowPDF = true;
            return View();
        }

        public ActionResult ReportEndOfTheDay()
        {
            carregaCombos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportShift([Bind(Include = "txtDtInicio, txtDtFim, txtNome")] Dominio.Mensagens.GridTotalTransactionIndex transaction)
        {
            var msgTotalTransactions = new Dominio.Mensagens.Filtro.TotalTransactions();
            msgTotalTransactions.Report_Type = Convert.ToInt32(Request["cmbReport_Type"]);
            msgTotalTransactions.dtInicio = Convert.ToString(Request["txtDtInicio"]);
            msgTotalTransactions.dtFim = Convert.ToString(Request["txtDtFim"]);
            msgTotalTransactions.txtNome = Convert.ToString(Request["txtNome"]);
            //msgTotalTransactions.ID_REPORT_TYPE = (int)TabReport_Type.Bank;

            var displayedCompanies = Fachada.Repositorio.TOTAL_TRANSACTIONS.GetReportEndOfTheDayTotalTransactions(msgTotalTransactions);

            string returnFolder = returnFolderPDF("Shift.pdf");

            //Create new PDF document
            Document document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(returnFolder, FileMode.Create));
                writer.PageEvent = new PDFUtils();
                document.Open();
                document.Add(createHeader("Shifts"));

                List<Dominio.Mensagens.GridTotalTransactionIndex> listGTT = new List<Dominio.Mensagens.GridTotalTransactionIndex>();

                DateTime dtNow = DateTime.Now;
                int idShiftTypeActual = 1;
                foreach (var item in displayedCompanies.Instances)
                {
                    if (item.DAY_MONTH_YEAR_REFERENCE.HasValue)
                    {
                        if (dtNow != item.DAY_MONTH_YEAR_REFERENCE.Value)
                        {
                            if (listGTT != null && listGTT.Count > 0)
                            {
                                document.Add(createTableEndOfTheDay(listGTT, 0));
                                listGTT = new List<Dominio.Mensagens.GridTotalTransactionIndex>();
                            }
                            dtNow = item.DAY_MONTH_YEAR_REFERENCE.Value;
                            listGTT.Add(item);
                        }
                        else
                        {
                            if (item.ID_SHIFT_TYPE != idShiftTypeActual)
                            {
                                document.Add(createTableEndOfTheDay(listGTT, 0));
                                listGTT = new List<Dominio.Mensagens.GridTotalTransactionIndex>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                document.Close();
            }

            ViewBag.ShowPDF = true;
            return View();
        }

        public ActionResult ReportShift()
        {
            return View();
        }

        public ActionResult CreateHouse_Keeping()
        {
            return View();
        }

        public ActionResult PrintHouse_Keeping()
        {
            return View();
        }

        public string returnFolderPDF(string fileName)
        {
            string pastaReport = @"~/Reports/";
            string folderServerMapPath = string.Empty;

            folderServerMapPath = Server.MapPath(pastaReport);
            if (!Directory.Exists(folderServerMapPath))
                Directory.CreateDirectory(folderServerMapPath);
            else
            {
                DirectoryInfo diHouseKeeping = new DirectoryInfo(folderServerMapPath);
                if (diHouseKeeping.Exists)
                {
                    foreach (var item in diHouseKeeping.GetFiles())
                        if (item.Name == fileName)
                            item.Delete();
                }
            }
            return Path.Combine(folderServerMapPath, fileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHouse_Keeping([Bind(Include = "")] House_Keeping house_Keeping)
        {
            if (ModelState.IsValid)
            {
                string txtCopied = Convert.ToString(Request["txtCopied"]);
                DateTime dtDay = DateTime.UtcNow;

                List<House_Keeping> listHK = new List<House_Keeping>();

                foreach (string row in txtCopied.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    int i = 0;
                    House_Keeping houseK = new House_Keeping();
                    if (!string.IsNullOrEmpty(row))
                    {
                        if (row.Contains("Room Number") ||
                            row.Contains("Status") ||
                            row.Contains("Adults"))
                            continue;
                        foreach (string cell in row.Split('\t'))
                        {
                            if (i == 0)
                                houseK.Room_Number = cell;
                            else if (i == 1)
                                houseK.Status = cell;
                            else if (i == 2)
                                houseK.Adults = Convert.ToInt32(cell);

                            houseK.DT_Registro = dtDay;
                            i++;
                        }
                        listHK.Add(houseK);
                    }
                }

                listHK = listHK.OrderBy(o => o.Room_Number).ToList();

                string returnFolder = returnFolderPDF("HouseKeeping.pdf");
                string checkIn = "Check-In:";
                string camasDispo = "Camas Disponíveis:";

                //Create new PDF document
                Document document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
                try
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(returnFolder, FileMode.Create));
                    writer.PageEvent = new PDFUtils();
                    document.Open();

                    List<House_Keeping> listHKA = listHK.Where(q => q.Room_Number.Contains("A")
                    || (q.Room_Number.Contains("B") && !q.Room_Number.Contains("Bed"))
                    || q.Room_Number.Contains("C")).ToList();

                    List<House_Keeping> listHKB = listHK.Where(q => q.Room_Number.Contains("D")
                    || q.Room_Number.Contains("E")
                    || q.Room_Number.Contains("F")).ToList();

                    List<House_Keeping> listHKC = listHK.Where(q => q.Room_Number.Contains("G")
                    || q.Room_Number.Contains("H")
                    || q.Room_Number.Contains("I")
                    || q.Room_Number.Contains("J")
                    || q.Room_Number.Contains("K")).ToList();

                    document.Add(createHeader("House Keeping"));
                    document.Add(createTable(listHKA));
                    document.Add(new Paragraph(checkIn));
                    document.Add(new Paragraph(camasDispo));
                    document.NewPage();

                    document.Add(createHeader("House Keeping"));
                    document.Add(createTable(listHKB));
                    document.Add(new Paragraph(checkIn));
                    document.Add(new Paragraph(camasDispo));
                    document.NewPage();

                    document.Add(createHeader("House Keeping"));
                    document.Add(createTable(listHKC));
                    document.Add(new Paragraph(checkIn));
                    document.Add(new Paragraph(camasDispo));
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    document.Close();
                }
            }
            return RedirectToAction("PrintHouse_Keeping", "Reports");
        }

        public PdfPTable createTable(List<House_Keeping> listHK)
        {
            PdfPTable table = new PdfPTable(3);
            table.TotalWidth = 400f;
            //fix the absolute width of the table
            table.LockedWidth = true;

            //relative col widths in proportions - 1/3 and 2/3
            float[] widths = new float[] { 2f, 5f, 1f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;
            //leave a gap before and after the table
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            PdfPCell cell = new PdfPCell();
            cell.Colspan = 3;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            table.AddCell("Room Number");
            table.AddCell("Status");
            table.AddCell("Adults");
            foreach (var item in listHK)
            {
                table.AddCell(item.Room_Number);
                table.AddCell(item.Status);
                table.AddCell(item.Adults.ToString());
            }

            return table;
        }

        public PdfPTable createTableEndOfTheDay(List<Dominio.Mensagens.GridTotalTransactionIndex> collection, decimal totalEndOfTheDay)
        {
            PdfPTable table = new PdfPTable(9);
            table.TotalWidth = 560f;
            table.LockedWidth = true;

            //relative col widths in proportions - 1/3 and 2/3
            float[] widths = new float[] { 5f, 4f, 4f, 5f, 5f, 5f, 5f, 6f, 3f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;
            //leave a gap before and after the table
            table.SpacingBefore = 20f;
            //table.SpacingAfter = 10f;

            Font fontTitle = new Font(Font.FontFamily.TIMES_ROMAN, 10f, Font.BOLD);
            Font font = new Font(Font.FontFamily.TIMES_ROMAN, 9f, Font.NORMAL);

            PdfPCell cell = new PdfPCell();
            cell.Colspan = 9;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            table.AddCell(new Phrase("Day Month Year Reference", fontTitle));
            table.AddCell(new Phrase("Shift", fontTitle));
            table.AddCell(new Phrase("User", fontTitle));
            table.AddCell(new Phrase("Total Transactions", fontTitle));
            table.AddCell(new Phrase("Total Last Cashier", fontTitle));
            table.AddCell(new Phrase("Total Final", fontTitle));
            table.AddCell(new Phrase("Total Calculator", fontTitle));
            table.AddCell(new Phrase("Difference between Cashier and Calculator", fontTitle));
            table.AddCell(new Phrase("Report Type", fontTitle));

            foreach (var item in collection)
            {
                table.AddCell(new Phrase(item.DAY_MONTH_YEAR_REFERENCE.Value.ToString("dd/MM/yyyy"), font));
                table.AddCell(new Phrase(UtilsLibrary.GetListEnum(typeof(TabSHIFT_TYPE)).Where(w => w.Value == item.ID_SHIFT_TYPE.ToString()).Select(s => s.StringDescription).FirstOrDefault(), font));
                table.AddCell(new Phrase(item.LogLogin, font));
                table.AddCell(new Phrase(item.TotalTransactions.ToString("C2"), font));
                table.AddCell(new Phrase(item.LastTotalCashier.ToString("C2"), font));
                table.AddCell(new Phrase(item.TotalFinal.ToString("C2"), font));
                table.AddCell(new Phrase(item.TotalCalc.ToString("C2"), font));
                table.AddCell(new Phrase(item.DifferenceFinalCalc.ToString("C2"), font));
                table.AddCell(new Phrase(UtilsLibrary.GetListEnum(typeof(TabReport_Type)).Where(w => w.Value == item.ID_Report_Type.ToString()).Select(s => s.StringDescription).FirstOrDefault(), font));
            }

            table.AddCell(new Phrase(string.Empty));
            table.AddCell(new Phrase(string.Empty));
            table.AddCell(new Phrase("Total: ", fontTitle));
            table.AddCell(new Phrase(totalEndOfTheDay.ToString("C2"), font));
            table.AddCell(new Phrase(string.Empty));
            table.AddCell(new Phrase(string.Empty));
            table.AddCell(new Phrase(string.Empty));
            table.AddCell(new Phrase(string.Empty));
            table.AddCell(new Phrase(string.Empty));

            return table;
        }

        public PdfPTable createHeader(string title)
        {
            PdfPTable table = new PdfPTable(4);
            table.LockedWidth = true;
            float[] widths = new float[] { 9f, 7f, 7f, 4f };
            table.SetWidths(widths);
            PdfPCell cell;
            table.TotalWidth = 560F;

            Image img = Image.GetInstance(Server.MapPath(@"~/Content/Images/EgaliHostelLogoHorizontal.png"));
            img.Alignment = Image.LEFT_ALIGN;
            img.ScalePercent(50f);
            cell = new PdfPCell(img);
            cell.PaddingBottom = 5f;
            cell.Border = 0;
            cell.BorderWidthBottom = 1;
            table.AddCell(cell);

            Font fontTitle = new Font(Font.FontFamily.TIMES_ROMAN, 20f, Font.BOLD);
            Font fontTitle2 = new Font(Font.FontFamily.TIMES_ROMAN, 10f, Font.BOLD);
            Font fontTime = new Font(Font.FontFamily.TIMES_ROMAN, 10f, Font.NORMAL);

            cell = new PdfPCell(new Phrase(title, fontTitle));
            cell.HorizontalAlignment = 2;
            cell.Border = 0;
            cell.BorderWidthBottom = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Datetime: ", fontTitle2));
            cell.HorizontalAlignment = 2;
            cell.Border = 0;
            cell.BorderWidthBottom = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(DateTime.UtcNow.ToString("dd/MM/yyyy - HH:MM"), fontTime));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            cell.BorderWidthBottom = 1;
            table.AddCell(cell);

            return table;
        }
    }
}