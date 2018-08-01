using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LogTestWebFrom.Startup))]
namespace LogTestWebFrom
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
