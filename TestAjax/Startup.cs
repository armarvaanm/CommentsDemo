using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CommentsDemo.Startup))]
namespace CommentsDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
