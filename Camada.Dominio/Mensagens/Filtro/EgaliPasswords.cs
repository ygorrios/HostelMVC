using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens.Filtro
{
    public class EgaliPasswords
    {
        //Filtro de Campo de Pesquisa
        public int ID;
        public string item;
        public string login;

        //Paginação do Grid
        public int iDisplayStart;
        public int iDisplayLength;
    }
}
