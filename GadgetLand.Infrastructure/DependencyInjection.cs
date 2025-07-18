﻿using GadgetLand.Application.Interfaces;
using GadgetLand.Application.Interfaces.Repositories;
using GadgetLand.Application.Interfaces.Services;
using GadgetLand.Infrastructure.Persistence;
using GadgetLand.Infrastructure.Persistence.Repositories;
using GadgetLand.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GadgetLand.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GadgetLandDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("GadgetLand")));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!)),
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies["token"];
                    return Task.CompletedTask;
                }
            };
        });

        services.AddHttpContextAccessor();
        services.AddHttpClient();

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<GadgetLandDbContext>());

        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<IPaymentService, ZarinPalPaymentService>();

        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IBrandsRepository, BrandsRepository>();
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<IReviewsRepository, ReviewsRepository>();
        services.AddScoped<IProvincesRepository, ProvincesRepository>();
        services.AddScoped<ICitiesRepository, CitiesRepository>();
        services.AddScoped<ISettingsRepository, SettingsRepository>();
        services.AddScoped<IPaymentsRepository, PaymentsRepository>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IReportsRepository, ReportsRepository>();

        return services;
    }
}
