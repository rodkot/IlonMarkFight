namespace Core.Config;


public record CoreConfig(int ExperimentCount, DbRequest Request);

public class ExperimentConfig
{
    public IList<Uri> Uris { get; init; }
}

public enum DbRequest
{
    None,
    Generate,
    UseGenerated
}