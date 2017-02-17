using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class TelaProventos
    {
        public int identificador { get; set; }
        [Display(Name = "Mes/Ano Refêrencia")]
        public int anoMesReferencia { get; set; }
        [Display(Name = "Sequência Provento")]
        public int sequencialProventoDesconto { get; set; }
        [Display(Name = "Código Proventos")]
        public int codigoProventoDesconto { get; set; }
        [Display(Name = "Valor do provento")]
        public decimal valorProventoDesconto { get; set; }
        [Display(Name = "Nome do Provento")]
        public string nomeProventoDesconto { get; set; }
        [Display(Name = "Descrição do proventoss")]
        public string descricaoProventoDesconto { get; set; }
        [Display(Name = "Competência")]
        public int competencia { get; set; }
        public int identificadorTAB_TipoProventosDesconto { get; set; }
        public int identificadorSFI_AtoPessoal { get; set; }
        public int identificadorUnidadeGestora { get; set; }
    }
}
