using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class TelaAto
    {
        public int identificador { get; set; }
        [Display(Name = "Lotação")]
        public string descricaoLotacao { get; set; }
        [Display(Name = "Grupo/Nível/Referência")]
        public string classeGrupoNivelReferencia { get; set; }
        [Display(Name = "Nº do Ato")]
        public string numeroAto { get; set; }
        [Display(Name = "Data Publicação")]
        public DateTime? dataPublicacao { get; set; }
        [Display(Name = "Data Vigência")]
        public DateTime? dataInicioVigenciaAto { get; set; } //DATA VIGÊNCIA
        public int identificadorUnidadeGestora { get; set; }
        [Display(Name = "Número Processo")]
        public int? numeroProcesso { get; set; }
        public int? numeroProtocolo { get; set; }
        public string codigoAcesso { get; set; }
        [Display(Name = "Cargo")]
        public string descricaoCargo { get; set; }
        public int? identificador_Solicitacao { get; set; }
        public int identificadorSFI_Servidor { get; set; }
        public int identificadorSFI_Cargo { get; set; }
        public virtual TelaAtoAposentadoria AtoAposentadoria { get; set; }
        public virtual TelaAtoPensao AtoPensao { get; set; }
        public virtual TelaCargo Cargo { get; set; }
        public virtual List<TelaProventos> Proventos { get; set; }
        public virtual TelaServidor Servidor { get; set; }
        public virtual List<TelaTempoServico> TempoServico { get; set; }
    }
}
