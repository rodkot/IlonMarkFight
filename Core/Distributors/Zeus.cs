using DataLib.Persons.Distributors;
using DataLib.Persons.Opponents;

namespace Core.Distributors;

public class Zeus : IDistributor
{
    public bool Judge(in Opponent firstOpponent, in Opponent secondOpponent)
    {
        return firstOpponent.ChooseCard.Color == secondOpponent.ChooseCard.Color;
    }
}