namespace DataLib.Exceptions;

public class UnexpectedHttpStatusCodeException : Exception
{
    public UnexpectedHttpStatusCodeException(string message) : base(message) {}
}