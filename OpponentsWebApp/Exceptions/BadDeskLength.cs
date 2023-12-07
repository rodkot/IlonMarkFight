namespace OpponentsWebApp.Exceptions;

public class BadDeskLength : ClientException
{
    public BadDeskLength() {}

    public BadDeskLength(string message) : base(message) {}
}