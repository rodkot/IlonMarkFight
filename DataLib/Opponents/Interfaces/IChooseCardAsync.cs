using DataLib.Cards;

namespace DataLib.Opponents.Interfaces;

public interface IChooseCardAsync
{
   Task<Card> Choose(IEnumerable<Card> cards);
}