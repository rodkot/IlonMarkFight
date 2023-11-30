using DataLib.Cards;
using Models;

namespace DbStorage.Entities;

public class ExperimentConditionEntity
{
    public int Id { get; set; }
    public IList<CardEntity> CardEntities { get; set; }

    public ExperimentConditionEntity(IList<CardEntity> entities)
    {
        CardEntities = entities;
    }

    public ExperimentConditionEntity(ShuffleableDesk desk)
    {
        CardEntities = (from Card card in desk select CardEntity.FromCard(card)).ToList();
    }

    public static ExperimentConditionEntity FromDeck(ShuffleableDesk desk)
    {
        return new ExperimentConditionEntity(desk);
    }

    public ShuffleableDesk ToDeck()
    {
        return new ShuffleableDesk(CardEntities.Select(c => c.ToCard()).ToList());
    }
}