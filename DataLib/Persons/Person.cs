namespace DataLib.Persons;

public abstract class Person
{
    
    protected Person(string name)
    {
        Name = name;
    }

    private string Name { get; }
}