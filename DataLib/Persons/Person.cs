namespace DataLib.Persons;

public abstract class Person
{
    
    protected Person(string name)
    {
        Name = name;
    }

    public string Name { get; }
}