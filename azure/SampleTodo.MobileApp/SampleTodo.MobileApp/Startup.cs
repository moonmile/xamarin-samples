using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SampleTodo.MobileApp.Startup))]

namespace SampleTodo.MobileApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}