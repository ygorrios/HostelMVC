using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Repositorio
{
    public interface IMONEY_COUNT
    {
        MessageInstance<Dominio.Entidades.Money_Count> GetMoney_CountById(int id);
        MessageInstance<Dominio.Entidades.Money_Count> GetLastMoney_CountByIdCalcType(int idCalcType);

        MessageInstance<Dominio.Entidades.Calc> GetTotalToCalc();
        //MessageCollection<Dominio.Mensagens.GridAtosPessoalConsulta> GetAtosPessoalGrid();


        /// <summary>
        /// Pesquisa retorna um Grid AtosPessoalConsultaGrid paginado utilizando o Plugin DataTabless
        /// </summary>
        /// <param name="filtroPesquisa">Filtro de Pesquisa feito pela View</param>
        /// <returns>Retorna uma lista de registro de acordo com a pesquisa realizada</returns>
        //MessageCollection<Dominio.Mensagens.GridAtosPessoalConsulta> ObterAtosPessoalPorFiltro(Dominio.Mensagens.Filtro.AtosPessoal filtroPesquisa);
        MessageCollection<Dominio.Mensagens.GridCalc> GetAllMoney_CountByFilter(Dominio.Mensagens.Filtro.Calc filtroPesquisa);


        MessageInstance<Dominio.Mensagens.TelaCalc> GetMoney_CountDetailsByID(int ID);
        MessageCollection<Dominio.Mensagens.GridMoney_Count> GetMoney_CountByID_Calc(int ID_Calc);
        MessageInstance<Dominio.Entidades.Money_Count> GetMoney_CountEnvelopeByIDCalc(int ID_Calc, int idCalcType);
        
    }
}
