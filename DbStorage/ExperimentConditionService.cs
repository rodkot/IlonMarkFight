using DataLib.Desks.Interfaces;
using DbStorage.Enitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace DbStorage;

/*
 * TODO Вопросы
 * Нужно ли сохранять стратегии? Или что/как сохранять контекст игры?
 * Нужны ли миграции?
 */
public class ExperimentConditionService
{
    private readonly ExperimentConditionContext _experimentConditionContext;
    private readonly ILogger<ExperimentConditionService> _logger;

    public ExperimentConditionService(
        IDbContextFactory<ExperimentConditionContext> contextFactory, 
        ILogger<ExperimentConditionService> logger)
    {
        _experimentConditionContext = contextFactory.CreateDbContext();
        _logger = logger;
        _experimentConditionContext.Database.EnsureCreated();
    }

    public void RecreateDb()
    {
        _experimentConditionContext.Database.EnsureDeleted();
        _experimentConditionContext.Database.EnsureCreated();
    }

    public void AddOne(EnumerableDesk deck)
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