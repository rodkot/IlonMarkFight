using DataLib.Persons.Opponents;

namespace DataLib.Persons.Distributors;

public interface  IDistributor
{
    public bool Judge(in Opponent firstOpponent, in Opponent secondOpponent);
}