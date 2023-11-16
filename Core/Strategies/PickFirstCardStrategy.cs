using DataLib.Cards;
using DataLib.Strategies.Interfaces;

namespace Core.Strategies;

public class PickFirstCardStrategy: ICardPickStrategy
{
    public Card Pick(IEnumerable<Card> cards)
    {
        return cards.First();
    }
}