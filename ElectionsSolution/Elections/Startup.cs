using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Elections.Startup))]
namespace Elections
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
