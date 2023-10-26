using DataLib.Cards;
using DataLib.Persons.Distributors;

namespace Core.Distributors;

public class Zeus : IDistributor
{
    public bool Judge(Card cardFirstOpponent, Card cardSecondOpponent)
    {
        return cardFirstOpponent.Color == cardSecondOpponent.Color;
    }
}