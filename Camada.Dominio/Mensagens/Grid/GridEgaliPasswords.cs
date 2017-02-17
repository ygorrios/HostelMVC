using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class GridEgaliPasswords
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de registro")]
        public DateTime DT_Reg { get; set; }
        [Display(Name = "Login")]
        public string LogLogin { get; set; }
        [Display(Name = "Item")]
        public string Item { get; set; }
        [Display(Name = "Login")]
        public string Login { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
