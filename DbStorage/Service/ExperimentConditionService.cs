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
        ExperimentConditionContext context)
    {
        _experimentConditionContext = context;
        _experimentConditionContext.Database.EnsureCreated();
    }

    public void RecreateDb()
    {
        _experimentConditionContext.Database.EnsureDeleted();
        _experimentConditionContext.Database.EnsureCreated();
    }

    public void AddOne(ShuffleableDesk deck)
    {
        _experimentConditionContext.Conditions.Add(ExperimentConditionEntity.FromDeck(deck));
        _experimentConditionContext.SaveChanges();
    }

    public IList<IShuffleableDesk> GetFirstN(int n)
    {
        var conditions = _experimentConditionContext.Conditions.OrderBy(c => c.Id).Take(n);
        var decks = new List<IShuffleableDesk>();
        if (!conditions.Any())
        {
            return decks;
        }

        foreach (var cond in conditions)
        {
            _experimentConditionContext.Entry(cond).Collection(c => c.CardEntities).Load();
            decks.Add(cond.ToDeck());
        }

        return decks;
    }
}