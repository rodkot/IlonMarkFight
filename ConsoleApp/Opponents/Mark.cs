using DataLib.Persons.Opponents;
using DataLib.Persons.Opponents.Strategies;

namespace ConsoleApp.Opponents;


public class Mark: Opponent
{
    public Mark(ICardPickStrategy strategy) : base("Mark", strategy)
    {
    }
}