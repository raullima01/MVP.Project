using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVP.Project.Domain.Core.Events;
using MVP.Project.Domain.Events;
using MVP.Project.Domain.Models;
using MVP.Project.Infra.Data.Context;
using MVP.Project.Infra.CrossCutting.Identity.Data;


namespace MVP.Project.Services.Api.Configurations
{
    public static class DbMigrationHelpers
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var context = scope.ServiceProvider.GetRequiredService<CustomerContext>();
            var contextStore = scope.ServiceProvider.GetRequiredService<EventStoreSqlContext>();
            var contextIdentity = scope.ServiceProvider.GetRequiredService<MvpProjectIdentityContext>();

            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
            {
                await context.Database.MigrateAsync();
                await contextStore.Database.MigrateAsync();
                await contextIdentity.Database.MigrateAsync();

                await EnsureSeedProducts(context, contextStore, contextIdentity);
            }
        }

        private static async Task EnsureSeedProducts(CustomerContext context, 
                                                     EventStoreSqlContext contextStore,
                                                     MvpProjectIdentityContext contextIdentity)
        {
            if (contextIdentity.Users.Any())
                return;

            var userId = Guid.NewGuid();

            await contextIdentity.Users.AddAsync(new IdentityUser
            {
                Id = userId.ToString(),
                UserName = "cidadaojose@gmail.com",
                NormalizedUserName = "cidadaojose@gmail.com",
                Email = "cidadaojose@gmail.com",
                NormalizedEmail = "CIDADAOJOSE@GMAIL.COM",
                AccessFailedCount = 0,
                LockoutEnabled = false,
                PasswordHash = "AQAAAAIAAYagAAAAEGODJRgRnZQnCOYpgMXQdHnXGBCQAJLUQVBNKxnPtHnVHYlTpxlrQXKqBmYTMFYGWA==",
                TwoFactorEnabled = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            });

            await contextIdentity.UserClaims.AddAsync(new IdentityUserClaim<string>
            {
                UserId = userId.ToString(),
                ClaimType = "Customers",
                ClaimValue = "Write,Remove"
            });

            await contextIdentity.SaveChangesAsync();

            if (context.Customers.Any())
                return;

            var customer = new Customer(
                Guid.NewGuid(),
                "Raul Lima",
                "raulzerahh@gmail.com",
                "36373199657",
                new DateTime(1987, 06, 09),
                "19981611696",
                "000000",
                "00000",
                "17",
                "casa",
                "Vila Itapura",
                "13053000",
                "Campinas",
                "State",
                true
                );

            await context.Customers.AddAsync(customer);
            
            await context.SaveChangesAsync();

            var customerEvent = new CustomerRegisteredEvent(customer.Id,
                                                            customer.Name,
                                                            customer.Email,
                                                            customer.DocumentNumber,
                                                            customer.BirthDate,
                                                            customer.Phone,
                                                            customer.StateInscription,
                                                            customer.StreetAddress,
                                                            customer.BuildingNumber,
                                                            customer.SecondaryAddress,
                                                            customer.Neighborhood,
                                                            customer.ZipCode,
                                                            customer.City,
                                                            customer.State,
                                                            customer.Active);

            var serializedData = JsonConvert.SerializeObject(customerEvent);

            await contextStore.StoredEvent.AddAsync(new StoredEvent(customerEvent, serializedData, "sistema@sistema.com"));

            await contextStore.SaveChangesAsync();
        }
    }
}
