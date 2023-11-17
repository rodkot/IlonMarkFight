using DbStorage.Enitites;
using Microsoft.EntityFrameworkCore;

namespace DbStorage;

public class ExperimentConditionContext : DbContext
{
    public DbSet<ExperimentConditionEntity> Conditions { get; set; }

    public ExperimentConditionContext(DbContextOptions<ExperimentConditionContext> options, DbSet<ExperimentConditionEntity> conditions) : base(options)
    {
        Conditions = conditions;
    }
}