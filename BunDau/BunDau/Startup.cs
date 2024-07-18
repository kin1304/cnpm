using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BunDau.StartupOwin))]

namespace BunDau
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
