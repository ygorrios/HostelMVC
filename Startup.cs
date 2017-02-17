using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Camada.Aplicacao.Startup))]
namespace Camada.Aplicacao
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
