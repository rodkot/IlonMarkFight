using DataLib.Cards;
using DataLib.Opponents.Interfaces;

namespace DataLib.Opponents;

public class AsyncOpponent: IChooseCardAsync
{
    public string Name {  get; init; }
    private IHttpAskerOpponent asker;
 

    public AsyncOpponent(IHttpAskerOpponent asker)
    {
       
    }

   

    public async Task<Card> Choose(IEnumerable<Card> cards)
    {
        return await asker.Ask(cards);
    }
}