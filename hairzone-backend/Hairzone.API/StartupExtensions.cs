using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hairzone.API;

internal static class StartupExtensions
{
    public static IServiceCollection AddConfiguration<T>(this IServiceCollection serviceCollection)
        where T : class, new()
    {
        serviceCollection.AddOptions<T>()
            .Configure<IConfiguration>((authSettings, configuration) => {
                BindConfiguration(authSettings, configuration);
            });
        return serviceCollection;
    }

    private static T BindConfiguration<T>(T option, IConfiguration configuration) where T : class, new()
    {
        var masterName = option.GetType().Name;
        var properties = option.GetType().GetProperties();
        foreach (var property in properties)
        {
            var propertyConfigName = $"{masterName}_{property.Name}";
            string configValue = configuration[propertyConfigName];
            dynamic value = property.PropertyType.Name switch
            {
                nameof(Boolean) => bool.Parse(configValue),
                nameof(Int32) => int.Parse(configValue),
                nameof(String) => configValue,
                _ => property.PropertyType.IsEnum
                        ? Enum.Parse(property.PropertyType, configValue)
                        : configValue
            };
            if (value is not null)
            {
                property.SetValue(option, value, null);
            }
        }
        return option;
    }
}