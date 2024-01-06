using DataLib.Desks.Interfaces;
using DbStorage.Context;
using DbStorage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace DbStorage.Service;

public class ExperimentConditionService
{
    private readonly ExperimentConditionContext _experimentConditionContext;
 

    public ExperimentConditionService(
        IDbContextFactory<ExperimentConditionContext> contextFactory)
    {
        _experimentConditionContext = contextFactory.CreateDbContext();
        _experimentConditionContext.Database.EnsureCreated();
    }

    public void RecreateDb()
    {
        _experimentConditionContext.Database.EnsureDeleted();
        _experimentConditionContext.Database.EnsureCreated();
    }

    public void AddOne(ShuffleableDesk desk)
    {
        _experimentConditionContext.Conditions.Add(ExperimentConditionEntity.FromDesk(desk));
        _experimentConditionContext.SaveChanges();
    }

    public IList<IShuffleableDesk> GetFirstN(int n)
    {
        var conditions = _experimentConditionContext.Conditions.OrderBy(c => c.Id).Take(n);
        var desks = new List<IShuffleableDesk>();
        if (!conditions.Any())
        {
            return desks;
        }

        foreach (var cond in conditions)
        {
            _experimentConditionContext.Entry(cond).Collection(c => c.CardEntities).Load();
            desks.Add(cond.ToDesk());
        }

        return desks;
    }
}