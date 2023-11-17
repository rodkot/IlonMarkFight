using System.Collections;
using DataLib.Cards;
using DataLib.Desks.Interfaces;

namespace Models;

public class EnumerableDesk : IShuffleableDesk, IEnumerable
{
    public IList<Card> Cards { get; }
    public EnumerableDesk(IList<Card> cards)
    {
        Cards = cards;
    }

    public IEnumerator GetEnumerator()
    {
        return Cards.GetEnumerator();
    }
}

