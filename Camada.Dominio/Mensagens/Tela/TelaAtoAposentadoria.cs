using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class TelaAtoAposentadoria
    {
        public int identificador { get; set; }
        [Display(Name = "Acumulação de Cargo/Proventos")]
        public bool? IndicativoAcumulacaoCargo { get; set; }
        [Display(Name = "Data de Ingresso no Cargo de Aposentadoria")]
        public DateTime? dataIngressoCargoAtual { get; set; }
        [Display(Name = "Data de Ingresso no Serviço Público")]
        public DateTime? DataUltimoIngressoServPublico { get; set; }
        [Display(Name = "Data de Ingresso na Carreira")]
        public DateTime? dataIngressoCarreira { get; set; }
        [Display(Name = "Modalidade")]
        public int identificadorTAB_ModalidadeAposentadoria { get; set; }
        [Display(Name = "Fundamento Legal")]
        public string descricaoFundamentoLegal { get; set; }
        [Display(Name = "Data Requerimento")]
        public DateTime? dataRequerimento { get; set; }
        [Display(Name = "Data do Ato")]
        public DateTime? DatadoAto { get; set; } //DATA EMISSÃO DO ATO
        [Display(Name = "Número processo Judicial")]
        public string numeroProcessoJudicial { get; set; }
        [Display(Name = "Comarca")]
        public string descricaoComarca { get; set; }
        [Display(Name = "Causa Invalidez")]
        public int? IdentificadorTipoCausaInvalidez { get; set; }
        [Display(Name = "Data do Laudo de Inspeção de saúde")]
        public DateTime? dataLaudoInspecaoSaude { get; set; }
        [Display(Name = "Código CID")]
        public string numeroCid { get; set; }
        [Display(Name = "Incapacitado p/ Trabalho?")]
        public bool? IndicativoIncapacidade { get; set; }
        [Display(Name = "Fundamento Legal da Doença")]
        public string DispositivoLegalDoenca { get; set; } //FUNDAMENTO LEGAL DA DOENÇA
        [Display(Name = "Processo Administrativo")]
        public string NumeroProcessoAdministrativo { get; set; }
        [Display(Name = "Parecer do Controle Interno")]
        public bool? indicativoParecerControleInterno { get; set; }
    }
}
