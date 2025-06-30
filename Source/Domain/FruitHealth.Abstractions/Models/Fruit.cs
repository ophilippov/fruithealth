
using FruitHealth.Abstractions.Exceptions;

namespace FruitHealth.Abstractions.Models;

public class Fruit
{
  // NOTE: We can remove the private fields if the model is immutable. But normally, the model is mutable
  private readonly double _protein;
  private readonly double _carbohydrates;
  private readonly double _sugar;
  private readonly double _fat;
  private readonly string _name;

  public string Name => _name;
  public double Protein => _protein;
  public double Carbohydrates => _carbohydrates;
  public double Sugar => _sugar;
  public double Fat => _fat;

  public double HealthScore => CalculateHealthScore();

  public Fruit(string name, double protein, double carbohydrates, double sugar, double fat)
  {
    // Validate inputs
    ValidateInput(name, protein, carbohydrates, sugar, fat);

    _name = name;
    _protein = protein;
    _carbohydrates = carbohydrates;
    _sugar = sugar;
    _fat = fat;
  }

  private double CalculateHealthScore()
  {
    var score = _protein * 2 + _carbohydrates * 0.5 - (_sugar + _fat * 2);
    return Math.Round(score, 2);
  }

  private static void ValidateInput(string name, double protein, double carbohydrates, double sugar, double fat)
  {
    if (string.IsNullOrWhiteSpace(name))
      InvalidModelException.ThrowFor(new ArgumentException("Fruit name cannot be null, empty, or whitespace.", nameof(name)));

    if (protein < 0)
      InvalidModelException.ThrowFor(new ArgumentOutOfRangeException(nameof(protein), "Protein value cannot be negative."));

    if (carbohydrates < 0)
      InvalidModelException.ThrowFor(new ArgumentOutOfRangeException(nameof(carbohydrates), "Carbohydrates value cannot be negative."));

    if (sugar < 0)
      InvalidModelException.ThrowFor(new ArgumentOutOfRangeException(nameof(sugar), "Sugar value cannot be negative."));

    if (fat < 0)
      InvalidModelException.ThrowFor(new ArgumentOutOfRangeException(nameof(fat), "Fat value cannot be negative."));
  }
}
