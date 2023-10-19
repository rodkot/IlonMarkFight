namespace DataLib.Persons.Opponents.Strategies;
using Cards;
public interface ICardPickStrategy
{
    public Card Pick(IEnumerable<Card> cards);
}