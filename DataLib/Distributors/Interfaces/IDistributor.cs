using DataLib.Cards;

namespace DataLib.Distributors.Interfaces;

public interface IDistributor
{
    bool Judge(Card cardFirstOpponent, Card cardSecondOpponent);
}