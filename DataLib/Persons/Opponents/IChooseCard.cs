using DataLib.Cards;

namespace DataLib.Persons.Opponents;

public interface IChooseCard
{ 
    Card Choose(IEnumerable<Card> cards);
}