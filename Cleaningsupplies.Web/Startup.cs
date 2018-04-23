using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cleaningsupplies.Web.Startup))]
namespace Cleaningsupplies.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
