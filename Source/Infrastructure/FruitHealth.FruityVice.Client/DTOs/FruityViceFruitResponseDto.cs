using FruitHealth.Abstractions.Models;

namespace FruitHealth.FruityVice.Client.DTOs;

public sealed record class FruityViceFruitResponseDto 
(
  string Name,
  FruityViceFruitNutritionsResponseDto Nutritions
){

  public Fruit ToModel()
  {
    return new Fruit(Name, Nutritions.Protein, Nutritions.Carbohydrates, Nutritions.Sugar, Nutritions.Fat);
  }

  public static implicit operator Fruit(FruityViceFruitResponseDto dto) => dto.ToModel();
}
