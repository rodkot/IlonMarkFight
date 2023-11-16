using DataLib.Cards;

namespace DataLib.Strategies.Interfaces;

public interface ICardPickStrategy
{
    Card Pick(IEnumerable<Card> cards);
}