namespace DataLib.Persons;

public abstract class Person
{
    public StateLive State { get; internal set; }

    public enum StateLive
    {
        Alive,
        Dead
    }
    
    protected Person(string name)
    {
        Name = name;
    }

    private string Name { get; }
}