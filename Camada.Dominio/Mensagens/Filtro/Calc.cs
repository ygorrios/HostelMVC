using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens.Filtro
{
    public class Calc
    {
        //Filtro de Campo de Pesquisa
        public int ID_Calc_Type;
        public string dtInicio;
        public string dtFim;
        public string user;


        //Paginação do Grid
        public int iDisplayStart;
        public int iDisplayLength;
        public string sSortDir_0;
        public int iSortCol_0;
    }
}
