using MVP.Project.Infra.CrossCutting.Identity.Configuration;
using MVP.Project.Services.Api.Configurations;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configure Services
builder.AddApiConfiguration()
       .AddDatabaseConfiguration()
       .AddApiIdentityConfiguration()
       .AddSwaggerConfiguration()
       .AddDependencyInjectionConfiguration();

var app = builder.Build();

// Configure
app.UseHttpsRedirection()
    .UseCors(c =>
    {
        c.AllowAnyHeader();
        c.AllowAnyMethod();
        c.AllowAnyOrigin();
    })
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();
app.MapIdentityApi<IdentityUser>();
app.UseDbSeed();
app.UseSwaggerSetup();
app.Run();