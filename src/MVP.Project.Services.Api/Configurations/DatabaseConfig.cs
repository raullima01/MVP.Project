using Microsoft.EntityFrameworkCore;
using MVP.Project.Infra.Data.Context;

namespace MVP.Project.Services.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static WebApplicationBuilder AddDatabaseConfiguration(this WebApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.Services.AddDbContext<CustomerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<EventStoreSqlContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            return builder;
        }

        public static WebApplication UseDbSeed(this WebApplication app)
        {
            ArgumentNullException.ThrowIfNull(app);

            DbMigrationHelpers.EnsureSeedData(app).Wait();

            return app;
        }
    }
}