using DataLib.Opponents;
using DataLib.Strategies.Interfaces;

namespace Core.Opponents;


public class Mark: Opponent
{
    public Mark(ICardPickStrategy strategy) : base("Mark", strategy)
    {
    }
}