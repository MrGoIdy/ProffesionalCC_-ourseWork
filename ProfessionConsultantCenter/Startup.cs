using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProfessionConsultantCenter.Startup))]
namespace ProfessionConsultantCenter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
