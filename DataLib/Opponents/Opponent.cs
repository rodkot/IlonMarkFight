using DataLib.Cards;
using DataLib.Opponents.Interfaces;
using DataLib.Strategies.Interfaces;

namespace DataLib.Opponents;

public abstract class Opponent : IChooseCard
{
    public string Name {  get; init; }
    private readonly ICardPickStrategy _strategy;

    public Opponent(string name, ICardPickStrategy strategy)
    {
        Name = name;
        _strategy = strategy;
    }

    public Card Choose(IEnumerable<Card> cards)
    {
        return _strategy.Pick(cards);
    }
}