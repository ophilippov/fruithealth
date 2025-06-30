namespace FruitHealth.FruityVice.Client.Configuration;

public class FruityViceClientOptions
{
  public Uri BaseAddress { get; set; } = default!;
  public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
}
