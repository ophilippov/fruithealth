using FluentValidation;
using FruitHealth.Common;
using FruitHealth.Fruits.DTOs.Validators;
using FruitHealth.Fruits.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace FruitHealth;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddFruitHealthApplicationServices(this IServiceCollection services)
  {
    ArgumentNullException.ThrowIfNull(services);
    // This will register all handlers in the current assembly and the validation pipeline
    services.AddMediatR(cfg => {
      cfg.RegisterServicesFromAssemblyContaining<GetFruitsQueryHandler>();
      cfg.AddOpenBehavior(typeof(RequestValidationPipeline<,>));
    });

    services.AddValidatorsFromAssemblyContaining<GetFruitsQueryValidator>();

    return services;
  }
}
