using DataLib.Persons.Opponents;
using DataLib.Persons.Opponents.Strategies;

namespace ConsoleApp.Opponents;

public class Ilon: Opponent
{
    public Ilon(ICardPickStrategy strategy) : base("Ilon", strategy)
    {
    }
}