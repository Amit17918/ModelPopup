using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ModelPopup.Startup))]
namespace ModelPopup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
