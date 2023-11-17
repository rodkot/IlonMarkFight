namespace Core.Config;


public class CoreConfig
{
    public int ExperimentCount { get; init; }
    public DbRequest Request { get; init; }
}

public enum DbRequest
{
    None,
    Generate,
    UseGenerated
}