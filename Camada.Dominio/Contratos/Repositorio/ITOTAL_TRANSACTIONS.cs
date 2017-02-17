using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Repositorio
{
    public interface ITOTAL_TRANSACTIONS
    {
        MessageInstance<Dominio.Entidades.Total_Transactions> GetLastTotal_Transaction();
        MessageInstance<Dominio.Entidades.Total_Transactions> GetLastTotal_Vagner();
        MessageInstance<Dominio.Entidades.Total_Transactions> GetLastTotal_Card();
        MessageInstance<Dominio.Mensagens.TelaTotalTransaction> GetTotalTransactionByID(int ID);
        MessageCollection<Dominio.Mensagens.GridTotalTransactionIndex> GetAllTotalTransactions(Dominio.Mensagens.Filtro.TotalTransactions filtroPesquisa);
        MessageCollection<Dominio.Mensagens.GridTotalTransactionIndex> GetReportEndOfTheDayTotalTransactions(Dominio.Mensagens.Filtro.TotalTransactions filtroPesquisa);
        MessageCollection<Dominio.Mensagens.GridTotalTransactionIndex> GetAllTotalTransactionsByBank(Dominio.Mensagens.Filtro.TotalTransactions filtroPesquisa);

    }
}
