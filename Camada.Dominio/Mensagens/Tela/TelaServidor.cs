using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class TelaServidor
    {
        public int identificador { get; set; }

        [Display(Name = "CPF")]
        public string CPFServidor { get; set; }
        [Display(Name = "Nome")]
        public string nomeServidor { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime? dtNascimentoServidor { get; set; }
        [Display(Name = "Matrícula")]
        public string numeroMatricula { get; set; }
        [Display(Name = "Sexo")]
        public int identificadorTAB_Sexo { get; set; }
        [Display(Name = "Unidade")]
        public int identificadorServidorUnidadeGestora { get; set; }
    }
}
