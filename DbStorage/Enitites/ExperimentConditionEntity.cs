using DataLib.Cards;
using Models;

namespace DbStorage.Enitites;

public class ExperimentConditionEntity
{
    public int Id { get; set; }
    public IList<CardEntity> CardEntities { get; set; }

    public ExperimentConditionEntity(IList<CardEntity> entities)
    {
        CardEntities = entities;
    }

    public ExperimentConditionEntity(EnumerableDesk desk)
    {
        CardEntities = (from Card card in desk select CardEntity.FromCard(card)).ToList();
    }

    public static ExperimentConditionEntity FromDeck(EnumerableDesk desk)
    {
        return new ExperimentConditionEntity(desk);
    }

    public EnumerableDesk ToDeck()
    {
        return new EnumerableDesk(CardEntities.Select(c => c.ToCard()).ToList());
    }
}