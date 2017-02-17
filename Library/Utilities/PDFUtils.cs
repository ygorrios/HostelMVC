using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Library.Utilities
{
    public class PDFUtils : PdfPageEventHelper
    {
        //Everytime when I create the PDF
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            base.OnOpenDocument(writer, document);
        }
        
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }
        
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 2F });
            PdfPCell cell;
            tabFot.TotalWidth = 560F;
            cell = new PdfPCell(new Phrase("Page: " + document.PageNumber));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            cell.BorderWidthTop = 1;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 5, document.Bottom, writer.DirectContent);
        }
        
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }
}
