using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class GridAtosPessoalConsulta
    {
        public int identificador { get; set; }

        //[StringLength(5, ErrorMessage = "Número ato não pode ter mais que 5 digitos.")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Calc")]
        public decimal TotalCalc { get; set; }
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "Data de registro")]
        public string DT_reg { get; set; }
        [Display(Name = "Calculator Type")]
        public string Calc_Type { get; set; }
        [Display(Name = "User")]
        public string LogLogin { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total")]
        public string TotalMoney_Count { get; set; }
    }
}
