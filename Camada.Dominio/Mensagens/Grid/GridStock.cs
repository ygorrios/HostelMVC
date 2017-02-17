using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class GridStock
    {
        public int ID { get; set; }
        [Display(Name = "Amount")]
        public int Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Entrada")]
        public DateTime DT_Reg { get; set; }
        [Display(Name = "User")]
        public string LogLogin { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Action Type")]
        public string Action_Type { get; set; }
    }
}
