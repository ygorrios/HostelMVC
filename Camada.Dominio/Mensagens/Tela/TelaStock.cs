using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class TelaStock
    {
        public int ID { get; set; }
        [Display(Name = "Amount")]
        public int AMOUNT { get; set; }
        [Display(Name = "Data Entrada")]
        public DateTime DT_Entrada { get; set; }
        [Display(Name = "User")]
        public string LOGLOGIN { get; set; }
        [Display(Name = "Product Type")]
        public int ID_PRODUCT_TYPE { get; set; }
        [Display(Name = "Action Type")]
        public int ID_ACTION_TYPE { get; set; }
    }
}
