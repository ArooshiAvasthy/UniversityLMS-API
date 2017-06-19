using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UniversityClient.Startup))]
namespace UniversityClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
