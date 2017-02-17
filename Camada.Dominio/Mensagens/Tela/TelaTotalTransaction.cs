using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class TelaTotalTransaction
    {
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
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Last Cashier")]
        public decimal LastTotalCashier { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Final")]
        public decimal TotalFinal { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Difference between Cashier and Calculator")]
        public decimal DifferenceFinalCalc { get; set; }
        
        public int ID { get; set; }
        public DateTime DT_Reg { get; set; }
        public string LogLogin { get; set; }
        public int ID_TOTAL_TRANSACTIONS { get; set; }

        [Display(Name = "Cashier Return")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal CashierLastReturn { get; set; }
        [Display(Name = "Bank Envelope")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal BankEnvelope { get; set; }
    }
}
