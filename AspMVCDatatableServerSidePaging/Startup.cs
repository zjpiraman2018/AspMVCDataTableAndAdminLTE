using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspMVCDatatableServerSidePaging.Startup))]
namespace AspMVCDatatableServerSidePaging
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
