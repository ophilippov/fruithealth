
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace FruitHealth.Common;

public sealed class RequestValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
  where TResponse : notnull
{
  private readonly IEnumerable<IValidator<TRequest>> _validators;

  public RequestValidationPipeline(IEnumerable<IValidator<TRequest>> validators)
  {
    _validators = validators ?? throw new ArgumentNullException(nameof(validators));
  }

  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    var context = new ValidationContext<TRequest>(request);

    var validationFailures = await Task.WhenAll(
      _validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

    List<ValidationFailure> errors = validationFailures
      .Where(validationResult => validationResult.IsValid is false)
      .SelectMany(validationResult => validationResult.Errors)
      .ToList();

    if (errors.Count != 0)
    {
      throw new ValidationException(errors);
    }

    return await next(cancellationToken);
  }
}

