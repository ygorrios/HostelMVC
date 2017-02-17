using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class TelaTempoServico
    {
        public int identificador { get; set; }
        [Display(Name = "Quantidade de Dias")]
        public int quantidadeDias { get; set; }
        public int identificadorTAB_OrigemTempoServico { get; set; }
        public int identificadorSFI_AtoPessoal { get; set; }
        public int identificadorUnidadeGestora { get; set; }
        [Required(ErrorMessage = "O campo 'Anos' é obrigatório")]
        [StringLength(2)]
        [Display(Name = "Anos")]
        public int anos { get; set; }
        [StringLength(2)]
        [Display(Name = "Meses")]
        public int meses { get; set; }
        [StringLength(2)]
        [Display(Name = "Dias")]
        public int dias { get; set; }
        [Display(Name = "Data Inicial")]
        public DateTime? dtInicial { get; set; }
        [Display(Name = "Data Final")]
        public DateTime? dtFinal { get; set; }

    }
}
