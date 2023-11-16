using DataLib.Cards;

namespace DataLib.Opponents.Interfaces;

public interface IChooseCard
{
    Card Choose(IEnumerable<Card> cards);
}