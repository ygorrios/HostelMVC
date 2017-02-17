using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Repositorio
{
    public interface ILISTS
    {
        MessageInstance<Dominio.Entidades.LIST> GetTotalLastCalc();

        /// <summary>
        /// Pesquisa retorna um Grid AtosPessoalConsultaGrid paginado utilizando o Plugin DataTabless
        /// </summary>
        /// <param name="filtroPesquisa">Filtro de Pesquisa feito pela View</param>
        /// <returns>Retorna uma lista de registro de acordo com a pesquisa realizada</returns>
        MessageCollection<Dominio.Mensagens.GridLists> GetAllListsByFilter(Dominio.Mensagens.Filtro.Lists filtroPesquisa);
    }
}
