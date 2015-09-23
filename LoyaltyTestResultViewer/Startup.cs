using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoyaltyTestResultViewer.Startup))]
namespace LoyaltyTestResultViewer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
