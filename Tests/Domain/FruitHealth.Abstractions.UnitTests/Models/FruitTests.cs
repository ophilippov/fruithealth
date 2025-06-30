using FruitHealth.Abstractions.Exceptions;
using FruitHealth.Abstractions.Models;

namespace FruitHealth.Abstractions.UnitTests;

public class FruitTests
{
    [Theory]
    [InlineData(20.0, 10.0, 5.0, 2.0, 36.0)]
    [InlineData(1.0, 2.0, 3.0, 4.0, -8.0 )]
    [InlineData(0.5, 1.5, 2.5, 3.5, -7.75)]
    [InlineData(0.0, 0.0, 0.0, 0.0, 0.0)]
    [InlineData(2.0, 3.0, 1.0, 0.5, 3.5)]
    [InlineData(1.0, 0.0, 0.0, 0.0, 2.0)]
    [InlineData(0.0, 1.0, 0.0, 0.0, 0.5)]
    [InlineData(0.0, 0.0, 1.0, 0.0, -1.0)]
    [InlineData(0.0, 0.0, 0.0, 1.0, -2.0)]
    public void HealthScore_ShouldReturnCorrectValue(double protein, double carbohydrates, double sugar, double fat, double expectedHealthScore)
    {
        // Arrange
        string fruitName = "Apple";
        var fruit = new Fruit(fruitName, protein, carbohydrates, sugar, fat);

        // Act
        double actualHealthScore = fruit.HealthScore;

        // Assert
        Assert.Equal(expectedHealthScore, actualHealthScore);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void Constructor_ShouldFail_WhenInvalidFruitNameProvided(string fruitName)
    {
        // Arrange
        double protein = 1.0;
        double carbohydrates = 2.0;
        double sugar = 3.0;
        double fat = 4.0;

        // Act & Assert
        var exception = Assert.Throws<InvalidModelException>(() => new Fruit(fruitName, protein, carbohydrates, sugar, fat));
        var innerException = Assert.IsType<ArgumentException>(exception.InnerException);
        Assert.Equal("The model is invalid. See inner exception for details.", exception.Message);
        Assert.Equal("Fruit name cannot be null, empty, or whitespace. (Parameter 'name')", innerException.Message);
    }

    [Fact]
    public void Constructor_ShouldFail_WhenNegativeProteinValueProvided()
    {
        // Arrange
        string fruitName = "Apple";
        double protein = -1.0;
        double carbohydrates = 2.0;
        double sugar = 3.0;
        double fat = 4.0;

        // Act & Assert
        var exception = Assert.Throws<InvalidModelException>(() => new Fruit(fruitName, protein, carbohydrates, sugar, fat));
        var innerException = Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        Assert.Equal("The model is invalid. See inner exception for details.", exception.Message);
        Assert.Equal("Protein value cannot be negative. (Parameter 'protein')", innerException.Message);
    }

    [Fact]
    public void Constructor_ShouldFail_WhenNegativeCarbohydratesValueProvided()
    {
        // Arrange
        string fruitName = "Apple";
        double protein = 1.0;
        double carbohydrates = -2.0;
        double sugar = 3.0;
        double fat = 4.0;

        // Act & Assert
        var exception = Assert.Throws<InvalidModelException>(() => new Fruit(fruitName, protein, carbohydrates, sugar, fat));
        var innerException = Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        Assert.Equal("The model is invalid. See inner exception for details.", exception.Message);
        Assert.Equal("Carbohydrates value cannot be negative. (Parameter 'carbohydrates')", innerException.Message);
    }

    [Fact]
    public void Constructor_ShouldFail_WhenNegativeFatValueProvided()
    {
        // Arrange
        string fruitName = "Apple";
        double protein = 1.0;
        double carbohydrates = 2.0;
        double sugar = 3.0;
        double fat = -4.0;

        // Act & Assert
        var exception = Assert.Throws<InvalidModelException>(() => new Fruit(fruitName, protein, carbohydrates, sugar, fat));
        var innerException = Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        Assert.Equal("The model is invalid. See inner exception for details.", exception.Message);
        Assert.Equal("Fat value cannot be negative. (Parameter 'fat')", innerException.Message);
    }

    [Fact]
    public void Constructor_ShouldFail_WhenNegativeSugarValueProvided()
    {
        // Arrange
        string fruitName = "Apple";
        double protein = 1.0;
        double carbohydrates = 2.0;
        double sugar = -3.0;
        double fat = 4.0;

        // Act & Assert
        var exception = Assert.Throws<InvalidModelException>(() => new Fruit(fruitName, protein, carbohydrates, sugar, fat));
        var innerException = Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        Assert.Equal("The model is invalid. See inner exception for details.", exception.Message);
        Assert.Equal("Sugar value cannot be negative. (Parameter 'sugar')", innerException.Message);
    }



}
