﻿using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.DataAcess.Infraestructure.Queries;
using BlazorApp.DataAcess.Infraestructure.Repositories;
using BlazorApp.Encryptor;
using BlazorApp.Features.Identity;
using BlazorApp.Messages;
using BlazorApp.Services;
using MassTransit;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BlazorApp.DataAcess.EF.Extensions
{
    public static class IoCExtension
    {
        public static void AddInfraestrutureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserDataQueries, UserDataQueries>();
            services.AddScoped<IPackagesRepository, PackagesRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<IWithdrawalRepository, WithdrawalRepository>();
            services.AddScoped<IAfiliadoDataQueries, AfiliadoDataQueries>();
            services.AddScoped<IEncryptor, Encryption>();
            services.AddScoped<IPackageMontoDataQueries, PackageMontoDataQueries>();
            services.AddScoped<IMessagesProcessor, MessagesProcessor>();
            services.AddScoped<IMovPackageDataQueries, MovPackageDataQueries>();
            services.AddScoped<IPujaRepository, PujaRepository>();
        }
    }
}
