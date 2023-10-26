using DataLib.Cards;
using DataLib.Persons.Opponents;

namespace DataLib.Persons.Distributors;

public interface  IDistributor
{
    public bool Judge(Card cardFirstOpponent, Card cardSecondOpponent);
}