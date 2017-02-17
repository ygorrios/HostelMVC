using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Repositorio
{
    public interface ITRANSACTIONS
    {
        MessageInstance<Dominio.Entidades.Money_Count> GetMoney_CountById(int id);
        MessageInstance<Dominio.Entidades.Calc> GetTotalToCalc();
        //MessageCollection<Dominio.Mensagens.GridAtosPessoalConsulta> GetAtosPessoalGrid();


        /// <summary>
        /// Pesquisa retorna um Grid AtosPessoalConsultaGrid paginado utilizando o Plugin DataTabless
        /// </summary>
        /// <param name="filtroPesquisa">Filtro de Pesquisa feito pela View</param>
        /// <returns>Retorna uma lista de registro de acordo com a pesquisa realizada</returns>
        //MessageCollection<Dominio.Mensagens.GridAtosPessoalConsulta> ObterAtosPessoalPorFiltro(Dominio.Mensagens.Filtro.AtosPessoal filtroPesquisa);
        MessageCollection<Dominio.Mensagens.GridTransactions> GetAllTransactionsByID_Total_Transactions(Dominio.Mensagens.Filtro.Transactions filtroPesquisa);


        //MessageInstance<Dominio.Mensagens.TelaAto> ObterAtoPorId(int id);
    }
}
