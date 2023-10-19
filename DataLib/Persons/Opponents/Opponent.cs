using DataLib.Persons.Opponents.Strategies;

namespace DataLib.Persons.Opponents;

public abstract class Opponent : Person
{
    public Cards.Card ChooseCard { get; private  set; }
    private readonly ICardPickStrategy _strategy;

    protected Opponent(string name, ICardPickStrategy strategy) : base(name)
    {
        _strategy = strategy;
    }

    public void Choose(Cards.Card[] cards)
    {
        ChooseCard = _strategy.Pick(cards);
    }
}