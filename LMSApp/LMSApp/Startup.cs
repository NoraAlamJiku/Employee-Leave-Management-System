using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMSApp.Startup))]
namespace LMSApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
