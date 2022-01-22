﻿using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.DataAcess.Infraestructure.Queries;
using BlazorApp.DataAcess.Infraestructure.Repositories;
using BlazorApp.Features.Identity;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BlazorApp.DataAcess.EF.Extensions
{
    public static class IoCExtension
    {
        public static void AddInfraestrutureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddScoped<IUserDataRepository, UserDataRepository>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserDataQueries, UserDataQueries>();


        }
    }
}