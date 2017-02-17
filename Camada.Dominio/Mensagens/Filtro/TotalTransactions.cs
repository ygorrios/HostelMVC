using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens.Filtro
{
    public class TotalTransactions
    {
        //Filtro de Campo de Pesquisa
        public int Report_Type;
        public string dtInicio;
        public string dtFim;
        public string txtNome;
        public bool banco = false;
        public int ID_REPORT_TYPE;
        public string Username;
        public int ID_SHIFT_TYPE;

        //Paginação do Grid
        public int iDisplayStart;
        public int iDisplayLength;
        public string sSortDir_0;
        public int iSortCol_0;
    }
}
