using Camada.Dominio.Mensagens;
using Library.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Contratos.Repositorio
{
    public interface ISTOCK
    {
        MessageInstance<Dominio.Entidades.Stock> GetStockById(int id);
        MessageCollection<GridStock> GetAllStockByFilter(Dominio.Mensagens.Filtro.Stock filtroPesquisa);
        MessageCollection<GridStock> GetAllStockControlByFilter(Dominio.Mensagens.Filtro.Stock filtroPesquisa);
    }
}
