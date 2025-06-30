using FluentValidation;
using FruitHealth.Fruits.Providers;
using FruitHealth.FruityVice.Client.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FruitHealth.FruityVice.Client;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddFruityViceClient(this IServiceCollection services, string configSectionName = "FruityVice")
  {
    ArgumentNullException.ThrowIfNull(services);

    services.AddHttpClient<IFruitsProvider, FruityViceClient>((provider, client) =>
    {
      var options = provider.GetRequiredService<IOptionsMonitor<FruityViceClientOptions>>().CurrentValue;

      client.BaseAddress = options.BaseAddress;
      client.Timeout = options.Timeout;
    });

    services.AddSingleton<IValidator<FruityViceClientOptions>, FruityViceClientOptionsValidator>();
    services.AddSingleton<IValidateOptions<FruityViceClientOptions>, FruityViceClientValidateOptions>();

    // Configure options
    services.AddOptions<FruityViceClientOptions>()
        .BindConfiguration(configSectionName)
        .ValidateOnStart();

    return services;
  }
}
