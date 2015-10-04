using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PiHomeAutomation.Startup))]
namespace PiHomeAutomation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
