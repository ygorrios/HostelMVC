﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Mensagens.Filtro
{
    public class Lists
    {
        //Filtro de Campo de Pesquisa
        public int ID_LIST_TYPE;

        public string firstName;
        public string lastName;

        public string[] lastFirstName;

        //Paginação do Grid
        public int iDisplayStart;
        public int iDisplayLength;
        public string sSortDir_0;
        public int iSortCol_0;
    }
}
