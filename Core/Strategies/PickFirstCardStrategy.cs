using DataLib.Cards;
using DataLib.Persons.Opponents.Strategies;

namespace Core.Strategies;

public class PickFirstCardStrategy: ICardPickStrategy
{
    public Card Pick(IEnumerable<Card> cards)
    {
        return cards.First();
    }
}