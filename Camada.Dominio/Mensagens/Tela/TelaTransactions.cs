using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class TelaTransactions
    {
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Transactions")]
        public decimal TotalTransactions { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Calculator")]
        public decimal TotalCalc { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Last Cashier Cash")]
        public decimal TotalLastCashier { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Last Cashier Card")]
        public decimal TotalLastCashierCard { get; set; }
        public int ID_Last_TransactionCard { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Last Cashier Vagner")]
        public decimal TotalLastCashierVagner { get; set; }
        public int ID_Last_TransactionVagner { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Final")]
        public decimal TotalFinal { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Difference between Cashier and Calculator")]
        public decimal DifferenceFinalCalc { get; set; }
        
        public int ID_Last_Transaction { get; set; }
        public int ID_Last_Calc { get; set; }
        public int Report_Type { get; set; }
        [Required(ErrorMessage = "Please, as")]
        public int cmbReport_Type { get; set; }
        public int cmbSHIFT_TYPE { get; set; }
        
        public string txtCopied { get; set; }
        //[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Total Last Cashier Vagner")]
        //public decimal TotalLastCashierVagner { get; set; }

        public int ID { get; set; }
        public DateTime DT_Reg { get; set; }
        public string LogLogin { get; set; }
        public string Reservation_Number { get; set; }
        public string GuestName { get; set; }
        public int ID_Transaction_Type { get; set; }
        public int ID_Payment_Type { get; set; }
        public int ID_Card_Type { get; set; }
        public string Description { get; set; }

        public string DescriptionCalcType { get; set; }
        [Display(Name = "Calc Type")]
        public int IDCalc_Type { get; set; }
        public int IDMoney_Count { get; set; }
        [Display(Name = "€ 0.01")]
        public int Qnt_1_Cent { get; set; }
        [Display(Name = "€ 0.02")]
        public int Qnt_2_Cents { get; set; }
        [Display(Name = "€ 0.05")]
        public int Qnt_5_Cents { get; set; }
        [Display(Name = "€ 0.10")]
        public int Qnt_10_Cents { get; set; }
        [Display(Name = "€ 0.20")]
        public int Qnt_20_Cents { get; set; }
        [Display(Name = "€ 0.50")]
        public int Qnt_50_Cents { get; set; }
        [Display(Name = "€ 1.00")]
        public int Qnt_1_Euro { get; set; }
        [Display(Name = "€ 2.00")]
        public int Qnt_2_Euros { get; set; }
        [Display(Name = "€ 5.00")]
        public int Qnt_5_Euros { get; set; }
        [Display(Name = "€ 10.00")]
        public int Qnt_10_Euros { get; set; }
        [Display(Name = "€ 20.00")]
        public int Qnt_20_Euros { get; set; }
        [Display(Name = "€ 50.00")]
        public int Qnt_50_Euros { get; set; }
        [Display(Name = "€ 100.00")]
        public int Qnt_100_Euros { get; set; }
        [Display(Name = "€ 200.00")]
        public int Qnt_200_Euros { get; set; }
        [Display(Name = "€ 500.00")]
        public int Qnt_500_Euros { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total")]
        public decimal TotalMoney_Count { get; set; }
    }
}
