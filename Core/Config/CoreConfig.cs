namespace Core.Config;


public record CoreConfig(int ExperimentCount, DbRequest Request);

public enum DbRequest
{
    None,
    Generate,
    UseGenerated
}