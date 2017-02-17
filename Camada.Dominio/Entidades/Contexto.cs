using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camada.Dominio.Entidades
{
    public partial class Contexto : Camada.Dominio.Entidades.HostelEntities, IDisposable
    {
        //public Contexto()
        //    : base(BuildConnectionString)
        //{
        //    Database.SetInitializer<Contexto>(null);
        //    this.Configuration.AutoDetectChangesEnabled = true;
        //    this.Configuration.LazyLoadingEnabled = true;
        //}

        
        //private static string BuildConnectionString
        //{
        //    get
        //    {
        //        //Conexão padrão
        //        string nomePc = System.Environment.MachineName;
        //        string nomeDataBase = "Atos_Pessoal";
        //        string connectionStringDefault = @"Data Source=rdp\homolog;Initial Catalog=" + nomeDataBase + "";    //YGOR-TCE

        //        Dictionary<string, string> mapPc = new Dictionary<string, string>();
        //        mapPc.Add("PDCASEBH11", @"Data Source=PDCASEBH11;Initial Catalog=" + nomeDataBase + ";Integrated Security=True"); // messi Pd Case

        //        return mapPc.ContainsKey(nomePc) ? mapPc[nomePc] : connectionStringDefault;
        //    }
        //}
    }
}
