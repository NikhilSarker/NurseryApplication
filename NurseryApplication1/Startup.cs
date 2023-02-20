using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NurseryApplication1.Startup))]
namespace NurseryApplication1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
