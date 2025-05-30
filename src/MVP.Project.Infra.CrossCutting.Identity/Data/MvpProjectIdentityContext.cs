using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace MVP.Project.Infra.CrossCutting.Identity.Data
{
    public class MvpProjectIdentityContext : IdentityDbContext
    {
        public MvpProjectIdentityContext(DbContextOptions<MvpProjectIdentityContext> options) : base(options) { }
    }

    public class MvpProjectIdentityContextFactory : IDesignTimeDbContextFactory<MvpProjectIdentityContext>
    {
        public MvpProjectIdentityContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<MvpProjectIdentityContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (environment == "Development")
            {
                builder.UseSqlServer(connectionString);
            }
                         

            return new MvpProjectIdentityContext(builder.Options);
        }
    }
}