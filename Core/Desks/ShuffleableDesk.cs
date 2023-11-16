using DataLib.Cards;
using DataLib.Desks;
using DataLib.Desks.Interfaces;

namespace Core.Desks;

public class ShuffleableDesk : IShuffleableDesk
{
    public IList<Card> Cards { get; }

    public int Length => Cards.Count;

    public Card GetByIndex(int index)
    {
        return Cards[index];
    }

    public void Split(out Card[] first, out Card[] second)
    {
        var mid = Length / 2;
        first = Cards.Take(mid).ToArray();
        second = Cards.Skip(mid).ToArray();
    }

    public ShuffleableDesk(int count)
    {
        var cards = new Card[count];
        for (var i = 0; i < count; i += 2)
        {
            cards[i] = new Card(Color.Black, i);
            cards[i + 1] = new Card(Color.Red, i + 1);
        }

        Cards = cards;
    }

    public void SwapCards(int i, int j)
    {
        if (i >= Cards.Count || j >= Cards.Count) return;
        (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
    }
}