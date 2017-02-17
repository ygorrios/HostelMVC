using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class TelaCalc
    {
        public string DescriptionCalcType { get; set; }
        [Display(Name = "Calc Type")]
        [Required(ErrorMessage = "Required.")]
        public int IDCalc_Type { get; set; }
        public string LogLogin { get; set; }
        //[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Calculator")]
        public decimal TotalCalc { get; set; }
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
        [DataType(DataType.Currency)]
        [Display(Name = "Total")]
        public decimal TotalMoney_Count { get; set; }
        public DateTime DT_Reg { get; set; }
    }
}
