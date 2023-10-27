using DataLib.Cards;

namespace DataLib.Persons.Distributors;

public interface  IDistributor
{ 
    bool Judge(Card cardFirstOpponent, Card cardSecondOpponent);
}