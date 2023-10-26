using DataLib.Persons.Opponents.Strategies;

namespace DataLib.Persons.Opponents;
using Cards;

public abstract class Opponent : Person, IChooseCard
{
    private readonly ICardPickStrategy _strategy;

    protected Opponent(string name, ICardPickStrategy strategy) : base(name)
    {
        _strategy = strategy;
    }
    
    public Card Choose(IEnumerable<Card> cards)
    {
        return _strategy.Pick(cards);
    }
}