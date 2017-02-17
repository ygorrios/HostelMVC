using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Repositorio
{
    public interface ICALC
    {
        MessageInstance<Dominio.Entidades.Calc> GetTotalLastCalc();

        /// <summary>
        /// Pesquisa retorna um Grid AtosPessoalConsultaGrid paginado utilizando o Plugin DataTabless
        /// </summary>
        /// <param name="filtroPesquisa">Filtro de Pesquisa feito pela View</param>
        /// <returns>Retorna uma lista de registro de acordo com a pesquisa realizada</returns>
        MessageCollection<Dominio.Mensagens.GridAtosPessoalConsulta> GetAllCalcByFilter(Dominio.Mensagens.Filtro.Calc filtroPesquisa);
    }
}
