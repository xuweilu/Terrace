using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Terrace.Startup))]
namespace Terrace
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
