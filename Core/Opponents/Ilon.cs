using DataLib.Opponents;
using DataLib.Strategies.Interfaces;

namespace Core.Opponents;

public class Ilon: Opponent
{
    public Ilon(ICardPickStrategy strategy) : base("Ilon", strategy)
    {
    }
}