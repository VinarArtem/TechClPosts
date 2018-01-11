using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TechClPosts.Startup))]
namespace TechClPosts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
