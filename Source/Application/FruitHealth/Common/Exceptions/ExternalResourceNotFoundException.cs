namespace FruitHealth.Common.Exceptions;

public class ExternalResourceNotFoundException : IntegrationException
{
  public ExternalResourceNotFoundException(string message = "External resource not found.") : base(message)
  {
  }
  public ExternalResourceNotFoundException(string message = "External resource not found.", Exception? innerException = null) : base(message, innerException)
  {
  }
}
