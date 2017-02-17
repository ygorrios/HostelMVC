using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class GridCalc
    {

        //[StringLength(5, ErrorMessage = "Número ato não pode ter mais que 5 digitos.")]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de registro")]
        public DateTime DT_reg { get; set; }
        [Display(Name = "Calculator Type")]
        public int ID_Calc_Type { get; set; }
        public string Calc_Type { get; set; }
        [Display(Name = "User")]
        public string LogLogin { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total")]
        public string Total { get; set; }
    }
}
