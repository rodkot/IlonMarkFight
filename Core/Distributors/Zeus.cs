using DataLib.Cards;
using DataLib.Distributors.Interfaces;

namespace Core.Distributors;

public class Zeus : IDistributor
{
    public bool Judge(Card cardFirstOpponent, Card cardSecondOpponent)
    {
        return cardFirstOpponent.Color == cardSecondOpponent.Color;
    }
}