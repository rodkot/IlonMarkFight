using DataLib.Cards;
using DataLib.Opponents.Interfaces;
using DataLib.Strategies.Interfaces;

namespace DataLib.Opponents;

public abstract class Opponent : IChooseCard
{
    private string Name { get; init; }
    private readonly ICardPickStrategy _strategy;

    protected Opponent(string name, ICardPickStrategy strategy)
    {
        Name = name;
        _strategy = strategy;
    }

    public Card Choose(IEnumerable<Card> cards)
    {
        return _strategy.Pick(cards);
    }
}