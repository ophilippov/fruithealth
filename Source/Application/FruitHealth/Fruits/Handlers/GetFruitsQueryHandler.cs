using FruitHealth.Fruits.DTOs;
using FruitHealth.Fruits.Providers;
using FruitHealth.Fruits.UseCases;
using MediatR;

namespace FruitHealth.Fruits.Handlers;

public class GetFruitsQueryHandler : IRequestHandler<GetFruitsQuery, IEnumerable<ScoredFruitResponseDto>>
{
  private readonly IFruitsProvider _fruitsProvider;

  public GetFruitsQueryHandler(IFruitsProvider fruitsProvider)
  {
    _fruitsProvider = fruitsProvider ?? throw new ArgumentNullException(nameof(fruitsProvider));
  }
  
  public async Task<IEnumerable<ScoredFruitResponseDto>> Handle(GetFruitsQuery request, CancellationToken cancellationToken)
  {
    // Get the fruits from the provider
    var fruits = await _fruitsProvider.GetFruitsAsync(request.SugarMin!.Value, request.SugarMax!.Value, cancellationToken);

    // Map the fruits to the response DTOs
    return fruits.Select(ScoredFruitResponseDto.From).OrderByDescending(f => f.Score);
  }
}