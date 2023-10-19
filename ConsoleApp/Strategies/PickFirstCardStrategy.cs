using DataLib.Card;
using DataLib.Persons.Opponents.Strategies;

namespace ConsoleApp.Strategies;

public class PickFirstCardStrategy: ICardPickStrategy
{
    public Card Pick(Card[] cards)
    {
        return cards.First();
    }
}