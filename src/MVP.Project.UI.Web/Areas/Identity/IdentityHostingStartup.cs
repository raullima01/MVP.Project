[assembly: HostingStartup(typeof(MVP.Project.UI.Web.Areas.Identity.IdentityHostingStartup))]
namespace MVP.Project.UI.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}