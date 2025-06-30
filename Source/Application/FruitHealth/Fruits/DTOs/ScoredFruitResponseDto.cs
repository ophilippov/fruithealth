using FruitHealth.Abstractions.Models;

namespace FruitHealth.Fruits.DTOs;

public record class ScoredFruitResponseDto(string Name, double Score) {
  public static ScoredFruitResponseDto From(Fruit fruit) {
    ArgumentNullException.ThrowIfNull(fruit);

    return new ScoredFruitResponseDto(fruit.Name, fruit.HealthScore);
  }

  public static explicit operator ScoredFruitResponseDto(Fruit fruit) {
    return From(fruit);
  }
}