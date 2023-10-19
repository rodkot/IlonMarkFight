using DataLib.Persons.Opponents.Strategies;

namespace DataLib.Persons.Opponents;

public abstract class Opponent : Person
{
    internal Card.Card ChooseCard { get; private  set; }
    private readonly ICardPickStrategy _strategy;

    protected Opponent(string name, ICardPickStrategy strategy) : base(name)
    {
        _strategy = strategy;
    }

    public void Choose(Card.Card[] cards)
    {
        ChooseCard = _strategy.Pick(cards);
    }
}