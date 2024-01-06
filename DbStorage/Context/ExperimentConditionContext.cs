using DbStorage.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbStorage.Context;

public class ExperimentConditionContext : DbContext
{
    public DbSet<ExperimentConditionEntity> Conditions { get; set; }

    public ExperimentConditionContext(DbContextOptions<ExperimentConditionContext> options) : base(options) { }
}