using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JewelryStore.UI.Startup))]
namespace JewelryStore.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

           
        }       
    }
}
