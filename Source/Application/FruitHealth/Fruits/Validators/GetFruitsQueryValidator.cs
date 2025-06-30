using FluentValidation;
using FruitHealth.Fruits.UseCases;

namespace FruitHealth.Fruits.DTOs.Validators;

public class GetFruitsQueryValidator: AbstractValidator<GetFruitsQuery>
{
  public GetFruitsQueryValidator()
  {
    RuleFor(x => x.SugarMin)
      .NotNull().WithMessage("SugarMin is required.")
      .GreaterThanOrEqualTo(0).WithMessage("SugarMin must be 0 or more.");

    RuleFor(x => x.SugarMax)
      .NotNull().WithMessage("SugarMax is required.")
      .GreaterThanOrEqualTo(0).WithMessage("SugarMax must be 0 or more.");

    RuleFor(x => x)
      .Must(x => x.SugarMin <= x.SugarMax)
      .WithMessage("SugarMin must be less than or equal to SugarMax.")
      .When(x => x.SugarMin.HasValue && x.SugarMax.HasValue);
  }
}
