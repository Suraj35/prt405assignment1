using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(prt405assignment1.Startup))]
namespace prt405assignment1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
