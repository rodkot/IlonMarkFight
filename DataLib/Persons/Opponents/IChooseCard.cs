using DataLib.Cards;

namespace DataLib.Persons.Opponents;

public interface IChooseCard
{
    public Card Choose(Card[] cards);
}