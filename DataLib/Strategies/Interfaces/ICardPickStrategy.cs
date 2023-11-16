using DataLib.Cards;

namespace DataLib.Strategies.Interfaces;

public interface ICardPickStrategy
{
    public Card Pick(IEnumerable<Card> cards);
}