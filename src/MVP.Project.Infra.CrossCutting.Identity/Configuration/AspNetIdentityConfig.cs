﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using MVP.Project.Infra.CrossCutting.Identity.API;
using MVP.Project.Infra.CrossCutting.Identity.Data;
using MVP.Project.Infra.CrossCutting.Identity.User;

namespace MVP.Project.Infra.CrossCutting.Identity.Configuration
{
    public static class AspNetIdentityConfig
    {
        public static WebApplicationBuilder AddApiIdentityConfiguration(this WebApplicationBuilder builder)
        {
            builder.AddIdentityDbContext()
                   .AddIdentityApiSupport()
                   .AddJwtSupport()
                   .AddAspNetUserSupport();

            return builder;
        }

        public static WebApplicationBuilder AddWebIdentityConfiguration(this WebApplicationBuilder builder)
        {
            builder.AddIdentityDbContext()
                   .AddIdentityWebUISupport()
                   .AddAspNetUserSupport()
                   .AddSocialAuthenticationSupport();

            return builder;
        }

        private static WebApplicationBuilder AddIdentityDbContext(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDbContext<MvpProjectIdentityContext>(options =>
                       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

                return builder;
            }

            builder.Services.AddDbContext<MvpProjectIdentityContext>(options =>
                       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            return builder;
        }

        private static WebApplicationBuilder AddIdentityApiSupport(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                            .AddRoles<IdentityRole>()
                            .AddEntityFrameworkStores<MvpProjectIdentityContext>()
                            .AddSignInManager()
                            .AddRoleManager<RoleManager<IdentityRole>>()
                            .AddDefaultTokenProviders();

            return builder;
        }

        private static WebApplicationBuilder AddIdentityWebUISupport(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<MvpProjectIdentityContext>()
                    .AddDefaultTokenProviders()
                    .AddDefaultUI();

            return builder;
        }

        private static WebApplicationBuilder AddJwtSupport(this WebApplicationBuilder builder)
        {
            var appSettingsSection = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppJwtSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppJwtSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = appSettings.Audience,
                        ValidIssuer = appSettings.Issuer
                    };
                });

            return builder;
        }

        public static WebApplicationBuilder AddAspNetUserSupport(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IAspNetUser, AspNetUser>();

            return builder;
        }

        public static WebApplicationBuilder AddSocialAuthenticationSupport(this WebApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.Services.AddAuthentication()
                .AddFacebook(o =>
                {
                    o.AppId = builder.Configuration["Authentication:Facebook:AppId"];
                    o.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
                })
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                });

            return builder;
        }
    }
}