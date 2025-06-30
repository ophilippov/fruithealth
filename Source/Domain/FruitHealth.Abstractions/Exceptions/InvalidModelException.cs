namespace FruitHealth.Abstractions.Exceptions;

public class InvalidModelException : DomainException
{
  public InvalidModelException(string message) : base(message)
  {
  }

  public InvalidModelException(string message, Exception innerException) : base(message, innerException)
  {
  }

  public static void ThrowFor(Exception innerException, string message = "The model is invalid. See inner exception for details.")
  {
    throw new InvalidModelException(message, innerException);
  }
}
