using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BloodBank.MVC.Startup))]
namespace BloodBank.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
