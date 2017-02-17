using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class TelaPasswords
    {
        public int ID_EGALI_PASSWORDS { get; set; }
        public int ID_EGALI_PASSWORDS_HISTORY { get; set; }
        public int ID { get; set; }
        [Display(Name = "Item")]
        public string Item { get; set; }
        [Display(Name = "Login")]
        public string Login { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        public System.DateTime DT_Reg { get; set; }
        public string LogLogin { get; set; }
    }
}
