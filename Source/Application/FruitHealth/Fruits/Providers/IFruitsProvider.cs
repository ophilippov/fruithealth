using FruitHealth.Abstractions.Models;

namespace FruitHealth.Fruits.Providers;

public interface IFruitsProvider
{
    /// <summary>
    /// Gets the fruit data from the external source based on the specified sugar content.
    /// This method retrieves a list of fruits that fall within the specified sugar content range.
    /// If no range is specified, it defaults to all fruits.
    /// </summary>
    /// <returns>A list of fruits based on the specified sugar content.</returns>
    Task<IEnumerable<Fruit>> GetFruitsAsync(double minSugar = 0, double maxSugar = double.MaxValue, CancellationToken cancellationToken = default);

}
