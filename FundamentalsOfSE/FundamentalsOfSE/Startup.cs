using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FundamentalsOfSE.Startup))]
namespace FundamentalsOfSE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
