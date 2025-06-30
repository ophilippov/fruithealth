using FruitHealth.Fruits.DTOs;
using MediatR;

namespace FruitHealth.Fruits.UseCases;

public sealed record class GetFruitsQuery: IRequest<IEnumerable<ScoredFruitResponseDto>>{
  public double? SugarMin { get; init; }
  public double? SugarMax { get; init; }
}
