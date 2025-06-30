using FluentValidation;

namespace FruitHealth.FruityVice.Client.Configuration;

public class FruityViceClientOptionsValidator: AbstractValidator<FruityViceClientOptions>
{
  public FruityViceClientOptionsValidator()
  {
    RuleFor(options => options.BaseAddress)
        .Cascade(CascadeMode.Stop)
        .NotEmpty()
        .Must(uri => uri.IsAbsoluteUri)
        .WithMessage("BaseAddress is required and must be an absolute URI.");
    
    RuleFor(options => options.Timeout)
        .GreaterThan(TimeSpan.Zero)
        .WithMessage("Timeout must be greater than zero.");
  }
}
