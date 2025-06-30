using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using FruitHealth.Abstractions.Models;
using FruitHealth.Common.Exceptions;
using FruitHealth.Fruits.Providers;
using FruitHealth.FruityVice.Client.DTOs;

namespace FruitHealth.FruityVice.Client;

public class FruityViceClient : IFruitsProvider
{
  private readonly HttpClient _httpClient;

  public FruityViceClient(HttpClient httpClient)
  {
    _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
  }

  public async Task<IEnumerable<Fruit>> GetFruitsAsync(double minSugar = 0, double maxSugar = double.MaxValue, CancellationToken cancellationToken = default)
  {
    try {
      var min = minSugar.ToString(CultureInfo.InvariantCulture);
      var max = maxSugar.ToString(CultureInfo.InvariantCulture);
      var response = await _httpClient.GetAsync($"fruit/sugar?min={min}&max={max}", cancellationToken);
      
      if (response.StatusCode == HttpStatusCode.NotFound)
      {
        throw new ExternalResourceNotFoundException("The requested FruityVice API resource was not found.");
      }

      response.EnsureSuccessStatusCode();

      var fruits = await response.Content.ReadFromJsonAsync<IEnumerable<FruityViceFruitResponseDto>>(cancellationToken: cancellationToken);
      return fruits?.Select(f => (Fruit)f) ?? [];
    }
    catch (Exception ex) when (ex is not ExternalResourceNotFoundException)
    {
      throw new ExternalResourceUnavailableException("The requested FruityVice API resource is unavailable or the request is invalid. See inner exception for details.", ex);
    }
  }
}
