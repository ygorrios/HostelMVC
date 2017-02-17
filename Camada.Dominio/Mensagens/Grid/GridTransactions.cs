using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class GridTransactions
    {

        //[StringLength(5, ErrorMessage = "Número ato não pode ter mais que 5 digitos.")]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "Data de registro")]
        public string Date { get; set; }
        public string User { get; set; }
        public string Reservation_Number { get; set; }
        public string Guest_Name { get; set; }
        public string Transaction_Type { get; set; }
        public string Payment_Type { get; set; }
        public string Card_Type { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Total { get; set; }
        public int ID_TOTAL_TRANSACTIONS { get; set; }
    }
}
