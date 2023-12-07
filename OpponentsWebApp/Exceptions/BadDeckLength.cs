namespace OpponentsWebApp.Exceptions;

public class BadDeckLength : ClientException
{
    public BadDeckLength() {}

    public BadDeckLength(string message) : base(message) {}
}