using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class GridTotalTransactionIndex
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:MM:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de registro")]
        public DateTime DT_Reg { get; set; }
        [Display(Name = "User")]
        public string LogLogin { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Transactions")]
        public decimal TotalTransactions { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Calculator")]
        public int IDLastTotalCalc { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Calculator")]
        public decimal TotalCalc { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Last Cashier")]
        public int IDLastTotalCashier { get; set; }
        [Display(Name = "Total Last Cashier")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal LastTotalCashier { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Final")]
        public decimal TotalFinal { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Difference between Cashier and Calculator")]
        public decimal DifferenceFinalCalc { get; set; }
        public int ID_FIRST_PAYMENT_TYPE { get; set; }
        public int ID_Report_Type { get; set; }
        [Display(Name = "Report Type")]
        public string Desc_Report_Type { get; set; }
        [Display(Name = "Cashier Return")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal CashierLastReturn { get; set; }
        [Display(Name = "Bank Envelope")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal BankEnvelope { get; set; }
        public int ID_SHIFT_TYPE { get; set; }
        [Display(Name = "Shift")]
        public string DESCRIPTION_SHIFT_TYPE { get; set; }
        public DateTime? DAY_MONTH_YEAR_REFERENCE { get; set; }
        public int ID_User { get; set; }
    }
}
