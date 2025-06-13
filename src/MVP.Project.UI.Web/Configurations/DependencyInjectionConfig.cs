using MVP.Project.Infra.CrossCutting.IoC;

namespace MVP.Project.UI.Web.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this WebApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            NativeInjectorBootStrapper.RegisterServices(builder);
        }
    }
}