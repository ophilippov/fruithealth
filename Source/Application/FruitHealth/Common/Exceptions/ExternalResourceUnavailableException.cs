namespace FruitHealth.Common.Exceptions;

public class ExternalResourceUnavailableException : IntegrationException
{
  public ExternalResourceUnavailableException(string message = "External resource unavailable.") : base(message)
  {
  }

  public ExternalResourceUnavailableException(string message = "External resource unavailable.", Exception? innerException = null) : base(message, innerException)
  {
  }
}
