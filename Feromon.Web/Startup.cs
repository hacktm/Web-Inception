using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Feromon.Web.Startup))]
namespace Feromon.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
