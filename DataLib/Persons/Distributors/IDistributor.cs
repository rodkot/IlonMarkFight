using DataLib.Cards;

namespace DataLib.Persons.Distributors;

public interface  IDistributor
{
    public bool Judge(Card cardFirstOpponent, Card cardSecondOpponent);
}