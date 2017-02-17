using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class TelaCargo
    {
        public int identificador { get; set; }
        [Display(Name = "Nome Cargo")]
        public string nomeCargo { get; set; }
        [Display(Name = "Carga Horária")]
        public int? cargaHoraria { get; set; }
        [Display(Name = "Período Carga Horária")]
        public string periodoCargaHoraria { get; set; }
    }
}
