using FluentValidation;
using Microsoft.Extensions.Options;

namespace FruitHealth.FruityVice.Client.Configuration;

public class FruityViceClientValidateOptions : IValidateOptions<FruityViceClientOptions>
{
  private readonly IValidator<FruityViceClientOptions> _validator;

  public FruityViceClientValidateOptions(IValidator<FruityViceClientOptions> validator)
  {
    _validator = validator ?? throw new ArgumentNullException(nameof(validator));
  }
    
  public ValidateOptionsResult Validate(string? name, FruityViceClientOptions options)
  {
    var validationResult = _validator.Validate(options);
    if (validationResult.IsValid)
    {
      return ValidateOptionsResult.Success;
    }

    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
    return ValidateOptionsResult.Fail(errors);
  }
}
