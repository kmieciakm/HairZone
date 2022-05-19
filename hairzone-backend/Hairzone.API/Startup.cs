using AzureTables;
using Hairzone.CORE.Contracts;
using Hairzone.CORE.Infrastructure;
using Hairzone.CORE.Integration;
using Hairzone.CORE.Services;
using Hairzone.DAL;
using Hairzone.DAL.Tables;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

[assembly: FunctionsStartup(typeof(Hairzone.API.Startup))]
namespace Hairzone.API;

internal class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        // Database
        builder.Services.AddConfiguration<AzureStorageSettings>();
        builder.Services.AddScoped<IAddressTable, AddressTable>();
        builder.Services.AddScoped<ISalonTable, SalonTable>();
        builder.Services.AddScoped<ISalonRepository, SalonRepository>();

        // Identity
        builder.Services.AddConfiguration<MicroAuthIdentitySettings>();
        builder.Services.AddScoped<IIdentityService, MicroAuthIdentityService>();


        builder.Services.AddScoped<IContractorParser, ContractorParser>();
        builder.Services.AddScoped<ISalonAdministrationService, SalonAdminstrationService>();
    }

    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        builder.ConfigurationBuilder
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("local.settings.json", true)
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
            .AddEnvironmentVariables()
            .Build();
    }
}
