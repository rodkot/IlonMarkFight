using DataLib.Cards;

namespace DataLib;

public interface IHttpAskerOpponent
{
    public Task<Card> Ask(IEnumerable<Card> cards);
}