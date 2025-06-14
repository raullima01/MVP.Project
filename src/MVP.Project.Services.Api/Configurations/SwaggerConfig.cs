using System.Diagnostics;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MVP.Project.Services.Api.Configurations
{
    public static class SwaggerConfig
    {
        public static WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder builder)
        {
            if (builder == null) 
                throw new ArgumentNullException(nameof(builder));

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MVP Project API",
                    Description = "API para gerenciamento do MVP Project",
                    Contact = new OpenApiContact 
                    { 
                        Name = "Raul Francisco Moreira Lima", 
                        Email = "raul.fml@outlook.com.br", 
                        Url = CreateSafeUri("https://github.com/raulzerahh") 
                    },
                    License = new OpenApiLicense 
                    { 
                        Name = "MIT", 
                        Url = CreateSafeUri("https://opensource.org/licenses/MIT") 
                    }
                });

                ConfigureSecurity(s);
                ConfigureEndpointFilters(s);
            });

            return builder;
        }

        private static void ConfigureSecurity(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Input the JWT like: Bearer {your token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        }

        private static void ConfigureEndpointFilters(SwaggerGenOptions options)
        {
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (apiDesc.RelativePath == null)
                    return false;

                var excludedPaths = new[]
                {
                    "register",
                    "manage",
                    "refresh",
                    "login",
                    "confirmEmail",
                    "resendConfirmationEmail",
                    "forgotPassword",
                    "resetPassword"
                };

                return !excludedPaths.Any(path => 
                    apiDesc.RelativePath.Contains(path, StringComparison.OrdinalIgnoreCase));
            });
        }

        private static Uri CreateSafeUri(string url)
        {
            try
            {
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                    {
                        url = "https://" + url;
                    }
                }
                return new Uri(url);
            }
            catch (UriFormatException ex)
            {
                Debug.WriteLine($"URI inválida: {url}. Erro: {ex.Message}");
                return new Uri("https://github.com"); // URL fallback
            }
        }

        public static IApplicationBuilder UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) 
                throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MVP Project API v1");
                options.RoutePrefix = "api-docs";
                options.DocumentTitle = "MVP Project API Documentation";
                options.DefaultModelsExpandDepth(-1);
                options.DisplayRequestDuration();
                options.EnableDeepLinking();
            });

            return app;
        }
    }
}