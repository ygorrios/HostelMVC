using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens
{
    public class DropDownlist
    {
        public bool Selected { get; set; }
        #region Estado
        public int Value { get; set; }
        public string Text { get; set; }

        public DropDownlist AdicionarSelecione()
        {
            return new DropDownlist { Value = 0, Text = "< Select >", Selected = true};
        }

        public List<DropDownlist> ListaDDLEstado()
        {
            return new List<DropDownlist>
            {
                new DropDownlist { Value = 0, Text = "Atos Não Enviados", Selected = true},
                new DropDownlist { Value = 1, Text = "Atos Enviados"},
            };
        }

        public List<DropDownlist> ListaDDLTipoAto()
        {
            return new List<DropDownlist>
            {
                new DropDownlist { Value = 46, Text = "Aposentadoria", Selected = true},
                new DropDownlist { Value = 48, Text = "Reforma"},
                new DropDownlist { Value = 54, Text = "Transferência para a Reserva Remunerada"},

                new DropDownlist { Value = 47, Text = "Pensão e Auxílio Especial"},
                new DropDownlist { Value = 52, Text = "Retificação do Ato de Pensão e Auxílio Especial"},

                new DropDownlist { Value = 49, Text = "Retificação de Ato Aposentatório"},
                new DropDownlist { Value = 50, Text = "Retificação de Ato de Reforma"},
                new DropDownlist { Value = 51, Text = "Retificação de Ato de Transf para a Res Remunerada"},
                
                new DropDownlist { Value = 53, Text = "Revogação de Registro de Ato Aposentatório"},
            };
        }

        public List<DropDownlist> ListaDDLPayment_Type()
        {
            return new List<DropDownlist>
            {
                new DropDownlist { Value = 3, Text = "Cash", Selected = true},
                new DropDownlist { Value = 4, Text = "Card"},
                new DropDownlist { Value = 1, Text = "BACS"},

                new DropDownlist { Value = 4, Text = "Cheque"},
                new DropDownlist { Value = 5, Text = "Gift Certifiacte"},

                new DropDownlist { Value = 6, Text = "OTA Paid"},
                new DropDownlist { Value = 7, Text = "Other"},
                new DropDownlist { Value = 8, Text = "Voucher"},
            };
        }

        public List<DropDownlist> ListaDDLIndicativoAcumulacaoDeCargo()
        {
            return new List<DropDownlist>
            {
                new DropDownlist { Value = 0, Text = "Não", Selected = true},
                new DropDownlist { Value = 1, Text = "Sim"},
            };
        }

        public List<DropDownlist> ListaDDLIncapacitadoParaTrabalho()
        {
            return new List<DropDownlist>
            {
                new DropDownlist { Value = 0, Text = "Não", Selected = true},
                new DropDownlist { Value = 1, Text = "Sim"},
            };
        }

        public List<DropDownlist> ListaDDLParecerControleInterno()
        {
            return new List<DropDownlist>
            {
                new DropDownlist { Value = 0, Text = "Favorável", Selected = true},
                new DropDownlist { Value = 1, Text = "Contrário"},
            };
        }

        #endregion
    }
}
