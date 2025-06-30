using FruitHealth.Api.Middlewares;

namespace FruitHealth.Api;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddGlobalExceptionHandling(this IServiceCollection services)
  {
    services.AddTransient<ExceptionHandlerMiddleware>();
    return services;
  }
}
