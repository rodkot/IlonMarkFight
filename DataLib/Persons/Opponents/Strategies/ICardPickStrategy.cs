namespace DataLib.Persons.Opponents.Strategies;

public interface ICardPickStrategy
{
    public abstract Card.Card Pick(Card.Card[] cards);
}