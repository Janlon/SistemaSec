using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaSec.Startup))]
namespace SistemaSec
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
