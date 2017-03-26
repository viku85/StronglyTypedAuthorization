using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomAuthorizationSample.Startup))]
namespace CustomAuthorizationSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
