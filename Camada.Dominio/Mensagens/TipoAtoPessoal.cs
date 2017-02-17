using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtosPessoalMVC.Models
{
    public class TipoAtoPessoal
    {
        public int IDTipoAtoPessoal { get; set; }
        public string DescricaoTipoAtoPessoal { get; set; }

        public List<TipoAtoPessoal> ListaTiposAtoPessoal()
        {
            return new List<TipoAtoPessoal>
            {
                new TipoAtoPessoal { IDTipoAtoPessoal = 0, DescricaoTipoAtoPessoal = "< Selecione >"},
                new TipoAtoPessoal { IDTipoAtoPessoal = 6, DescricaoTipoAtoPessoal = "Aposentadoria / Reforma / Transferencia para reserva"},
                new TipoAtoPessoal { IDTipoAtoPessoal = 13, DescricaoTipoAtoPessoal = "Pensão"},
                new TipoAtoPessoal { IDTipoAtoPessoal = 18, DescricaoTipoAtoPessoal = "Retificação Aposentadoria / Reforma / Transferencia para reserva"},
                new TipoAtoPessoal { IDTipoAtoPessoal = 18, DescricaoTipoAtoPessoal = "Retificação Pensão"},
                new TipoAtoPessoal { IDTipoAtoPessoal = 20, DescricaoTipoAtoPessoal = "Revogação Aposentadoria / Reforma / Transferencia para reserva"}
            };
        }
    }
}